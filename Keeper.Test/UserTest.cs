//using Bibliotekaen.Sql;
//using FluentAssertions;
//using Row.Common1.Dto1;
//using RowAuth.Client;

//namespace RowAuth.Test
//{
//    [TestClass]
//    public class UserTest
//    {
//        List<int> CreatedUsers = new List<int>();
//        ISqlFactory m_sql;

//        [TestInitialize]
//        public void Init()
//        {
//            //begin transaction
//        }

//        [TestCleanup]
//        public void Cleanup()
//        {
//            if (m_sql != null)
//            {
//                if (CreatedUsers.Any())
//                {
//                    var userRoleDelete = $"delete from [row].[UserRoleAccess] WHERE UserId in ({string.Join(",", CreatedUsers)})";
//                    m_sql.Query(userRoleDelete);
//                    Console.WriteLine(userRoleDelete);
//                    var userDelete = $"delete from [row].[User] WHERE Id in ({string.Join(",", CreatedUsers)})";
//                    m_sql.Query(userDelete);
//                    Console.WriteLine(userDelete);
//                }
//            }
//        }

//        public void InitCleaner(ISqlFactory sql)
//        {
//            m_sql = sql;
//        }

//        public void AddUserForCleaner(int id)
//        {
//            CreatedUsers.Add(id);
//        }

//        //[TestMethod]
//        //public void TestMethodUserCreate()
//        //{
//        //    var baseUrl = "http://localhost:8525";
//        //    var application = new WebApplicationFactory<Program>();

//        //    var starter = new Starter();

//        //    application.ClientOptions.BaseAddress = new Uri(baseUrl);

//        //    var client = new AuthApiClient(baseUrl);
//        //    client.InitHttp(application.CreateClient());

//        //    var create = Context.DefaultUser($"testuser_{DateTime.Now.ToString("ddMMyyyy")}_{Guid.NewGuid().ToString().Substring(1, 5)}", $"testuser_{DateTime.Now.ToString("ddMMyyyy")}");

//        //    var result = create.ExecTest(client);
//        //    result.Id.Should().BeGreaterThan(0);
//        //    //api/user/get/1
//        //}

//        //

//        [TestMethod]
//        public void UserCreate_test()
//        {
//            using (var server = new Starter().TestLogin())
//            {
//                InitCleaner(server.Sql);


//                var create = Context.DefaultUser($"testuser_{DateTime.Now.ToString("ddMMyyyy")}_{Guid.NewGuid().ToString().Substring(1, 5)}", $"testuser_{DateTime.Now.ToString("ddMMyyyy")}");
//                var loadFileUrl = server.LoadFile(@"RowAuth.Test.Files.22222222.jpg", "22222222.jpg");
//                create.ImageUrl = loadFileUrl;

//                var now = DateTime.Now;

//                var result = create.ExecTest(server.Client);
//                result.Id.Should().BeGreaterThan(0);

//                result.Name.Should().Be(create.Name);
//                result.SurName.Should().Be(create.SurName);
//                result.PatronicName.Should().Be(create.PatronicName);

//                result.Login.Should().Be(create.Phone);
//                result.ImageUrl.Should().NotBeNull();

//                result.Inn.Should().Be(create.Inn);
//                result.Phone.Should().Be(create.Phone);
//                result.Email.Should().Be(create.Email);
//                result.PassportNumber.Should().Be(create.PassportNumber);
//                result.Address.Should().Be(create.Address);

//                result.CreateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
//                result.UpdateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));

//                result.Roles.Should().HaveCount(1);
//                result.Roles[0].RoleId.Should().Be(UserRoles.ActivatedUser);

//                AddUserForCleaner(result.Id);
//            }
//        }

//        [TestMethod]
//        public void UserUpdate_test()
//        {
//            using (var server = new Starter().TestLogin())
//            {
//                InitCleaner(server.Sql);

