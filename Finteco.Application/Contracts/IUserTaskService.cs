using Finteco.Application.DTOs;

namespace Finteco.Application.Contracts
{
    public interface IUserTaskService
    {
        Task<List<TaskListDTO>> GetAllUserTasks(Guid userId);
    }
}
