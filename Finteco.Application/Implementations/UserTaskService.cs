using Finteco.Application.Commands;
using Finteco.Application.Contracts;
using Finteco.Application.DTOs;
using Finteco.DataAccess.Databases;
using Finteco_Core.Domain.Tasks;
using Finteco_Core.Exceptions;
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
        public async Task<PagginationTaskListDTO> GetAllUserTasks(Guid userId, int page)
        {
            var userTasks = await _dataDbContext.Tasks.Where(x => x.UserId == userId).ToListAsync();
            List<TaskListDTO> tasks = userTasks.Select(TaskListDTO.GetFromBaseTask).ToList();
            var result = new PagginationTaskListDTO()
            {
                maxPage = (int)Math.Ceiling((double)tasks.Count() / 10) == 0 ? 1 : (int)Math.Ceiling((double)tasks.Count() / 10),
                page = page,
                tasks = tasks.OrderByDescending(x => x.difficult).Skip(page * 10).Take(10).ToList()
            };
            return result;
        }
        public async Task AssignTasksToUser(AssignTasksCommand command)
        {
            if (command.userId == Guid.Empty || command.userId == null)
                throw new BadRequestException("UserId_Cannot_Be_Null_Or_Empty");
            var newTasks = await _dataDbContext.Tasks.Where(x => command.tasksId.Contains(x.Id)).ToListAsync();
            var userTasks = await _dataDbContext.Tasks.Where(x => x.UserId == command.userId).ToListAsync();
            #region Validations

            if (newTasks.Any(x=>x.UserId != null))
                throw new BadRequestException("Task_Already_Assigned");
            
            if (userTasks.Count() + newTasks.Count() < 5 || userTasks.Count() + newTasks.Count() > 11)
                throw new BadRequestException("Invalid_User_Task_Count");


            List<BaseTask> totalTasks = newTasks.Concat(userTasks).ToList();

            int hardTasks = totalTasks.Count(t => t.Difficult >= 4);
            int easyTasks = totalTasks.Count(t => t.Difficult <= 2);

            double hardTasksPercentage = (double)hardTasks / totalTasks.Count() * 100;
            double easyTasksPercentage = (double)easyTasks / totalTasks.Count() * 100;

            if (hardTasksPercentage < 10 || hardTasksPercentage > 30)
                throw new BadRequestException("Invalid_Hard_Tasks_Percentage");

            if (easyTasksPercentage > 50)
                throw new BadRequestException("Too_Many_Easy_Tasks");
            #endregion
            foreach(var oneTask in newTasks)
            {
                oneTask.AssignUser(command.userId);
                _dataDbContext.Update(oneTask);
            }
            await _dataDbContext.SaveChangesAsync();
        }

    }
}
