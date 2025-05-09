using Finteco.Application.DTOs;
using Finteco_Core.Enums;

namespace Finteco.Application.Contracts
{
    public interface ITaskService
    {
        Task<List<TaskListDTO>> GetAllTasksToAssignByUserType(UserTypeEnum type);
    }
}
