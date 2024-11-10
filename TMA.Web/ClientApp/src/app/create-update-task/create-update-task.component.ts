import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskDto } from '../modal/TaskDto';
import { ValidationHelper } from '../utility/validation-helper';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskService } from '../services/task.service';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DropdownValuesDto } from '../modal/dropdown-values-dto';
import * as _ from 'lodash';
import { CommonDropdownValues } from '../constants';
import { TokenService } from '../services/token.service';


@Component({
  selector: 'app-create-update-task',
  templateUrl: './create-update-task.component.html',
  styleUrls: ['./create-update-task.component.css']
})
export class CreateUpdateTaskComponent implements OnInit {
  taskForm!: FormGroup;
  submitted = false;
  isDeleteMode: boolean = false;
  loggedinUser: string = '';
  taskStatusList: DropdownValuesDto[] = [];
  taskTypeList: DropdownValuesDto[] = [];
  userRoleList: DropdownValuesDto[] = [];
  @ViewChild('deleteTaskModal', { static: true }) deleteTaskModal!: ModalDirective;
  constructor(
    private formBuilder: FormBuilder,
    private readonly taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router,
    private auth: TokenService,

  ) {
    this.taskStatusList = _.cloneDeep(CommonDropdownValues.TaskStatusTypes);
    this.taskTypeList = _.cloneDeep(CommonDropdownValues.TaskTypes);
    this.userRoleList = _.cloneDeep(CommonDropdownValues.UserRoles);
    this.initializeForm();
    this.loggedinUser = this.auth.getItem('userName');
  }

  ngOnInit() {
    const taskId = +this.route.snapshot.paramMap.get('id')!;
    if (taskId) {
      this.taskService.getTasksById(taskId).subscribe((task: TaskDto) => {
        this.taskForm.patchValue({
          taskId: task.taskId || 0,
          title: task.title,
          description: task.description || '',
          taskTypeId: this.isNullOrEmptyForKey(task.taskTypeId!) ? null : task.taskTypeId!.toString(),
          taskStatusId: this.isNullOrEmptyForKey(task.taskStatusId!) ? null : task.taskStatusId!.toString(),
          createdBy: task.createdBy || '',
          modifiedBy: task.modifiedBy || '',
          createdDate: task.createdDate ? new Date(task.createdDate) : new Date(),
          modifiedDate: task.modifiedDate ? new Date(task.modifiedDate) : null,
          isCompleted: task.isCompleted || false,
        });
      });
    }

  }

  initializeForm() {
    this.taskForm = this.formBuilder.group({
      taskId: [0],
      title: ['', [Validators.required]],
      description: [''],
      taskTypeId: [null, [Validators.required]],
      taskStatusId: [null, [Validators.required]],
      createdBy: '',
      modifiedBy: '',
      createdDate: new Date(),
      modifiedDate: null,
      isCompleted: false,
    });
  }
  saveTaskForm() {
    this.submitted = true;
    if (this.taskForm.value.taskId == 0) {
      this.taskForm.get('createdBy')?.setValue(this.loggedinUser);
    } else if (this.taskForm.value.taskId > 0 && this.taskForm.dirty) {
      this.taskForm.get('modifiedBy')?.setValue(this.loggedinUser);
    }
    if (this.taskForm.invalid) {
      ValidationHelper.validateAllFormFields(this.taskForm);
      return;
    }

    const taskDto: TaskDto = {
      ...this.taskForm.value,
      taskId: this.taskForm.value.taskId || 0
    };

    const saveObservable = taskDto.taskId! > 0
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
    const taskId = this.taskForm.value.taskId;
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

  isNullOrEmptyForKey(value: string | number): boolean {
    return (
      value == 'undefined' || value == undefined || value == null || value == "null" || value == ""
    );
  }
}

