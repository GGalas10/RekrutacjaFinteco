using Finteco.DataAccess.Databases;
using Finteco_Core.Domain.Users;
using Finteco_Core.Enums;

namespace Finteco.DataAccess
{
    public static class UserDbSeeder
    {
        public static void SeedUsers(UserDbContext context)
        {
            context.Users.AddRange(
                new User("Ethan", UserTypeEnum.Programmer),
                new User("Sophia", UserTypeEnum.Programmer),
                new User("Olivia", UserTypeEnum.Programmer),
                new User("Liam", UserTypeEnum.Devops),
                new User("Ava", UserTypeEnum.Devops),
                new User("Noah", UserTypeEnum.Devops),
                new User("James", UserTypeEnum.Devops)
                );
            context.SaveChanges();
        }
    }
}
