using Finteco_Core.Domain.Tasks;
using Finteco_Core.Enums;

namespace Finteco.Application.DTOs
{
    public class TaskListDTO
    {
        public Guid taskId { get; set; }
        public TypeEnum type { get; private set; }
        public StatusEnum status { get; private set; }
        public int difficult { get; private set; }
        public string title { get; private set; }
        public static TaskListDTO GetFromBaseTask(BaseTask task)
        {
            return new TaskListDTO()
            {
                taskId = task.Id,
                type = task.Type,
                status = task.Status,
                difficult = task.Difficult,
                title = task.Title,
            };
        }
    }
}
