import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { TaskDto } from "../modal/TaskDto";
import { DataService } from "./data.service";

@Injectable({
  providedIn: 'root'
})
export class TaskService extends DataService {
  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    super(http, baseUrl);
  }

  getTask(page = 1, pageSize = 10): Observable<TaskDto[]> {
    const getTaskURL = `task/GetTasks?page=${page}&pageSize=${pageSize}`;
    return this.get(getTaskURL);
  }

  getTasksById(taskId: number): Observable<TaskDto> {
    const getTaskByIdUrl = `task/GetTaskById?taskId=${taskId}`;
    return this.get(getTaskByIdUrl);
  }

  createTask(data: TaskDto): Observable<any> {
    const createTaskURL = `task/AddTask/`;
    return this.post(createTaskURL, data);
  }

  updateTask(data: TaskDto): Observable<any> {
    const updateTaskURL = `task/UpdateTask/`;
    return this.put(updateTaskURL, data);
  }

  deleteTask(taskId: number): Observable<any> {
    const deleteTaskUrl = `task/DeleteTask?id=${taskId}`;
    return this.delete(deleteTaskUrl);
  }

  //validateUserAndGetToken(userCredential: any): Observable<any> {
  //  return this.post(`task/ValidateUserAndGetToken`, userCredential);
  //}
}
