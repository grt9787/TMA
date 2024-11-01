import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { CreateUpdateTaskComponent } from './create-update-task.component';
import { TaskService } from '../services/task.service';
import { ModalDirective, ModalModule } from 'ngx-bootstrap/modal';
import { TaskDto } from '../modal/TaskDto';

describe('CreateUpdateTaskComponent', () => {
  let component: CreateUpdateTaskComponent;
  let fixture: ComponentFixture<CreateUpdateTaskComponent>;
  let mockTaskService: jasmine.SpyObj<TaskService>;
  let mockRouter: jasmine.SpyObj<Router>;
  let mockActivatedRoute: jasmine.SpyObj<ActivatedRoute>;

  beforeEach(() => {
    // Create mocks for the services
    mockTaskService = jasmine.createSpyObj('TaskService', ['getTasksById', 'createTask', 'updateTask', 'deleteTask']);
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);
    // Create a mock for ActivatedRoute with paramMap
    mockActivatedRoute = {
      snapshot: {
        paramMap: {
          get: jasmine.createSpy('get').and.callFake((key: string) => {
            if (key === 'id') {
              return null; 
            }
            return null;
          })
        }
      }
    } as any;
    TestBed.configureTestingModule({
      declarations: [CreateUpdateTaskComponent],
      imports: [ReactiveFormsModule, ModalModule.forRoot()],
      providers: [
        FormBuilder,
        { provide: TaskService, useValue: mockTaskService },
        { provide: Router, useValue: mockRouter },
        { provide: ActivatedRoute, useValue: mockActivatedRoute }
      ]
    });

    fixture = TestBed.createComponent(CreateUpdateTaskComponent);
    component = fixture.componentInstance;
    const mockDeleteTaskModal = jasmine.createSpyObj('ModalDirective', ['hide']);
    component.deleteTaskModal = mockDeleteTaskModal;
  });

  describe('ngOnInit', () => {
    it('should patch the form with task data when taskId is present', () => {
      const taskId = 1;
      const task: TaskDto = { id: taskId, title: 'Test Task', description: 'Test Description', isCompleted: false, createdAt: new Date(), updatedAt: null };

      // Setup the mock route to return the taskId
      mockActivatedRoute.snapshot.paramMap.get = () => taskId.toString();
      mockTaskService.getTasksById.and.returnValue(of(task));

      component.ngOnInit();

      expect(component.taskForm.value).toEqual(task);
      expect(mockTaskService.getTasksById).toHaveBeenCalledWith(taskId);
    });

    it('should not call getTasksById if no taskId is present', () => {
      mockActivatedRoute.snapshot.paramMap.get = () => null;

      component.ngOnInit();

      expect(mockTaskService.getTasksById).not.toHaveBeenCalled();
    });
  });

  describe('saveTaskForm', () => {
    it('should mark form as submitted and navigate to task-list on valid form submission', () => {
      component.taskForm.patchValue({ id: 0, title: 'New Task', description: 'Task Description', isCompleted: false });
      mockTaskService.createTask.and.returnValue(of({})); // Mock create task response

      component.saveTaskForm();

      expect(component.submitted).toBeTrue();
      expect(mockTaskService.createTask).toHaveBeenCalledWith(component.taskForm.value);
      expect(mockRouter.navigate).toHaveBeenCalledWith(['/task-list']);
    });

    it('should not submit the form if it is invalid', () => {
      component.taskForm.patchValue({ title: '' }); // Invalid form state

      component.saveTaskForm();

      expect(component.submitted).toBeTrue();
      expect(mockTaskService.createTask).not.toHaveBeenCalled();
 
    });
  });

  describe('deleteTask', () => {
    it('should confirm and delete the task, then navigate to task-list', () => {
      spyOn(window, 'confirm').and.returnValue(true); // Mock confirm dialog
      component.taskForm.patchValue({ id: 1 });
      mockTaskService.deleteTask.and.returnValue(of({})); // Mock delete response

      component.deleteTask();

      expect(mockTaskService.deleteTask).toHaveBeenCalledWith(1);
      expect(mockRouter.navigate).toHaveBeenCalledWith(['/task-list']);
    });

    it('should not delete the task if confirm is canceled', () => {
      spyOn(window, 'confirm').and.returnValue(false); // Mock confirm dialog to cancel

      component.deleteTask();

      expect(mockTaskService.deleteTask).not.toHaveBeenCalled();
    });
  });

  describe('cancel', () => {
    it('should navigate to task-list on cancel', () => {
      component.cancel();

      expect(mockRouter.navigate).toHaveBeenCalledWith(['/task-list']);
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
