using TodoList.Core.Entities;

namespace TodoList.Logic.AuthenticationLogics
{
    public interface IAuthenticator
    {
        User? Login(User user, string password);
        User? Registration(User user, string password);
    }
}
