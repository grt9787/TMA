import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskListComponent } from './task-list/task-list.component';
import { CreateUpdateTaskComponent } from './create-update-task/create-update-task.component';
import { LoginComponent } from './login/login.component';
import { AdminRoleManagementComponent } from './admin-role-management/admin-role-management.component';
import { PermissionGuard } from './services/permission-guard.service';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'task-list',
    component: TaskListComponent,
    canActivate: [PermissionGuard],
    data: {
      permission: [{ area: '4', action: 'view' }
            ]
    }
  },
  {
    path: 'tasks/create',
    component: CreateUpdateTaskComponent,
    canActivate: [PermissionGuard],
    data: {
      permission: [
        { area: '1', action: 'create' }
      ]
    }
  },
  {
    path: 'tasks/edit/:id',
    component: CreateUpdateTaskComponent,
    canActivate: [PermissionGuard],
    data: {
      permission: [

        { area: '2', action: 'edit' }
      ]
    }
  },
  {
    path: 'roleManagement',
    component: AdminRoleManagementComponent,
    canActivate: [PermissionGuard],
    data: {
      permission: [
        { role: 'Admin', area: '1', action: 'create' ,access: 'FullAccess' },
      
      ]
    }
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
