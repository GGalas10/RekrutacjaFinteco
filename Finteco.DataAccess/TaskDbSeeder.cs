using Bogus;
using Finteco.DataAccess.Databases;
using Finteco_Core.Domain.Tasks;

namespace Finteco.DataAccess
{
    public class TaskDbSeeder
    {
        public static void SeedUsers(DataDbContext context)
        {
            List<Guid> programmersId = new List<Guid>() { Guid.Parse("7d24c8b3-2e38-4f9f-b5a6-62e1bc7cc77d"),Guid.Parse("4c7b931e-0f20-4a6f-a2e1-fad6d7a71f1c") }; 
            List<Guid> devopsIds = new List<Guid>() { Guid.Parse("65bc9174-9b8e-4194-9d5d-994cb158e0a3"),Guid.Parse("f1b17434-03f1-404b-8e30-b350053c91ae") }; 
            var faker = new Faker();
            List<BaseTask> tasks = new List<BaseTask>();
            for(int i = 0; i < 25; i++)
            {
                var deployScope = string.Join("\nWdrożenie: ", faker.Lorem.Words(faker.Random.Int(5, 5)));

                var implementationDescription = string.Join("\nImplementacja: ", faker.Lorem.Words(faker.Random.Int(5, 50)));

                var serviceList = string.Join("\nSerwisy:", faker.Lorem.Words(faker.Random.Int(5, 35)));
                var serverList = string.Join("\nSerwery: ", faker.Lorem.Words(faker.Random.Int(5, 35)));
                tasks.Add(new DeploymentTask(
                        DateTime.Now.AddDays(new Random().Next(1, 35)),
                        deployScope.Length > 399 ? deployScope.Substring(0, 399) : deployScope, 
                        new Random().Next(1, 6), 
                        $"Zadanie {new Random().Next(1, 150).ToString()}"
                        )
                    );
                tasks.Add(new ImplementationTask(
                        implementationDescription.Length > 999 ? implementationDescription.Substring(0, 999) : implementationDescription,
                        new Random().Next(1, 6),
                        $"Zadanie {new Random().Next(1, 150).ToString()}"
                        )
                    );
                tasks.Add(new MaintenanceTask(
                        DateTime.Now.AddDays(new Random().Next(1, 35)),
                        serviceList.Length > 399 ? serviceList.Substring(0, 399) : serviceList,
                        serverList.Length > 399 ? serverList.Substring(0, 399) : serverList,
                        new Random().Next(1, 6),
                        $"Zadanie {new Random().Next(1, 150).ToString()}")
                    );
            }
            var implementationTasks = tasks.OfType<ImplementationTask>().ToList();
            for (int i = 0; i <= 4; i++)
            {
                var implementationTaskWithoutUser = implementationTasks.Where(x => x.UserId == null).ToList();
                implementationTaskWithoutUser[new Random().Next(0, implementationTaskWithoutUser.Count()-1)].AssignUser(programmersId[new Random().Next(0, 2)]);
            }
            for (int i = 0; i <= 4; i++)
            {
                var tasksWithoutUsers = tasks.Where(x => x.UserId == null).ToList();
                tasksWithoutUsers[new Random().Next(0, tasksWithoutUsers.Count()-1)].AssignUser(devopsIds[new Random().Next(0, 2)]);
            }
            context.Tasks.AddRange(tasks);
            context.SaveChanges();
        }
    }
}
