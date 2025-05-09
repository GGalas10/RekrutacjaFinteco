using Finteco_Core.Domain.Users;
using Finteco_Core.Enums;

namespace Finteco.Application.DTOs
{
    public class UserListDTO
    {
        public Guid userId {  get; set; }
        public string name { get; set; }
        public UserTypeEnum type { get; set; }
        public static UserListDTO GetFromModel(User user)
        {
            return new UserListDTO()
            {
                userId = user.Id,
                name = user.Name,
                type = user.UserType,
            };
        }
    }
}
