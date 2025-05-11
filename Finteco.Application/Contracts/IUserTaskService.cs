using Finteco.Application.Commands;
using Finteco.Application.DTOs;

namespace Finteco.Application.Contracts
{
    public interface IUserTaskService
    {
        Task<PagginationTaskListDTO> GetAllUserTasks(Guid userId, int page);
        Task AssignTasksToUser(AssignTasksCommand command);
    }
}
