using Keeper.Client;

namespace Keeper.Test
{
    public class Context
    {
        public const string TestLogin = "admin";
        public const string TestPassword = "12345";

        public static User.Create DefaultUser()
        {
            var result = new User.Create
            {
                Name = "Tommy - test",
                Surname = "Shelby - test",
                Email = "Email - TEST",
                Phone = "992" +
                        new Random().Next(0, 9) +
                        new Random().Next(0, 9) +

                        new Random().Next(0, 9) +
                        new Random().Next(0, 9) +
                        new Random().Next(0, 9) +

                        new Random().Next(0, 9) +
                        new Random().Next(0, 9) +

                        new Random().Next(0, 9) +
                        new Random().Next(0, 9),
                Login = "testLogin" + new Random().Next(1, 99999),
                UserType = UserType.Manager
            };

            return result;

        }
    }
}
