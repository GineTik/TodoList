using TodoList.Core.DTOs;
using TodoList.Core.Entities;

namespace TodoList.Logic.Services.Interfaces
{
    public interface IUserService
    {
        User? Login(UserDTO dto);
        User? Registration(UserDTO dto);
    }
}
