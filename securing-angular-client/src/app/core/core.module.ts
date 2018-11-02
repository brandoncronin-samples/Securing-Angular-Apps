import { NgModule } from '@angular/core';
import { ProjectService } from './project.service';
import { AccountService } from './account.service';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';

@NgModule({
    imports: [],
    exports: [],
    declarations: [],
    providers: [ 
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
        ProjectService,
        AccountService
    ],
})
export class CoreModule { }
