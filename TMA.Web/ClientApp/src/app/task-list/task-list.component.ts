import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { TaskService } from '../services/task.service';
import { TaskDto } from '../modal/TaskDto';
import { Router } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal';

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
  isDeleteMode: boolean = false;
  hasMoreTasks = true;
  @ViewChild('deleteTaskModal', { static: true }) deleteTaskModal!: ModalDirective;

  constructor(
    private readonly taskService: TaskService,
    private router: Router
  ) { }

  ngOnInit() {
    this.getTasks();
  }

  getTasks() {
    this.loading = true;
    this.taskService.getTask(this.currentPage, this.pageSize).subscribe((data: any) => {
      this.tasks = data.tasks;
      this.totalTasks = data.count;
      this.loading = false;
    });
  }


  createTask() {
    this.router.navigate(['/tasks/create']);
  }

  updateTask(taskId: number) {
    this.router.navigate(['/tasks/edit', taskId]);
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
        this.router.navigate(['/task-list']);
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
