using Finteco.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Finteco.API.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUserTaskService _userTaskService;
        public TaskController(ITaskService taskService, IUserTaskService userTaskService)
        {
            _taskService = taskService;
            _userTaskService = userTaskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserTasks([FromBody]Guid userId)
        {
            var result = await _userTaskService.GetAllUserTasks(userId);
            return Json(result);
        }
    }
}
