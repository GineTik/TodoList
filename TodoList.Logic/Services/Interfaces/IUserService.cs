using TodoList.Core.DTOs;
using TodoList.Core.Entities;

namespace TodoList.Logic.Services.Interfaces
{
    public interface IUserService
    {
        User? TryLogin(UserDTO dto);
        User? TryRegistration(UserDTO dto);
    }
}
