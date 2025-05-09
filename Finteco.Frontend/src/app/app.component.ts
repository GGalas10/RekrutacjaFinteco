import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TaskService } from './services/task.service';
import { User } from './models/Users';
import {MatListModule} from '@angular/material/list';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [MatListModule]
})
export class AppComponent implements OnInit {
  userList!:User[];
  title = 'Test for Finteco';
  constructor(private taskService:TaskService){}
  ngOnInit(): void {
    this.taskService.GetAllUsers().subscribe({next: value =>{ console.log(value); this.userList = value;}})
  }

}
