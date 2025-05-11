import { Injectable } from '@angular/core';
import { User } from '../models/Users';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserTaskAssignmentRequest } from '../models/Tasks';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = 'https://localhost:7110/api';
  constructor(private http: HttpClient) {}
  GetAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}/User/GetAllUsers`);
  }
  AssignTaskToUser(command: UserTaskAssignmentRequest): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Task/AssignTasks`, command);
  }
}
