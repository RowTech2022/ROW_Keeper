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
                FullName = "Tommy Shelby - test",
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

        public static string GeneratePhone()
        {
            return "992" +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +

                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +

                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +

                   new Random().Next(0, 10) +
                   new Random().Next(0, 10);
        }

        public static string GenerateEmail()
        {
            return "test" +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   "@mail.com";
        }
        
        public static string GenerateUPC()
        {
            return "" +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10) +
                   new Random().Next(0, 10);
        }
    }
}
