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

        public User? TryLogin(UserDTO dto)
        {
            return _authenticator.TryLogin(new User { Login = dto.Login }, dto.Password);
        }

        public User? TryRegistration(UserDTO dto)
        {
            return _authenticator.TryRegistration(new User { Login = dto.Login }, dto.Password);
        }
    }
}
