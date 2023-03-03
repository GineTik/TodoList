using TodoList.Core.Entities;

namespace TodoList.Logic.AuthenticationLogics
{
    public interface IAuthenticator
    {
        User? TryLogin(User user, string password);
        User? TryRegistration(User user, string password);
    }
}
