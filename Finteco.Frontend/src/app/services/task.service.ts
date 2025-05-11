import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagginationTaskListDTO, TaskDetails } from '../models/Tasks';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private baseUrl = 'https://localhost:7110/api';
  constructor(private http: HttpClient) {}
  GetAllUserTasks(
    userId: string,
    page: number
  ): Observable<PagginationTaskListDTO> {
    return this.http.get<PagginationTaskListDTO>(
      `${this.baseUrl}/Task/GetAllUserTasks?userId=${userId}&page=${page}`
    );
  }
  GetAllAvaliableTasks(
    userType: number,
    page: number
  ): Observable<PagginationTaskListDTO> {
    return this.http.get<PagginationTaskListDTO>(
      `${this.baseUrl}/Task/GetAllTaskToAssigned?type=${userType}&page=${page}`
    );
  }
  GetTaskDetails(taskId: string): Observable<TaskDetails> {
    return this.http.get<TaskDetails>(
      `${this.baseUrl}/Task/GetTaskDetails?taskId=${taskId}`
    );
  }
}
