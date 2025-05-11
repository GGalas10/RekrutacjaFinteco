using Finteco.Application.Contracts;
using Finteco.Application.DTOs;
using Finteco.DataAccess.Databases;
using Finteco_Core.Domain.Tasks;
using Finteco_Core.Enums;
using Finteco_Core.Exceptions;
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
        public async Task<PagginationTaskListDTO> GetAllTasksToAssignByUserType(UserTypeEnum type, int page)
        {
            if(type == UserTypeEnum.Programmer)
            {
                var onlyImplementationTasks = await _dataDbContext.Tasks.Where(x => x.UserId == null && x is ImplementationTask).ToListAsync();
                List<TaskListDTO> tasksOnly = onlyImplementationTasks.Select(TaskListDTO.GetFromBaseTask).ToList();
                var onlyImplementationresult = new PagginationTaskListDTO()
                {
                    maxPage = ((int)Math.Ceiling((double)onlyImplementationTasks.Count() / 10)) == 0 ? 1 : (int)Math.Ceiling((double)onlyImplementationTasks.Count() / 10),
                    page = page,
                    tasks = tasksOnly.OrderByDescending(x => x.difficult).Skip(page*10).Take(10).ToList()
                };
                return onlyImplementationresult;
            }

            var allTasks = await _dataDbContext.Tasks.Where(x => x.UserId == null).ToListAsync();
            List<TaskListDTO> tasks = allTasks.Select(TaskListDTO.GetFromBaseTask).ToList();
            var result = new PagginationTaskListDTO()
            {
                maxPage = (int)Math.Ceiling((double)tasks.Count() / 10) == 0 ? 1 : (int)Math.Ceiling((double)tasks.Count() / 10),
                page = page,
                tasks = tasks.OrderByDescending(x => x.difficult).Skip(page * 10).Take(10).ToList()
            };
            return result;
        }
        public async Task<TaskDetailsDTO> GetTaskDetails(Guid taskId)
        {
            if (taskId == Guid.Empty)
                return new TaskDetailsDTO();

            var result = await _dataDbContext.Tasks.FirstOrDefaultAsync(x=>x.Id == taskId);
            if (result == null)
                throw new BadRequestException($"Cannot_Find_Task_With_Id_{taskId}");
            return TaskDetailsDTO.GetFromModel(result);
        }
    }
}