//                var create = Context.DefaultUser($"testuser_{DateTime.Now.ToString("ddMMyyyy")}_{Guid.NewGuid().ToString().Substring(1, 5)}", $"testuser_{DateTime.Now.ToString("ddMMyyyy")}");
//                var loadFileUrl = server.LoadFile(@"RowAuth.Test.Files.22222222.jpg", "22222222.jpg");
//                create.ImageUrl = loadFileUrl;

//                var result = create.ExecTest(server.Client);
//                var image = result.ImageUrl;

//                var now = DateTime.Now;
//                var update = new User.Update()
//                {
//                    Name = "Updated_Name",
//                    SurName = "Updated_SName",
//                    PatronicName = "Updated_PName",
//                    Inn = "up-1232456",
//                    Phone = "98989898",
//                    Email = "po-update@mail.ru",
//                    PassportNumber = "44444",
//                    Address = "Amerika"
//                };
//                update.Id = result.Id;
//                update.ImageUrl = image;

//                result = update.ExecTest(server.Client);

//                result.Id.Should().Be(update.Id);
//                result.Name.Should().Be(update.Name);
//                result.SurName.Should().Be(update.SurName);
//                result.PatronicName.Should().Be(update.PatronicName);

//                var indOrigin = image.IndexOf("?SignKey");
//                var indUpdate = result.ImageUrl.IndexOf("?SignKey");
//                var t = result.ImageUrl.Substring(0, indUpdate);
//                var t1 = t.LastIndexOf("\\");
//                var prefix = t.Substring(0, t1);

//                result.ImageUrl.Substring(0, indUpdate).Should().Be(image.Substring(0, indOrigin));
//                result.Inn.Should().Be(update.Inn);
//                result.Phone.Should().Be(update.Phone);
//                result.Email.Should().Be(update.Email);
//                result.PassportNumber.Should().Be(update.PassportNumber);
//                result.Address.Should().Be(update.Address);

//                result.CreateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
//                result.UpdateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));

//                result.Roles.Should().HaveCount(1);
//                result.Roles[0].RoleId.Should().Be(UserRoles.ActivatedUser);

//                loadFileUrl = server.LoadFile(@"RowAuth.Test.Files.22222222.jpg", "1111111.jpg");
//                update.ImageUrl = loadFileUrl;
//                result = update.ExecTest(server.Client);
//                result.ImageUrl.Should().NotBeNullOrEmpty();
//                result.ImageUrl.Substring(0, indUpdate).Should().NotBe(image.Substring(0, indOrigin));
//                result.ImageUrl.Should().StartWith(prefix);

//                AddUserForCleaner(result.Id);
//            }
//        }

//        [TestMethod]
//        public void UserSetRole_test()
//        {
//            using (var server = new Starter().TestLogin())
//            {
//                InitCleaner(server.Sql);

//                var create = Context.DefaultUser($"testuser_{DateTime.Now.ToString("ddMMyyyy")}_{Guid.NewGuid().ToString().Substring(1, 5)}", $"testuser_{DateTime.Now.ToString("ddMMyyyy")}");
//                var result = create.ExecTest(server.Client);

//                var set = new User.UserRoleAccess.Set();
//                set.UserId = result.Id;
//                set.RoleId = UserRoles.Admin;
//                set.ExecTest(server.Client);

//                result = User.GetInTest(result.Id, server.Client);

//                result.Roles.Should().HaveCount(2);
//                result.Roles.Select(x => x.RoleId).ToList().Should().BeEquivalentTo(new List<UserRoles>() { UserRoles.ActivatedUser, UserRoles.Admin });

//                var remove = new User.UserRoleAccess.Remove();
//                remove.UserId = result.Id;
//                remove.RoleId = UserRoles.Admin;
//                remove.ExecTest(server.Client);

//                result = User.GetInTest(result.Id, server.Client);

//                result.Roles.Should().HaveCount(1);
//                result.Roles[0].RoleId.Should().Be(UserRoles.ActivatedUser);

//                AddUserForCleaner(result.Id);
//            }
//        }
//    }
//}