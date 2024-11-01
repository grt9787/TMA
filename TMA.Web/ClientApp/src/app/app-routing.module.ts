import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskListComponent } from './task-list/task-list.component';
import { CreateUpdateTaskComponent } from './create-update-task/create-update-task.component';

const routes: Routes = [
  { path: 'task-list', component: TaskListComponent },
  { path: 'tasks/create', component: CreateUpdateTaskComponent },
  { path: 'tasks/edit/:id', component: CreateUpdateTaskComponent },
  { path: '', redirectTo: '/task-list', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
