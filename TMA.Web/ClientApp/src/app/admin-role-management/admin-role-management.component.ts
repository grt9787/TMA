import { Component, OnInit } from '@angular/core';
import { Action, Role, RoleAction, RoleActionUpdateRequest } from '../constants';
import { RoleService } from '../services/role.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-role-management',
  templateUrl: './admin-role-management.component.html',
  styleUrls: ['./admin-role-management.component.css']
})

export class AdminRoleManagementComponent implements OnInit {
  roles: Role[] = [];
  actions: Action[] = [];
  roleActions: RoleAction[] = [];
  selectedRoleId: number = 0;

  constructor(private roleService: RoleService, private router: Router,) { }

  ngOnInit(): void {
    this.roleService.getRoles()?.subscribe((data: any) => {
      this.roles = data;
    });
    this.roleService.getActions()?.subscribe((data: any) => {
      this.actions = data;
    });
  }

  onRoleSelect(event: any) {

    this.selectedRoleId = event.roleId;
    this.roleService.getRoleActions(this.selectedRoleId).subscribe((data: any) => {
      this.roleActions = data;
    });
  }


  updateRoleAction(actionId: number, firstCheckboxValue: boolean | Event, secondCheckboxValue: boolean | Event) {
    const hasFullAccess = firstCheckboxValue instanceof Event ? (firstCheckboxValue.target as HTMLInputElement).checked : firstCheckboxValue;
    const hasReadOnly = secondCheckboxValue instanceof Event ? (secondCheckboxValue.target as HTMLInputElement).checked : secondCheckboxValue;

    const request: RoleActionUpdateRequest = {
      roleId: this.selectedRoleId,
      actionId,
      hasFullAccess,
      hasReadOnly
    };

    this.roleService.updateRoleAction(request).subscribe({
      next: () => {
        console.log('Role action updated successfully');
      },
      error: (error) => {
        console.error('Error updating role action:', error);
      }
    });
  }

  cancel() {
    this.router.navigate(['/task-list']);
  }
}
