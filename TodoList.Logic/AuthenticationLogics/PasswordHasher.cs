namespace TodoList.Logic.AuthenticationLogics
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return password + "_hashed";
        }

        public static bool ValidateHash(string hash, string password)
        {
            return hash == HashPassword(password);
        }
    }
}
