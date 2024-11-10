import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TaskDto } from "../modal/TaskDto";
@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private baseUrl = 'api/task/'; 

  constructor(private http: HttpClient) { }

  getTask(page: number = 1, pageSize: number = 10): Observable<any> {
    const url = `${this.baseUrl}GetTasks`;
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<any>(url, { params });
  }

  getTasksById(taskId: number): Observable<TaskDto> {
    const url = `${this.baseUrl}GetTaskById`;
    const params = new HttpParams().set('taskId', taskId.toString());
    return this.http.get<TaskDto>(url, { params });
  }

  createTask(data: TaskDto): Observable<any> {
    const url = `${this.baseUrl}AddTask`;
    return this.http.post<any>(url, data);
  }

  updateTask(data: TaskDto): Observable<any> {
    const url = `${this.baseUrl}UpdateTask`;
    return this.http.put<any>(url, data);
  }

  deleteTask(taskId: number): Observable<any> {
    const url = `${this.baseUrl}DeleteTask`;
    const params = new HttpParams().set('id', taskId.toString());
    return this.http.delete<any>(url, { params });
  }
}
