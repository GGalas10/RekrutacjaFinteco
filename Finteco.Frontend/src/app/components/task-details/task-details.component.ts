import {
  Component,
  EventEmitter,
  HostListener,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { TaskDetails, GetStatusName } from '../../models/Tasks';
import { TaskService } from '../../services/task.service';
import { DatePipe, NgIf, NgSwitch, NgSwitchCase } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-task-details',
  imports: [NgIf, MatButtonModule, NgSwitch, DatePipe, NgSwitchCase],
  templateUrl: './task-details.component.html',
  styleUrl: './task-details.component.scss',
})
export class TaskDetailsComponent implements OnInit {
  @Input() taskId!: string;
  @Output() exitEmitter = new EventEmitter<void>();
  task!: TaskDetails;
  constructor(private taskService: TaskService) {}
  ngOnInit(): void {
    this.taskService.GetTaskDetails(this.taskId).subscribe({
      next: (value) => (this.task = value),
      error: (err) => alert('Coś poszło nie tak'),
    });
  }
  GetStatusName(status: number): string {
    var newStatus = GetStatusName(status);
    return newStatus;
  }

  @HostListener('document:keydown.escape', ['$event'])
  onEscPressed(event: KeyboardEvent) {
    this.exitEmitter.emit();
  }
}
