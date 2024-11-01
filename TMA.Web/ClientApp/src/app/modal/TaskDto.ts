export class TaskDto {
  id: number | null = null;
  title: string | null = null;
  description: string | null = null;
  isCompleted: boolean = false;
  createdAt: Date | null=null;
  updatedAt: Date | null = null;
}
