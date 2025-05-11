export interface UserTask {
  taskId: string;
  status: number;
  type: string;
  difficult: number;
  title: string;
}
export interface TaskDetails {
  taskId: string;
  status: number;
  difficult: number;
  title: string;
  type: string;
  deadline: Date;
  deploymentScope: string;
  taskDescription: string;
  serviceList: string;
  serverList: string;
}
export interface PagginationTaskListDTO {
  page: number;
  maxPage: number;
  tasks: TaskDetails[];
}
export interface UserTaskAssignmentRequest {
  userId: string;
  tasksId: string[];
}
export function GetStatusName(status: number): string {
  switch (status) {
    case 0:
      return 'Zrobione';
    case 1:
      return 'Do zrobienia';
    default:
      return 'Nieznany Status';
  }
}
export function GetTaskTypeShort(type: string): string {
  switch (type) {
    case 'DeploymentTask':
      return 'WDRO';
    case 'MaintenanceTask':
      return 'MAIN';
    case 'ImplementationTask':
      return 'IMPL';
    default:
      return 'NWN';
  }
}
