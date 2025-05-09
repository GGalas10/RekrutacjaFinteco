import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/Users';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private baseUrl = 'https://localhost:7110/api';
  constructor(private http: HttpClient) {}
  GetAllUsers():Observable<User[]>{
    return this.http.get<User[]>(`${this.baseUrl}/User/GetAllUsers`);
  }
}
