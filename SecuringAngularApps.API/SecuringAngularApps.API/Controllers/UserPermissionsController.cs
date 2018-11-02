using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecuringAngularApps.API.Model;

namespace SecuringAngularApps.API.Controllers
{
    [Produces("application/json")]
    [Route("api/UserPermissions")]
    [Authorize(Roles = "Admin")]
    public class UserPermissionsController : Controller
    {
        private readonly ProjectDbContext _context;

        public UserPermissionsController(ProjectDbContext context)
        {
            _context = context;
        }

        // GET: api/UserPermissions
        [HttpGet]
        public IEnumerable<UserPermission> GetUserPermissions()
        {
            return _context.UserPermissions;
        }

        [HttpGet("Project/{projectId}")]
        public IEnumerable<UserPermission> GetUserPermissions(int projectId)
        {
            return _context.UserPermissions.Where(up => up.ProjectId == projectId);
        }

        // GET: api/UserPermissions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserPermission([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userPermission = await _context.UserPermissions.SingleOrDefaultAsync(m => m.UserProfileId == id);

            if (userPermission == null)
            {
                return NotFound();
            }

            return Ok(userPermission);
        }

        // PUT: api/UserPermissions/5
        [HttpPut()]
        public async Task<IActionResult> PutUserPermission([FromBody] UserPermission userPermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(userPermission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPermissionExists(userPermission.UserProfileId, userPermission.ProjectId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserPermissions
        [HttpPost]
        public async Task<IActionResult> PostUserPermission([FromBody] UserPermission userPermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserPermissions.Add(userPermission);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserPermissionExists(userPermission.UserProfileId, userPermission.ProjectId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserPermission", new { id = userPermission.UserProfileId }, userPermission);
        }

        // DELETE: api/UserPermissions/5
        [HttpDelete()]
        public async Task<IActionResult> DeleteUserPermission([FromQuery] string userId, [FromQuery] int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userPermission = await _context.UserPermissions.SingleOrDefaultAsync(m => m.UserProfileId == userId && m.ProjectId == projectId);
            if (userPermission == null)
            {
                return NotFound();
            }

            _context.UserPermissions.Remove(userPermission);
            await _context.SaveChangesAsync();

            return Ok(userPermission);
        }

        private bool UserPermissionExists(string userId, int? projectId)
        {
            return _context.UserPermissions.Any(e => e.UserProfileId == userId && e.ProjectId == projectId);
        }
    }
}