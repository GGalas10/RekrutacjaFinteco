import { Component, OnInit } from '@angular/core';
import { User } from './models/Users';
import { MatListModule } from '@angular/material/list';
import { NgClass, NgIf } from '@angular/common';
import { UserService } from './services/user.service';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [
    MatListModule,
    NgClass,
    UserDetailsComponent,
    NgIf,
    MatProgressSpinnerModule,
  ],
})
export class AppComponent implements OnInit {
  userList!: User[];
  selectedUser!: User;
  showDetails = false;
  title = 'Test for Finteco';
  ApiResponded = false;
  constructor(private userService: UserService) {}
  ngOnInit(): void {
    this.userService.GetAllUsers().subscribe({
      next: (value) => {
        this.ApiResponded = true;
        this.userList = value;
      },
      error: (err) => (this.ApiResponded = true),
    });
  }
  GetProgrammers(): User[] {
    return this.userList.filter((x) => x.type == 1);
  }
  GetDevOps(): User[] {
    return this.userList.filter((x) => x.type == 2);
  }
  SelectUser(user: User) {
    this.showDetails = false;
    this.selectedUser = user;
    setTimeout(() => (this.showDetails = true));
  }
  IsSelected(userId: string): boolean {
    if (!this.selectedUser) {
      return false;
    }
    if (userId == this.selectedUser.userId) return true;
    return false;
  }
}
