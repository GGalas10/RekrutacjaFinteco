using Finteco.Application.DTOs;

namespace Finteco.Application.Contracts
{
    public interface IUserService
    {
        Task<List<UserListDTO>> GetAllUsers();
    }
}
