export class TaskDto {
  taskId: number | null = null;
  taskTypeId: number | null  = null;
  taskStatusId: number | null = null;
  title: string | null = null;
  description: string | null = null;
  isCompleted: boolean = false;
  createdBy: string | null = null;
  modifiedBy: string | null = null;
  createdDate: Date | null=null;
  modifiedDate: Date | null = null;
}
