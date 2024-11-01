import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TaskListComponent } from './task-list.component';
import { TaskService } from '../services/task.service';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { TaskDto } from '../modal/TaskDto';
import { ElementRef } from '@angular/core';
import { ModalDirective, ModalModule } from 'ngx-bootstrap/modal';

describe('TaskListComponent', () => {
  let component: TaskListComponent;
  let fixture: ComponentFixture<TaskListComponent>;
  let routerSpy: jasmine.SpyObj<Router>;
  let mockTaskService: jasmine.SpyObj<TaskService>;
  let deleteTaskModalMock: jasmine.SpyObj<ModalDirective>;
  beforeEach(async () => {
     mockTaskService = jasmine.createSpyObj('TaskService', ['getTask', 'deleteTask']);
     routerSpy = jasmine.createSpyObj('Router', ['navigate']);
     deleteTaskModalMock = jasmine.createSpyObj('ModalDirective', ['show', 'hide']);

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
    component.deleteTaskModal = deleteTaskModalMock;
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

  describe('closeDeleteModal', () => {
    it('should reset the modal state and hide the modal', () => {
      component.isDeleteMode = true; // Set initial state
      component.closeDeleteModal(); // Call the method

      expect(component.isDeleteMode).toBeFalse(); // Check if isDeleteMode is reset
      expect(component.deleteTaskModal.hide).toHaveBeenCalled(); // Check if hide was called
    });
  });

  describe('openDeletetaskModal', () => {
    it('should open the delete modal and set the correct task ID in openDeletetaskModal', () => {
      const taskId = 5; // Sample task ID
      component.openDeleteTaskModal(taskId);

      // Check that delete mode is enabled
      expect(component.isDeleteMode).toBeTrue();

      // Verify that the correct task ID is set
      expect(component.selectedTaskId).toEqual(taskId);

      // Check that the modal's show method was called
      expect(deleteTaskModalMock.show).toHaveBeenCalled();
    });
  });

  describe('deleteTask', () => {
    it('should call deleteTask on TaskService, close the modal, and navigate to task list', () => {
      const taskId = 5; // Sample task ID
      component.selectedTaskId = taskId;

      // Mock the deleteTask service method to return an observable
      mockTaskService.deleteTask.and.returnValue(of(null));

      // Call the deleteTask method
      component.deleteTask();

      // Assert that deleteTask was called on the service with the correct task ID
      expect(mockTaskService.deleteTask).toHaveBeenCalledWith(taskId);

      // Assert that closeDeleteModal hides the modal
      expect(deleteTaskModalMock.hide).toHaveBeenCalled();

      // Assert that the router navigates to the task list
      expect(routerSpy.navigate).toHaveBeenCalledWith(['/task-list']);
    });

    it('should not call deleteTask if selectedTaskId is invalid', () => {
      component.selectedTaskId = 0; // Invalid ID

      // Call the deleteTask method
      component.deleteTask();

      // Assert that deleteTask was not called on the service
      expect(mockTaskService.deleteTask).not.toHaveBeenCalled();
    });
  });
});

