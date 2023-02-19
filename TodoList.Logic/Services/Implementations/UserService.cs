using TodoList.Core.DTOs;
using TodoList.Core.Entities;
using TodoList.Logic.AuthenticationLogics;
using TodoList.Logic.Services.Interfaces;

namespace TodoList.Logic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IAuthenticator _authenticator;

        public UserService(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public User? Login(UserDTO dto)
        {
            return _authenticator.Login(new User { Login = dto.Login }, dto.Password);
        }

        public User? Registration(UserDTO dto)
        {
            return _authenticator.Registration(new User { Login = dto.Login }, dto.Password);
        }
    }
}
