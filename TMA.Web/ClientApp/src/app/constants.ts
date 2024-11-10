import { DropdownValuesDto } from "./modal/dropdown-values-dto";

export class Constants {
}
export class CommonDropdownValues {

  static TaskTypes: DropdownValuesDto[] = [
    { key: "Request", value: '1', selected: true },
    { key: "Bug", value: '2', selected: false },
    { key: "Task", value: '3', selected: false },
  ];

  static TaskStatusTypes: DropdownValuesDto[] = [
    { key: "To Do", value: '1', selected: true },
    { key: "In Progress", value: '2', selected: false },
    { key: "Done", value: '3', selected: false },
  ];

  static UserRoles: DropdownValuesDto[] = [
    { key: "Admin", value: '1', selected: true },
    { key: "Manager", value: '2', selected: false },
    { key: "User", value: '3', selected: false },
  ];

}
export class PermissionConstants {
  static areaActionMap: { [key: string]: string } = {
    '1': 'create',
    '2': 'edit',
    '3': 'delete',
    '4': 'view'
  };
}
export interface Role {
  roleId: number;
  name: string;
  roleDescription: string;
  isDeleted: boolean;
  createdBy: string;
  createdDate: Date;
  modifiedBy?: string;
  modifiedDate: Date | null;
}

export interface RoleAction {
  roleActionId: number;
  roleId: number;
  actionId: number;
  hasFullAccess: boolean;
  hasReadOnly: boolean;
  createdBy: string;
  createdDate: Date;
  modifiedBy?: string;
  modifiedDate: Date | null;  role?: Role;
  action?: Action;
}

export interface Action {
  actionId: number;
  actionName: string;
  createdBy: string;
  createdDate: Date;
  modifiedBy?: string;
  modifiedDate: Date | null;
}
export interface RoleActionUpdateRequest {
  roleId: number;
  actionId: number;
  hasFullAccess: boolean;
  hasReadOnly: boolean;
}
