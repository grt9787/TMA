import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskDto } from '../modal/TaskDto';
import { ValidationHelper } from '../utility/validation-helper';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskService } from '../services/task.service';
import { ModalDirective } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-create-update-task',
  templateUrl: './create-update-task.component.html',
  styleUrls: ['./create-update-task.component.css']
})
export class CreateUpdateTaskComponent implements OnInit {
  taskForm!: FormGroup;
  submitted = false;
  isDeleteMode: boolean = false;
  @ViewChild('deleteTaskModal', { static: true }) deleteTaskModal!: ModalDirective;
  constructor(
    private formBuilder: FormBuilder,
    private readonly taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.taskForm = this.formBuilder.group({
      id: [0],
      title: ['', [Validators.required]],
      description: [''],
      isCompleted: false,
      createdAt:new Date(),
      updatedAt: ''
    });
  }

  ngOnInit() {
    const taskId = +this.route.snapshot.paramMap.get('id')!;
    if (taskId) {
      this.taskService.getTasksById(taskId).subscribe((task: TaskDto) => {
        this.taskForm.patchValue(task);
      });
    }
  }

  saveTaskForm() {
    this.submitted = true;
    if (this.taskForm.invalid) {
      ValidationHelper.validateAllFormFields(this.taskForm);
      return;
    }

    const taskDto: TaskDto = {
      ...this.taskForm.value,
      id: this.taskForm.value.id || 0
    };

    const saveObservable = taskDto.id! > 0
      ? this.taskService.updateTask(taskDto)
      : this.taskService.createTask(taskDto);

    saveObservable.subscribe(() => {
      this.router.navigate(['/task-list']); 
    });
  }

  openDeleteContactModal() {
    this.isDeleteMode = true;
    this.deleteTaskModal.show();
  }

  deleteTask() {
    const taskId = this.taskForm.value.id;
    if (taskId > 0) {
      this.taskService.deleteTask(taskId).subscribe(() => {
        this.closeDeleteModal();
        this.router.navigate(['/task-list']); 
      });
    }
  }

  closeDeleteModal() {
    this.resetModal();
    this.deleteTaskModal.hide();
  }

  resetModal() {
    this.isDeleteMode = false;
  }

  cancel() {
    this.router.navigate(['/task-list']); 
  }
}

