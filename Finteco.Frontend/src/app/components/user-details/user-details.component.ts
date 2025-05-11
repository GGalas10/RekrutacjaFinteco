import {
  Component,
  EventEmitter,
  HostListener,
  inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {
  GetTaskTypeShort,
  UserTask,
  UserTaskAssignmentRequest,
} from '../../models/Tasks';
import { TaskService } from '../../services/task.service';
import { User } from '../../models/Users';
import { forkJoin } from 'rxjs';
import { MatListModule, MatListOption } from '@angular/material/list';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { TaskDetailsComponent } from '../task-details/task-details.component';
import { UserService } from '../../services/user.service';
import { MatDialog } from '@angular/material/dialog';
import { ErrorModalComponent } from '../error-modal/error-modal.component';

@Component({
  selector: 'app-user-details',
  imports: [
    MatListModule,
    NgForOf,
    NgIf,
    MatButtonModule,
    MatDividerModule,
    MatTooltipModule,
    TaskDetailsComponent,
    NgClass,
  ],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.scss',
})
export class UserDetailsComponent implements OnInit {
  readonly dialog = inject(MatDialog);
  @Input() user!: User;
  @Output() exitEmitter = new EventEmitter<void>();
  RequestIsSend = false;
  pageSize = 10;
  currentPage = 0;
  userTasks!: UserTask[];
  availableTasks!: UserTask[];
  selectedTaskIds: string[] = [];
  taskId = '';
  showTaskDetails = false;
  userPage = 0;
  taskPage = 0;
  userMaxPage = 1;
  taskMaxPage = 1;
  constructor(
    private taskService: TaskService,
    private userService: UserService
  ) {}
  ngOnInit(): void {
    forkJoin({
      userTasks: this.taskService.GetAllUserTasks(
        this.user.userId,
        this.userPage
      ),
      availableTasks: this.taskService.GetAllAvaliableTasks(
        this.user.type,
        this.taskPage
      ),
    }).subscribe({
      next: ({ userTasks, availableTasks }) => {
        this.userTasks = userTasks.tasks;
        this.userPage = userTasks.page;
        this.userMaxPage = userTasks.maxPage;
        this.availableTasks = availableTasks.tasks;
        this.taskPage = availableTasks.page;
        this.taskMaxPage = availableTasks.maxPage;
      },
      error: (err) => console.log('Coś poszło nie tak'),
    });
  }
  onSelectionChange(selected: MatListOption[]) {
    this.selectedTaskIds = selected.map((opt) => opt.value);
  }
  @HostListener('document:keydown.escape', ['$event'])
  onEscPressed(event: KeyboardEvent) {
    if (!this.showTaskDetails && this.dialog.openDialogs.length <= 0)
      this.CloseEvent();
  }
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: BeforeUnloadEvent): void {
    if (!this.canDeactivate()) {
      $event.preventDefault();
      $event.returnValue = '';
    }
  }
  CloseEvent() {
    if (this.canDeactivate()) {
      this.exitEmitter.emit();
    }
  }
  canDeactivate(): boolean {
    if (this.selectedTaskIds.length > 0) {
      return confirm(
        'Ta strona prosi o potwierdzenie decyzji jej opuszczenia — wprowadzone informacje mogą nie zostać zapisane.'
      );
    }
    return true;
  }
  onDetailsClick(taskId: string, event: MouseEvent): void {
    event.stopPropagation();
    this.taskId = taskId;
    setTimeout(() => (this.showTaskDetails = true));
  }
  closeDetails() {
    this.showTaskDetails = false;
  }
  nextPage(isUser: boolean): void {
    this.selectedTaskIds = [];
    if (isUser) {
      this.userPage++;
      this.GetUserTasks();
    } else {
      this.taskPage++;
      this.GetAvaliableTasks();
    }
  }
  previousPage(isUser: boolean): void {
    this.selectedTaskIds = [];
    if (isUser) {
      this.userPage--;
      this.GetUserTasks();
    } else {
      this.taskPage--;
      this.GetAvaliableTasks();
    }
  }
  GetUserTasks() {
    this.taskService
      .GetAllUserTasks(this.user.userId, this.userPage)
      .subscribe({
        next: (result) => {
          this.userTasks = result.tasks;
          this.userPage = result.page;
          this.userMaxPage = result.maxPage;
        },
      });
  }
  GetAvaliableTasks() {
    this.taskService
      .GetAllAvaliableTasks(this.user.type, this.taskPage)
      .subscribe({
        next: (result) => {
          this.availableTasks = result.tasks;
          this.taskPage = result.page;
          this.taskMaxPage = result.maxPage;
        },
      });
  }
  getDifficultyClass(difficult: number): string {
    switch (difficult) {
      case 1:
        return 'diff-easy';
      case 2:
        return 'diff-light';
      case 3:
        return 'diff-medium';
      case 4:
        return 'diff-hard';
      case 5:
        return 'diff-extreme';
      default:
        return 'diff-easy';
    }
  }
  getTaskTypeShort(type: string): string {
    return GetTaskTypeShort(type);
  }
  openDialog(message: string) {
    this.dialog.open(ErrorModalComponent, { data: message });
  }

  OnSubmit() {
    this.RequestIsSend = true;
    if (this.selectedTaskIds.length <= 0) {
      if (
        confirm(
          'Nie dokonałeś żadnej zmiany.\nJeśli potwierdzasz formularz się zamknię.'
        )
      ) {
        this.CloseEvent();
      } else {
        this.RequestIsSend = false;
      }
    } else {
      if (
        this.selectedTaskIds.length + this.userTasks.length < 5 ||
        this.selectedTaskIds.length + this.userTasks.length > 11
      ) {
        this.openDialog(
          'Nieprawidłowa liczba zadań. Użytkownik powinien mieć przypisanych co najmniej 5, a maksymalnie 11 zadań.'
        );
        this.RequestIsSend = false;
        return;
      }
      const request: UserTaskAssignmentRequest = {
        userId: this.user.userId,
        tasksId: this.selectedTaskIds,
      };
      this.userService.AssignTaskToUser(request).subscribe({
        next: () => {
          this.userPage = 0;
          this.taskPage = 0;
          this.GetAvaliableTasks();
          this.GetUserTasks();
          this.RequestIsSend = false;
          this.selectedTaskIds = [];
        },
        error: (err) => {
          if (err.error == null) {
            this.openDialog('Coś poszło nie tak.\nSpróbuj ponownie później');
            this.RequestIsSend = false;
            return;
          }
          if (err.error.includes('Task_Already_Assigned')) {
            this.openDialog(
              'Wybrane zadania są już przypisane do innego użytkownika'
            );
            this.RequestIsSend = false;
            return;
          }
          if (err.error.includes('Invalid_User_Task_Count')) {
            this.openDialog(
              'Nieprawidłowa liczba zadań.\nUżytkownik powinien mieć przypisanych co najmniej 5 i maksymalnie 11 zadań.'
            );
            this.RequestIsSend = false;
            return;
          }
          if (err.error.includes('Invalid_Hard_Tasks_Percentage')) {
            this.openDialog(
              'Nieprawidłowy udział trudnych zadań.\nDozwolony zakres to od 10% do 30% zadań o trudności 4–5.'
            );
            this.RequestIsSend = false;
            return;
          }
          if (err.error.includes('Too_Many_Easy_Tasks')) {
            this.openDialog(
              'Zbyt wiele zadań o niskiej trudności.\nMaksymalnie 50% przypisanych zadań może mieć poziom trudności 1–2.'
            );
            this.RequestIsSend = false;
            return;
          }
          this.openDialog('Coś poszło nie tak.\nSpróbuj ponownie później');
          this.RequestIsSend = false;
        },
      });
    }
  }
}
