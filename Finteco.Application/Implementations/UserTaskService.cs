using Finteco.Application.Contracts;
using Finteco.Application.DTOs;
using Finteco.DataAccess.Databases;
using Microsoft.EntityFrameworkCore;

namespace Finteco.Application.Implementations
{
    public class UserTaskService : IUserTaskService
    {
        private readonly DataDbContext _dataDbContext;
        public UserTaskService(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }
        public async Task<List<TaskListDTO>> GetAllUserTasks(Guid userId)
        {
            var deploymentTasks = await _dataDbContext.DeploymentTasks.Where(x => x.UserId == userId).ToListAsync();
            var mainteanceTasks = await _dataDbContext.MaintenanceTasks.Where(x => x.UserId == userId).ToListAsync();
            var implementationTasks = await _dataDbContext.ImplementationTasks.Where(x => x.UserId == userId).ToListAsync();
            List<TaskListDTO> tasks =
            [
                .. deploymentTasks.Select(TaskListDTO.GetFromBaseTask),
                .. mainteanceTasks.Select(TaskListDTO.GetFromBaseTask),
                .. implementationTasks.Select(TaskListDTO.GetFromBaseTask),
            ];
            return tasks.OrderByDescending(x=>x.difficult).Take(10).ToList();
        }
        public async Task AssignTasksToUser(Guid userId)
        {

        }

    }
}
