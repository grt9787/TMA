import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListComponent } from './task-list/task-list.component';
import { TaskService } from './services/task.service';
import { AppRoutingModule } from './app-routing.module';
import { CreateUpdateTaskComponent } from './create-update-task/create-update-task.component';
import { AppComponent } from './app.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { LoginComponent } from './login/login.component';
import { TokenService } from './services/token.service';
import { NgSelectModule } from '@ng-select/ng-select';
import { AdminRoleManagementComponent } from './admin-role-management/admin-role-management.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TaskListComponent,
    CreateUpdateTaskComponent,
    LoginComponent,
    AdminRoleManagementComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    ModalModule.forRoot(),
    AppRoutingModule,
    NgSelectModule
  ],
  providers: [TaskService,TokenService],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA],
})
export class AppModule { }
