using TodoList.Core.Entities;
using TodoList.Data.EF;

namespace TodoList.Logic.AuthenticationLogics
{
    public class Authenticator : IAuthenticator
    {
        private readonly DataContext _context;

        public Authenticator(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> TryLogin(User user, string password)
        {
            return await Task.Run(() =>
            {
                var hash = PasswordHasher.HashPassword(password);
                var loginedUser = _context.Users.FirstOrDefault(u =>
                    u.Login == user.Login &&
                    u.Password == hash);

                return loginedUser?.Banned == false ? loginedUser : null;
            });
        }

        public async Task<User?> TryRegistration(User user, string password)
        {
            var findedUser = _context.Users.FirstOrDefault(u =>
                u.Login == user.Login);

            if (findedUser != null)
                return null;

            user.RoleId = _context.Roles.First(r => r.Name == "User").Id;
            user.Password = PasswordHasher.HashPassword(password);
            user.DateOfRegistrationUTC = DateTime.UtcNow;
            user.Banned = false;

            var registeredUser = _context.Users.Add(user).Entity;
            await _context.SaveChangesAsync();

            return registeredUser;
        }
    }
}
