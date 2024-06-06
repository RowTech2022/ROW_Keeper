using Bibliotekaen.Sql;
using FluentAssertions;
using Keeper.Client;

namespace Keeper.Test
{
    [TestClass]
    public class User_Test
    {
        List<int> CreatedUser = new();
        ISqlFactory m_sql;

        [TestInitialize]
        public void Init()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (m_sql != null)
            {
                if (CreatedUser.Any())
                {
                    /*logic for delet*/
                }
            }
        }

        public void InitCleaner(ISqlFactory sql)
        {
            m_sql = sql;
        }

        public void AddDataForCleaner(int id)
        {
            CreatedUser.Add(id);
        }

        private static string RandomPhone()
        {
            return "992" +
                   new Random().Next(0, 9) +
                   new Random().Next(0, 9) +

                   new Random().Next(0, 9) +
                   new Random().Next(0, 9) +
                   new Random().Next(0, 9) +

                   new Random().Next(0, 9) +
                   new Random().Next(0, 9) +

                   new Random().Next(0, 9) +
                   new Random().Next(0, 9);
        }

        [TestMethod]
        public void User_Create_Admin_Test()
        {
            using var server = new Starter();

            new User.UserCode
            {
                Login = "TestLogin"
            }.ExecTest(server.Client);
            
            var create = new User.Create
            {
                Name = "Name - Admin",
                Surname = "Surname - Admin",
                Phone = RandomPhone(),
                Email = "Email - Admin",
                UserType = UserType.Admin,
                Login = "userName" + new Random().Next(1, 999999) 
            };

            var now = DateTime.Now;

            var result = create.ExecTest(server.Client); 

            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be(create.Name);
            result.Surname.Should().Be(create.Surname);
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


        /*[TestMethod]
        public void User_Create_Test()
        {
            using (var server = new Starter().TestLogin())
            {
                InitCleaner(server.Sql);
                var create = Context.DefaultUser();

                var now = DateTime.Now;

                var result = create.ExecTest(server.Client); //need to use correct client server 

                result.Id.Should().BeGreaterThan(0);
                result.OrgName.Should().Be(create.OrgName);
                //result.Inn.Should().Be(create.Inn);
                result.OrgCreateAt.Should().Be(create.OrgCreateAt);
                result.OPF.Should().Be(create.OPF);
                result.OrgUpr.Should().Be(create.OrgUpr);
                result.Activity.Should().Be(create.Activity);
                result.OwnershipId.Should().Be(create.OwnershipId);
                result.LegalAddress.Should().Be(create.LegalAddress);
                result.OrgSite.Should().Be(create.OrgSite);
                result.Email.Should().Be(create.Email);
                result.FirstHead.Should().Be(create.FirstHead);
                result.InnFirstHead.Should().Be(create.InnFirstHead);
                result.PhoneFirstHead.Should().Be(create.PhoneFirstHead);
                result.ChiefAccountant.Should().Be(create.ChiefAccountant);
                result.InnChiefAccountant.Should().Be(create.InnChiefAccountant);
                result.PhoneChiefAccountant.Should().Be(create.PhoneChiefAccountant);
                result.SertChiefAccountant.Should().Be(create.SertChiefAccountant);
                result.Agreement.Should().Be(create.Agreement);
                result.ProfSert.Should().Be(create.ProfSert);
                result.ProfUserSert.Should().Be(create.ProfUserSert);
                result.CreateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
                result.UpdateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
                result.Timestamp.Should().NotBeEmpty();
                AddDataForCleaner(result.Id);
            }
        }

        [TestMethod]
        public void User_Create_Public_Test()
        {
            using (var server = new Starter().TestLogin())
            {
                InitCleaner(server.Sql);
                var create =
                    Context.DefaultUser(
                        $"testuser_{DateTime.Now.ToString("ddMMyyyy")}_{Guid.NewGuid().ToString().Substring(1, 5)}",
                        $"testuser_{DateTime.Now.ToString("ddMMyyyy")}");
                //create.Password = "123456";
                //create.ConfirmPassWord = "123456";

                var now = DateTime.Now;

                var result = create.ExecTestPublic(server.Client); //need to use correct client server 

                result.Id.Should().BeGreaterThan(0);
                result.OrgName.Should().Be(create.OrgName);
                result.OrgCreateAt.Should().Be(create.OrgCreateAt);
                result.OPF.Should().Be(create.OPF);
                result.OrgUpr.Should().Be(create.OrgUpr);
                result.Activity.Should().Be(create.Activity);
                result.OwnershipId.Should().Be(create.OwnershipId);
                result.LegalAddress.Should().Be(create.LegalAddress);
                result.OrgSite.Should().Be(create.OrgSite);
                result.Email.Should().Be(create.Email);
                result.FirstHead.Should().Be(create.FirstHead);
                result.InnFirstHead.Should().Be(create.InnFirstHead);
                result.PhoneFirstHead.Should().Be(create.PhoneFirstHead);
                result.ChiefAccountant.Should().Be(create.ChiefAccountant);
                result.InnChiefAccountant.Should().Be(create.InnChiefAccountant);
                result.PhoneChiefAccountant.Should().Be(create.PhoneChiefAccountant);
                result.SertChiefAccountant.Should().Be(create.SertChiefAccountant);
                result.Agreement.Should().Be(create.Agreement);
                result.ProfSert.Should().Be(create.ProfSert);
                result.ProfUserSert.Should().Be(create.ProfUserSert);
                result.CreateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
                result.UpdateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
                result.Timestamp.Should().NotBeEmpty();
                AddDataForCleaner(result.Id);
            }
        }

        [TestMethod]
        public void User_Update_Test()
        {
            using (var server = new Starter().TestLogin())
            {
                InitCleaner(server.Sql);
                var create = new User.Create();
                create.OrgName = "OrgName - TEST";
                create.Inn = "Inn - TEST";
                create.OrgCreateAt = DateTime.Now.AddDays(6);
                create.OPF = "OPF - TEST";
                create.OrgUpr = "OrgUpr - TEST";
                create.Activity = "Activity - TEST";
                create.OwnershipId = 1;
                create.LegalAddress = "LegalAddress - TEST";
                create.OrgSite = "OrgSite - TEST";
                create.Email = "Email - TEST";
                create.FirstHead = "FirstHead - TEST";
                create.InnFirstHead = "InnFirstHead - TEST";
                create.PhoneFirstHead = "PhoneFirstHead - TEST";
                create.ChiefAccountant = "ChiefAccountant - TEST";
                create.InnChiefAccountant = "InnChiefAccountant - TEST";
                create.PhoneChiefAccountant = "992880008444";
                create.SertChiefAccountant = "SertChiefAccountant - TEST";
                create.Agreement = true;
                create.ProfSert = true;
                create.ProfSert = true;
                create.ProfUserSert = "ProfUserSert - TEST";
                create.ProfUserSertDate = "ProfUserSert - TEST";
                create.ProfUserSertVM = "ProfUserSert - TEST";
                create.CreateAt = DateTime.Now.AddDays(9);
                create.UpdateAt = DateTime.Now.AddDays(6);

                var now = DateTime.Now;

                var result = create.ExecTest(server.Client); //need to use correct client server 

                var resId = 1;
                var update = new User.Update();
                update.Id = resId;
                update.OrgName = "OrgName - TEST-updated";
                update.Inn = "Inn - TEST-updated";
                update.OrgCreateAt = DateTime.Now.AddDays(2);
                update.OPF = "OPF - TEST-updated";
                update.OrgUpr = "OrgUpr - TEST-updated";
                update.Activity = "Activity - TEST-updated";
                update.LegalAddress = "LegalAddress - TEST-updated";
                update.OrgSite = "OrgSite - TEST-updated";
                update.Email = "Email - TEST-updated";
                update.FirstHead = "FirstHead - TEST-updated";
                update.InnFirstHead = "InnFirstHead - TEST-updated";
                update.PhoneFirstHead = "PhoneFirstHead - TEST-updated";
                update.ChiefAccountant = "ChiefAccountant - TEST-updated";
                update.InnChiefAccountant = "InnChiefAccountant - TEST-updated";
                update.PhoneChiefAccountant = "992880008444";
                update.SertChiefAccountant = "SertChiefAccountant - TEST-updated";
                update.Agreement = true;
                update.ProfSert = false;
                update.State = User.Status.Active;
                update.UserType = UserType.Admin;
                update.ProfUserSert = "ProfUserSert - TEST-updated";
                update.ProfUserSertDate = "ProfUserSert - TEST";
                update.ProfUserSertVM = "ProfUserSert - TEST";
                //update.Login = "Login - TEST-updated";
                update.UpdateAt = DateTime.Now.AddDays(3);
                update.Timestamp = result.Timestamp;
                result = update.ExecTest(server.Client); //need to use correct client server 

                result.Id.Should().Be(resId);
                result.OrgName.Should().Be(update.OrgName);
                result.Inn.Should().Be(update.Inn);
                result.OrgCreateAt.Should().Be(update.OrgCreateAt);
                result.OPF.Should().Be(update.OPF);
                result.OrgUpr.Should().Be(update.OrgUpr);
                result.Activity.Should().Be(update.Activity);
                result.OwnershipId.Should().Be(create.OwnershipId);
                result.LegalAddress.Should().Be(update.LegalAddress);
                result.OrgSite.Should().Be(update.OrgSite);
                result.Email.Should().Be(update.Email);
                result.FirstHead.Should().Be(update.FirstHead);
                result.InnFirstHead.Should().Be(update.InnFirstHead);
                result.PhoneFirstHead.Should().Be(update.PhoneFirstHead);
                result.ChiefAccountant.Should().Be(update.ChiefAccountant);
                result.InnChiefAccountant.Should().Be(update.InnChiefAccountant);
                result.PhoneChiefAccountant.Should().Be(update.PhoneChiefAccountant);
                result.SertChiefAccountant.Should().Be(update.SertChiefAccountant);
                result.Agreement.Should().Be(update.Agreement);
                result.ProfSert.Should().Be(update.ProfSert);
                result.ProfUserSert.Should().Be(update.ProfUserSert);
                result.UserType.Should().Be(update.UserType);
                // result.Login.Should().Be(update.Login);
                result.CreateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
                result.UpdateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
                result.Timestamp.Should().NotBeEmpty();
                AddDataForCleaner(result.Id);
            }
        }

        [TestMethod]
        public void User_Search_Test()
        {
            using (var server = new Starter().TestLogin())
            {
                InitCleaner(server.Sql);
                var create = new User.Create();
                create.OrgName = "OrgName - TEST";
                create.Inn = "Inn - TEST";
                create.OrgCreateAt = DateTime.Now.AddDays(3);
                create.OPF = "OPF - TEST";
                create.OrgUpr = "OrgUpr - TEST";
                create.Activity = "Activity - TEST";
                create.OwnershipId = 1;
                create.LegalAddress = "LegalAddress - TEST";
                create.OrgSite = "OrgSite - TEST";
                create.Email = "Email - TEST";
                create.FirstHead = "FirstHead - TEST";
                create.InnFirstHead = "InnFirstHead - TEST";
                create.PhoneFirstHead = "PhoneFirstHead - TEST";
                create.ChiefAccountant = "ChiefAccountant - TEST";
                create.InnChiefAccountant = "InnChiefAccountant - TEST";
                create.PhoneChiefAccountant = "PhoneChiefAccountant - TEST";
                create.SertChiefAccountant = "SertChiefAccountant - TEST";
                create.Agreement = true;
                create.ProfSert = false;
                create.ProfUserSert = "ProfUserSert - TEST";
                create.ProfUserSertDate = "ProfUserSert - TEST";
                create.ProfUserSertVM = "ProfUserSert - TEST";
                create.CreateAt = DateTime.Now.AddDays(6);
                create.UpdateAt = DateTime.Now.AddDays(3);

                var search = new User.Search();
                search.Ids = new int[] { 0 };

                var searchRes = search.ExecTest(server.Client);

                searchRes.Total.Should().Be(0);
                searchRes.Items.Should().HaveCount(0);
                search.OrderBy.Direction = SearchByDirection.Desc;
                searchRes = search.ExecTest(server.Client);

                searchRes.Total.Should().Be(0);
                searchRes.Items.Should().HaveCount(0);
                search.PageInfo = new PageInfo(1, 10);
                search.OrderBy.Direction = SearchByDirection.Asc;
                searchRes = search.ExecTest(server.Client);

                searchRes.Total.Should().Be(0);
                searchRes.Items.Should().HaveCount(0);
                search.PageInfo.PageNumber = 2;
                searchRes = search.ExecTest(server.Client);

                searchRes.Total.Should().Be(0);
                searchRes.Items.Should().HaveCount(0);

                search.PageInfo = new PageInfo(1, 10);
                search.OrderBy.Direction = SearchByDirection.Asc;

                /*                   *   *   *   *
                                              PLEASE ADD COMBINE FILTERS
                                            *   *   *   *   *   *   #1#
            }
        }

        [TestMethod]
        public void User_List_Test()
        {
            using (var server = new Starter().TestLogin())
            {
                InitCleaner(server.Sql);
                var create = new User.Create();
                create.OrgName = "OrgName - TEST";
                create.Inn = "Inn - TEST";
                create.OrgCreateAt = DateTime.Now.AddDays(5);
                create.OPF = "OPF - TEST";
                create.OrgUpr = "OrgUpr - TEST";
                create.Activity = "Activity - TEST";
                create.OwnershipId = 1;
                create.LegalAddress = "LegalAddress - TEST";
                create.OrgSite = "OrgSite - TEST";
                create.Email = "Email - TEST";
                create.FirstHead = "FirstHead - TEST";
                create.InnFirstHead = "InnFirstHead - TEST";
                create.PhoneFirstHead = "PhoneFirstHead - TEST";
                create.ChiefAccountant = "ChiefAccountant - TEST";
                create.InnChiefAccountant = "InnChiefAccountant - TEST";
                create.PhoneChiefAccountant = "PhoneChiefAccountant - TEST";
                create.SertChiefAccountant = "SertChiefAccountant - TEST";
                create.Agreement = true;
                create.ProfSert = false;
                create.ProfUserSert = "ProfUserSert - TEST";
                create.ProfUserSertDate = "ProfUserSert - TEST";
                create.ProfUserSertVM = "ProfUserSert - TEST";
                create.CreateAt = DateTime.Now.AddDays(2);
                create.UpdateAt = DateTime.Now.AddDays(2);

                var now = DateTime.Now;

                var result = create.ExecTest(server.Client); //need to use correct client server 

                result = User.GetDocumentForTest(result.Id, server.Client); //need to use correct client server 

                result.Id.Should().BeGreaterThan(0);
                result.OrgName.Should().Be(create.OrgName);
                result.Inn.Should().Be(create.Inn);
                result.OrgCreateAt.Should().Be(create.OrgCreateAt);
                result.OPF.Should().Be(create.OPF);
                result.OrgUpr.Should().Be(create.OrgUpr);
                result.Activity.Should().Be(create.Activity);
                result.OwnershipId.Should().Be(create.OwnershipId);
                result.LegalAddress.Should().Be(create.LegalAddress);
                result.OrgSite.Should().Be(create.OrgSite);
                result.Email.Should().Be(create.Email);
                result.FirstHead.Should().Be(create.FirstHead);
                result.InnFirstHead.Should().Be(create.InnFirstHead);
                result.PhoneFirstHead.Should().Be(create.PhoneFirstHead);
                result.ChiefAccountant.Should().Be(create.ChiefAccountant);
                result.InnChiefAccountant.Should().Be(create.InnChiefAccountant);
                result.PhoneChiefAccountant.Should().Be(create.PhoneChiefAccountant);
                result.SertChiefAccountant.Should().Be(create.SertChiefAccountant);
                result.Agreement.Should().Be(create.Agreement);
                result.ProfSert.Should().Be(create.ProfSert);
                result.ProfUserSert.Should().Be(create.ProfUserSert);
                result.ProfUserSertVM.Should().Be(create.ProfUserSertVM);
                result.ProfUserSertDate.Should().Be(create.ProfUserSertDate);
                result.CreateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
                result.UpdateAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
                result.Timestamp.Should().NotBeEmpty();
                AddDataForCleaner(result.Id);
            }
        }

        [TestMethod]
        public void User_Org_Info_Create_Test()
        {
            using (var server = new Starter().TestLogin())
            {
                InitCleaner(server.Sql);
                var create = Context.DefaultUser_OrgInfo(
                    $"testuser_{DateTime.Now.ToString("ddMMyyyy")}_{Guid.NewGuid().ToString().Substring(1, 5)}",
                    $"testuser_{DateTime.Now.ToString("ddMMyyyy")}");
                var result = create.ExecTest(server.Client); //need to use correct client server 
                result.Should().BeTrue();
            }
        }

        [TestMethod]
        public void User_IndividualsInfo_Create_Test()
        {
            using (var server = new Starter().TestLogin())
            {
                InitCleaner(server.Sql);
                var create = Context.DefaultUser_IndividualsInfo(
                    $"testuser_{DateTime.Now.ToString("ddMMyyyy")}_{Guid.NewGuid().ToString().Substring(1, 5)}",
                    $"testuser_{DateTime.Now.ToString("ddMMyyyy")}");
                var result = create.ExecTest(server.Client); //need to use correct client server 
                result.Should().BeTrue();
            }
        }*/
    }
}