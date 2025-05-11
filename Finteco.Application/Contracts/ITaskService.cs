using Finteco.Application.DTOs;
using Finteco_Core.Enums;

namespace Finteco.Application.Contracts
{
    public interface ITaskService
    {
        Task<PagginationTaskListDTO> GetAllTasksToAssignByUserType(UserTypeEnum type, int page);
        Task<TaskDetailsDTO> GetTaskDetails(Guid taskId);
    }
}
