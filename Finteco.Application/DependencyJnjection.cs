using Finteco.Application.Contracts;
using Finteco.Application.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Finteco.Application
{
    public static class DependencyJnjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IUserTaskService, UserTaskService>();
            service.AddScoped<ITaskService, TaskService>();
            return service;
        } 
    }
}
