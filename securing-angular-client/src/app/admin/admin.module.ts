import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import {
  MatFormFieldModule,
  MatDialogModule,
  MatButtonModule,
  MatToolbarModule,
  MatTableModule,
  MatInputModule,
  MatSelectModule
} from '@angular/material';

import { ManageProjectsComponent } from './manage-projects.component';
import { AddProjectDialogComponent } from './add-project-dialog.component';
import { DeleteDialogComponent } from './delete-dialog.component';
import { ManagePermissionsComponent } from './manage-permissions.component';
import { AdminRoutingModule } from './admin-routing.module';
import { AddProjectUserDialogComponent } from './add-project-user-dialog.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    MatFormFieldModule,
    MatDialogModule,
    MatButtonModule,
    MatToolbarModule,
    MatTableModule,
    MatInputModule,
    MatSelectModule,
    AdminRoutingModule,
    CoreModule
  ],
  exports: [],
  declarations: [
    ManageProjectsComponent,
    ManagePermissionsComponent,
    AddProjectDialogComponent,
    DeleteDialogComponent,
    AddProjectUserDialogComponent
  ],
  providers: [],
  entryComponents: [
    AddProjectDialogComponent,
    DeleteDialogComponent,
    AddProjectUserDialogComponent
  ]
})
export class AdminModule {}
