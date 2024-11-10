import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { TaskService } from '../services/task.service';
import { TaskDto } from '../modal/TaskDto';
import { Router } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { CommonDropdownValues } from '../constants';
import * as _ from 'lodash';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  public tasks: TaskDto[] = [];
  selectedTaskId: number | null = null;
  loading = false;
  currentPage = 1;
  pageSize = 10;
  totalTasks = 0;
  userPermissions: { action: string }[] = []
  taskTypeList: any[] = [];
  isDeleteMode: boolean = false;
  @ViewChild('deleteTaskModal', { static: true }) deleteTaskModal!: ModalDirective;

  constructor(
    private readonly taskService: TaskService,
    private authService: AuthService,

    private router: Router
  ) {
    this.taskTypeList = _.cloneDeep(CommonDropdownValues.TaskTypes).map(taskType => ({
      ...taskType,
      value: Number(taskType.value) 
    }));
  }

  ngOnInit(): void {
    const permissions = this.authService.getUserPermissions();
    this.userPermissions = permissions.map((permission:any) => permission.action);
    this.getTasks();
  }
  hasPermission(action: string): boolean {
    return this.userPermissions.some((permission:any) => permission === action);
  }

  getTasks(): void {
    this.loading = true;
    this.taskService.getTask(this.currentPage, this.pageSize)!.subscribe((data: any) => {
      this.tasks = data.tasks;
      this.totalTasks = data.totalRecords;
      this.loading = false;
    });
  }


  createTask() {
    this.router.navigate(['/tasks/create']);
  }

  updateTask(taskId: number) {
    this.router.navigate(['/tasks/edit',taskId]);
  }

  openDeleteTaskModal(taskId: number) {
    this.isDeleteMode = true;
    this.selectedTaskId = taskId;
    this.deleteTaskModal.show();
  }

  closeDeleteModal() {
    this.resetModal();
    this.deleteTaskModal.hide();
  }


  deleteTask() {
    if (this.selectedTaskId! > 0) {
      this.taskService.deleteTask(this.selectedTaskId!).subscribe(() => {
        this.closeDeleteModal();
        this.getTasks();
      });
    }
  }
  resetModal() {
    this.isDeleteMode = false;
    this.selectedTaskId = 0;
  }

  nextPage() {
    if (this.currentPage * this.pageSize < this.totalTasks) {
      this.currentPage++;
      this.getTasks();
    }
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.getTasks();
    }
  }
}
