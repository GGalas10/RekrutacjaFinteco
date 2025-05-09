using Finteco.Application.Contracts;
using Finteco.Application.DTOs;
using Finteco.DataAccess.Databases;
using Finteco_Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Finteco.Application.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly DataDbContext _dataDbContext;
        public TaskService(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }
        public async Task<List<TaskListDTO>> GetAllTasksToAssignByUserType(UserTypeEnum type)
        {
            if(type == UserTypeEnum.Programmer)
            {
                var onlyImplementationTasks = await _dataDbContext.ImplementationTasks.Where(x => x.UserId == Guid.Empty).ToListAsync();
                return onlyImplementationTasks.Select(TaskListDTO.GetFromBaseTask).ToList();
            }
            var deploymentTasks = await _dataDbContext.DeploymentTasks.Where(x => x.UserId == Guid.Empty).ToListAsync();
            var mainteanceTasks = await _dataDbContext.MaintenanceTasks.Where(x => x.UserId == Guid.Empty).ToListAsync();
            var implementationTasks = await _dataDbContext.ImplementationTasks.Where(x => x.UserId == Guid.Empty).ToListAsync();
            List<TaskListDTO> tasks =
            [
                .. deploymentTasks.Select(TaskListDTO.GetFromBaseTask),
                .. mainteanceTasks.Select(TaskListDTO.GetFromBaseTask),
                .. implementationTasks.Select(TaskListDTO.GetFromBaseTask),
            ];
            return tasks.OrderByDescending(x => x.difficult).Take(10).ToList();
        }
    }
}
