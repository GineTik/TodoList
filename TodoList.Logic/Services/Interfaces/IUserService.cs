using TodoList.Core.DTOs;
using TodoList.Core.Entities;

namespace TodoList.Logic.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> TryLoginAsync(UserDTO dto);
        Task<User?> TryRegistrationAsync(UserDTO dto);
    }
}
