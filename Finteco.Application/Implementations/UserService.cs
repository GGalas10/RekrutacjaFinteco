using Finteco.Application.Contracts;
using Finteco.Application.DTOs;
using Finteco.DataAccess.Databases;
using Microsoft.EntityFrameworkCore;

namespace Finteco.Application.Implementations
{ 
    public class UserService : IUserService
    {
        private readonly UserDbContext _userDbContext;
        public UserService(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public async Task<List<UserListDTO>> GetAllUsers()
        {
            var users = await _userDbContext.Users.ToListAsync();
            return users.Select(x=>UserListDTO.GetFromModel(x)).ToList();
        }
    }
}
