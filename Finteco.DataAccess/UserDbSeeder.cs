using Bogus;
using Finteco.DataAccess.Databases;
using Finteco_Core.Domain.Users;
using Finteco_Core.Enums;

namespace Finteco.DataAccess
{
    public static class UserDbSeeder
    {
        public static void SeedUsers(UserDbContext context)
        {
            var faker = new Faker();
            context.Users.AddRange(
                new User(Guid.Parse("7d24c8b3-2e38-4f9f-b5a6-62e1bc7cc77d"),faker.Name.FirstName(), UserTypeEnum.Programmer),
                new User(faker.Name.FirstName(), UserTypeEnum.Programmer),
                new User(Guid.Parse("4c7b931e-0f20-4a6f-a2e1-fad6d7a71f1c"),faker.Name.FirstName(), UserTypeEnum.Programmer),
                new User(faker.Name.FirstName(), UserTypeEnum.Programmer),
                new User(faker.Name.FirstName(), UserTypeEnum.Programmer),
                new User(Guid.Parse("65bc9174-9b8e-4194-9d5d-994cb158e0a3"),faker.Name.FirstName(), UserTypeEnum.Devops),
                new User(faker.Name.FirstName(), UserTypeEnum.Devops),
                new User(Guid.Parse("f1b17434-03f1-404b-8e30-b350053c91ae"),faker.Name.FirstName(), UserTypeEnum.Devops),
                new User(faker.Name.FirstName(), UserTypeEnum.Devops),
                new User(faker.Name.FirstName(), UserTypeEnum.Devops)
                );
            context.SaveChanges();
        }
    }
}
