import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TaskListComponent } from './task-list.component';
import { TaskService } from '../services/task.service';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { TaskDto } from '../modal/TaskDto';
import { ElementRef } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';

describe('TaskListComponent', () => {
  let component: TaskListComponent;
  let fixture: ComponentFixture<TaskListComponent>;
  let routerSpy: jasmine.SpyObj<Router>;
  let mockTaskService: jasmine.SpyObj<TaskService>;
  beforeEach(async () => {
     mockTaskService = jasmine.createSpyObj('TaskService', ['getTask', 'deleteTask']);
     routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      imports: [ModalModule.forRoot()],
      declarations: [TaskListComponent],
      providers: [
        { provide: TaskService, useValue: mockTaskService },
        { provide: Router, useValue: routerSpy }
      ]
    });
    fixture = TestBed.createComponent(TaskListComponent);
    component = fixture.componentInstance;
    const mockDeleteTaskModal = jasmine.createSpyObj('ModalDirective', ['hide']);
    component.deleteTaskModal = mockDeleteTaskModal;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  

  it('should navigate to create task page on createTask call', () => {
    component.createTask();

    expect(routerSpy.navigate).toHaveBeenCalledWith(['/tasks/create']);
  });

  it('should navigate to edit task page on updateTask call', () => {
    const taskId = 1;
    component.updateTask(taskId);

    expect(routerSpy.navigate).toHaveBeenCalledWith(['/tasks/edit', taskId]);
  });

  it('should fetch the next page of Tasks', () => {
    component.currentPage = 1;
    component.totalTasks = 20;
    component.pageSize = 10;
    spyOn(component, 'getTasks');

    component.nextPage();

    expect(component.currentPage).toEqual(2);
    expect(component.getTasks).toHaveBeenCalled();
  });

  it('should fetch the previous page of Tasks', () => {
    component.currentPage = 2;
    component.pageSize = 10;
    spyOn(component, 'getTasks');

    component.previousPage();

    expect(component.currentPage).toEqual(1);
    expect(component.getTasks).toHaveBeenCalled();
  });

  describe('deleteTask', () => {
    it('should call deleteTask on TaskService and getTasks if confirmed', () => {
      const taskId = 1;

      // Mock the confirm function to return true
      spyOn(window, 'confirm').and.returnValue(true);

      // Mock deleteTask to return an observable
      mockTaskService.deleteTask.and.returnValue(of(null)); // Assuming deleteTask returns an observable

      // Call the deleteTask method
      component.deleteTask(taskId);

      // Assert that deleteTask was called with the correct taskId
      expect(mockTaskService.deleteTask).toHaveBeenCalledWith(taskId);

      // After deleting, expect getTasks to be called
      expect(mockTaskService.getTask).toHaveBeenCalled();
    });

    it('should not call deleteTask on TaskService if not confirmed', () => {
      const taskId = 1;

      // Mock the confirm function to return false
      spyOn(window, 'confirm').and.returnValue(false);

      // Call the deleteTask method
      component.deleteTask(taskId);

      // Assert that deleteTask was not called
      expect(mockTaskService.deleteTask).not.toHaveBeenCalled();
    });
  });
  describe('closeDeleteModal', () => {
    it('should reset the modal state and hide the modal', () => {
      component.isDeleteMode = true; // Set initial state
      component.closeDeleteModal(); // Call the method

      expect(component.isDeleteMode).toBeFalse(); // Check if isDeleteMode is reset
      expect(component.deleteTaskModal.hide).toHaveBeenCalled(); // Check if hide was called
    });
  });

});
