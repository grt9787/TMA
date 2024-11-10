import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminRoleManagementComponent } from './admin-role-management.component';
import { RoleService } from '../services/role.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { Action, Role, RoleAction, RoleActionUpdateRequest } from '../constants';
import { RouterTestingModule } from '@angular/router/testing';
import { TokenService } from '../services/token.service';

describe('AdminRoleManagementComponent', () => {
  let component: AdminRoleManagementComponent;
  let fixture: ComponentFixture<AdminRoleManagementComponent>;
  let mockRoleService: jasmine.SpyObj<RoleService>;
  let routerSpy: jasmine.SpyObj<Router>;
  let mockTokenService: jasmine.SpyObj<TokenService>;

  beforeEach(async () => {
    mockRoleService = jasmine.createSpyObj('RoleService', ['getRoles', 'getActions', 'getRoleActions', 'updateRoleAction']);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    mockTokenService = jasmine.createSpyObj('TokenService', ['getItem']);

    await TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      declarations: [AdminRoleManagementComponent],
      providers: [
        { provide: RoleService, useValue: mockRoleService },
        { provide: Router, useValue: routerSpy },
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(AdminRoleManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should call getRoles and getActions on ngOnInit', () => {
    mockRoleService.getRoles.and.returnValue(of([]));  // Mocking the return value
    mockRoleService.getActions.and.returnValue(of([])); // Mocking the return value

    component.ngOnInit(); // Call ngOnInit

    expect(mockRoleService.getRoles).toHaveBeenCalled(); // Check if the method was called
    expect(mockRoleService.getActions).toHaveBeenCalled(); // Check if the method was called
  });
  it('should update role actions on updateRoleAction call', () => {
    const actionId = 1;
    const firstCheckboxValue = true;  // Simulate checked state
    const secondCheckboxValue = false; // Simulate unchecked state
    const updateRequest: RoleActionUpdateRequest = {
      roleId: component.selectedRoleId,
      actionId,
      hasFullAccess: firstCheckboxValue,
      hasReadOnly: secondCheckboxValue,
    };

    mockRoleService.updateRoleAction.and.returnValue(of());

    component.updateRoleAction(actionId, firstCheckboxValue, secondCheckboxValue);

    expect(mockRoleService.updateRoleAction).toHaveBeenCalledWith(updateRequest);
  });

  it('should navigate to task-list on cancel', () => {
    component.cancel();

    expect(routerSpy.navigate).toHaveBeenCalledWith(['/task-list']);
  });

  it('should fetch role actions on role selection', () => {
    const selectedRoleId = 1;
    const roleActionsMockData: RoleAction[] = [
      { actionId: 1, roleId: selectedRoleId, hasFullAccess: true, hasReadOnly: false, roleActionId: 1, createdBy: 'Admin', modifiedDate: null, modifiedBy: '', createdDate: new Date() }
    ];

    mockRoleService.getRoleActions.and.returnValue(of(roleActionsMockData));

    component.onRoleSelect({ roleId: selectedRoleId });

    expect(component.selectedRoleId).toEqual(selectedRoleId);
    expect(mockRoleService.getRoleActions).toHaveBeenCalledWith(selectedRoleId);
    expect(component.roleActions).toEqual(roleActionsMockData);
  });
});
