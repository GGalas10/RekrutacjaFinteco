using Finteco_Core.Domain.Tasks;
using Finteco_Core.Enums;

namespace Finteco.Application.DTOs
{
    public class TaskDetailsDTO
    {
        public Guid taskId { get; set; }
        public StatusEnum status { get; set; }
        public int difficult { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public DateTime deadline { get; set; }
        public string deploymentScope { get; set; }
        public string taskDescription { get; set; }
        public string serviceList { get; set; }
        public string serverList { get; set; }
        public static TaskDetailsDTO GetFromModel(BaseTask model)
        {
            var taskDetailsDTO = new TaskDetailsDTO()
            {
                taskId = model.Id,
                status = model.Status,
                difficult = model.Difficult,
                title = model.Title,
            };
            switch (model.GetType().Name)
            {
                case "ImplementationTask":
                    taskDetailsDTO.taskDescription = (model as ImplementationTask).TaskDescription;
                    taskDetailsDTO.type = "Implementacja";
                    break;
                case "DeploymentTask":
                    taskDetailsDTO.deadline = (model as DeploymentTask).Deadline;
                    taskDetailsDTO.deploymentScope = (model as DeploymentTask).DeploymentScope;
                    taskDetailsDTO.type = "Wdrożenie";
                    break;
                case "MaintenanceTask":
                    taskDetailsDTO.deadline = (model as MaintenanceTask).Deadline;
                    taskDetailsDTO.serviceList = (model as MaintenanceTask).ServiceList;
                    taskDetailsDTO.serverList = (model as MaintenanceTask).ServerList;
                    taskDetailsDTO.type = "Maintanance";
                    break;
                default:
                    taskDetailsDTO.type = "Nieznany typ zadania";
                    break;
            }
            return taskDetailsDTO;
        }
    }
}
