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
                Email = GenerateEmail(),
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

        public static Organization.Create DefaultOrganization()
        {
            return new Organization.Create
            {
                PlanId = 0,
                OwnerId = 0,
                OrgName = "Test organization",
                OrgPhone = "992123456789",
                OrgAddress = "Dushanbe, Rudaky 17",
                OrgEmail = "test@mail.com",
                OwnerFullName = "Test Testov",
                OwnerEmail = "test@mail.com",
                OwnerPhone = "992123456789",
                Status = Organization.OrgStatus.Acitve
            };
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
