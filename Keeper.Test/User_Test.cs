using Bibliotekaen.Sql;
using FluentAssertions;
using Keeper.Client;

namespace Keeper.Test
{
    [TestClass]
    public class User_Test
    {
        private readonly List<int> createdUsers = [];
        private readonly List<int> orgIds = [];
        ISqlFactory? m_sql;

        [TestInitialize]
        public void Init()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (m_sql == null) return;
            
            if (createdUsers.Count != 0)
            {
                var query = $"delete from [new-keeper].[Users] where [Id] in ({string.Join(", ", createdUsers)})";
                m_sql.Query(query);
            }
            
            if (orgIds.Count != 0)
            {
                var query = $"delete from [new-keeper].[Organizations] where [Id] in ({string.Join(", ", orgIds)})";
                m_sql.Query(query);
            }
        }

        private void InitCleaner(ISqlFactory sql)
        {
            m_sql = sql;
        }

        [TestMethod]
        public void Create()
        {
            using var server = new Starter().TestLogin();
            
            InitCleaner(server.Sql);

            var orgRequest = Context.DefaultOrganization();
            var org = orgRequest.ExecTest(server.Client);

            org.Id.Should().BeGreaterThan(0);
            orgIds.Add(org.Id);
            
            var create = new User.Create
            {
                OrgId = org.Id,
                FullName = "Name Surname - Admin",
                Phone = Context.GeneratePhone(),
                Email = Context.GenerateEmail(),
                UserType = UserType.Admin,
                Login = "userName" + new Random().Next(1, 999999),
                State = User.Status.Active,
            };

            var now = DateTime.Now;

            var result = create.ExecTest(server.Client); 

            result.Id.Should().BeGreaterThan(0);
            createdUsers.Add(result.Id);
            
            result.FullName.Should().Be(create.FullName);
            result.Phone.Should().Be(create.Phone);
            result.Email.Should().Be(create.Email);
            result.Login.Should().Be(create.Login);
            result.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
            result.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
            result.Timestamp.Should().NotBeEmpty();

            var password = server.Settings.SendSmsMock.GetLastPassword(result.Login);
            password.Should().NotBeNullOrWhiteSpace();

            var token = new Login
            {
                UserLogin = result.Login,
                Password = password
            }.ExecTest(server.Client);

            token.Should().NotBeNull();
            token.AccessToken.Should().NotBeNullOrWhiteSpace();
            token.RefreshToken.Should().NotBeNullOrWhiteSpace();
            token.ExpireTime.Should().NotBeNull();
        }

        [TestMethod]
        public void Update()
        {
            using var server = new Starter().TestLogin();
            
            InitCleaner(server.Sql);

            var orgRequest = Context.DefaultOrganization();
            var org = orgRequest.ExecTest(server.Client);

            org.Id.Should().BeGreaterThan(0);
            orgIds.Add(org.Id);
            
            var userRequest = new User.Create
            {
                OrgId = org.Id,
                FullName = "Name Surname - Admin",
                Phone = Context.GeneratePhone(),
                Email = Context.GenerateEmail(),
                UserType = UserType.Admin,
                Login = "userName" + new Random().Next(1, 999999),
                State = User.Status.Active
            };

            var now = DateTime.Now;

            var userResult = userRequest.ExecTest(server.Client); 

            userResult.Id.Should().BeGreaterThan(0);
            createdUsers.Add(userResult.Id);

            var request = new User.Update
            {
                Id = userResult.Id,
                FullName = "Upd_" + userResult.FullName,
                Email = userResult.Email + 2,
                Login = userResult.Login + 2,
                Phone = userResult.Phone,
                OrgId = userRequest.OrgId,
                UserType = UserType.Cashier
            };

            var result = request.ExecTest(server.Client);

            result.Id.Should().Be(request.Id);
            result.FullName.Should().Be(request.FullName);
            result.Email.Should().Be(request.Email);
            result.Login.Should().Be(request.Login);
            result.Phone.Should().Be(request.Phone);
            result.UserType.Should().Be(request.UserType);
            result.CreatedAt.Should().Be(userResult.CreatedAt);
            result.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
            result.Timestamp.Should().NotBeNullOrEmpty();
        }
    }
}