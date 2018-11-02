using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecuringAngularApps.API.Model;

namespace SecuringAngularApps.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    
    public class AccountController : Controller
    {
        private readonly ProjectDbContext _context;

        public AccountController(ProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet("Users")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var admins = _context.UserPermissions.Where(up => up.Value == "Admin").Select(up => up.UserProfileId).ToList();
            var users = _context.UserProfiles.Where(u => !admins.Contains(u.Id));
            return Ok(users);
        }

        [HttpPost("Profile")]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfile userProfile)
        {
            var existing = _context.UserProfiles.FirstOrDefault(up => up.Email == userProfile.Email);
            if (existing != null) return StatusCode(409);
            userProfile.Id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
            return Ok(userProfile);
        }

        [HttpPut("Profile/{id}")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfile userProfile, string id)
        {
            if (userProfile.Id != id) return BadRequest();
            var existing = _context.UserProfiles.FirstOrDefault(up => up.Id == id);
            if (existing == null) return NotFound();
            existing.FirstName = userProfile.FirstName;
            existing.LastName = userProfile.LastName;
            existing.Email = userProfile.Email;
            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpPost("ContactUs")]
        [AllowAnonymous]
        public IActionResult ContactUs(string email, string subject, string message)
        {
            return Ok();
        }

        [HttpGet("AuthContext")]
        [Authorize]
        public AuthContext GetAuthContext()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = _context.UserProfiles.Include("UserPermissions")
                .FirstOrDefault(u => u.Id == userId);
            if (profile == null) return null;
            return new AuthContext
            {
                UserProfile = profile,
                Claims =
                User.Claims.Select(c => new SimpleClaim
                {
                    Type = c.Type,
                    Value = c.Value
                }).ToList()
            };
        }
    }
}