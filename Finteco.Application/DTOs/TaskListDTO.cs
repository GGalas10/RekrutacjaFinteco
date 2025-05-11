using Finteco_Core.Domain.Tasks;
using Finteco_Core.Enums;

namespace Finteco.Application.DTOs
{
    public class TaskListDTO
    {
        public Guid taskId { get; set; }
        public StatusEnum status { get; set; }
        public string type { get; set; }
        public int difficult { get; set; }
        public string title { get; set; }
        public static TaskListDTO GetFromBaseTask(BaseTask task)
        {
            return new TaskListDTO()
            {
                taskId = task.Id,
                status = task.Status,
                difficult = task.Difficult,
                title = task.Title,
                type = task.GetType().Name
            };
        }
    }
}
