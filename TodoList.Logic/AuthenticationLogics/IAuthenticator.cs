using TodoList.Core.Entities;

namespace TodoList.Logic.AuthenticationLogics
{
    public interface IAuthenticator
    {
        Task<User?> TryLogin(User user, string password);
        Task<User?> TryRegistration(User user, string password);
    }
}
