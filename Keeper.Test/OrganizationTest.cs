using System.Net.Http.Headers;
using Bibliotekaen.Sql;
using FluentAssertions;
using Keeper.Client;
using Row.Common.Dto1;

namespace Keeper.Test;

[TestClass]
public class OrganizationTest
{
    private readonly List<int> m_organizationIds = [];
    private readonly List<int> m_planIds = [];
    private ISqlFactory? m_sql;

    [TestInitialize]
    public void Init()
    {
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (m_sql != null)
        {
            if (m_organizationIds.Any())
            {
                var query = $"delete from [new-keeper].[Organizations] where [Id] in ({string.Join(", ", m_organizationIds)})";
                m_sql.Query(query);
            }
            if (m_planIds.Any())
            {
                var query = $"delete from [new-keeper].[Plans] where [Id] in ({string.Join(", ", m_planIds)})";
                m_sql.Query(query);
            }
        }
    }

    private void InitCleaner(ISqlFactory sql)
    {
        m_sql = sql;
    }

    public void AddDataForCleaner(int id)
    {
        m_organizationIds.Add(id);
    }

    [TestMethod]
    public void Create()
    {
        using var server = new Starter().TestLogin();

        InitCleaner(server.Sql);

        var now = DateTime.Now;

        var userRequest = Context.DefaultUser();
        var user = userRequest.ExecTest(server.Client);
        var password = server.Settings.SendSmsMock.GetLastPassword(user.Login);

        var token = new Login
        {
            UserLogin = user.Login,
            Password = password
        }.ExecTest(server.Client);
        
        server.ClientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        
        server.Login(user.Login, password);

        var planRequest = new Plan.Create
        {
            Name = "Test plan",
            Price = 125.50m,
            Duration = 20,
            Type = Plan.PlanType.Monthly
        };

        var plan = planRequest.ExecTest(server.Client);

        plan.Id.Should().BeGreaterThan(0);
        m_planIds.Add(plan.Id);

        var request = new Organization.Create
        {
            PlanId = plan.Id,
            OwnerId = user.Id,
            OrgName = "Test organization",
            OrgDescription = "Test description",
            OrgPhone = "992123456789",
            OrgAddress = "Dushanbe, Rudaky 17",
            OrgEmail = "test@mail.com",
            OwnerFullName = "Test Testov",
            OwnerEmail = "test@mail.com",
            OwnerPhone = "992123456789",
            Status = Organization.OrgStatus.Acitve
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_organizationIds.Add(result.Id);

        result.Plan?.Id.Should().Be(plan.Id);
        result.Plan?.Value.Should().Be(plan.Name);
        result.OwnerId.Should().BeGreaterThan(0);
        result.OrgName.Should().Be(request.OrgName);
        result.OrgDescription.Should().Be(request.OrgDescription);
        result.OrgPhone.Should().Be(request.OrgPhone);
        result.OwnerFullName.Should().Be(request.OwnerFullName);
        result.OwnerEmail.Should().Be(request.OwnerEmail);
        result.OwnerPhone.Should().Be(request.OwnerPhone);
        result.OrgAddress.Should().Be(request.OrgAddress);
        result.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.Timestamp.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void Update()
    {
        using var server = new Starter().TestLogin();

        InitCleaner(server.Sql);

        var now = DateTime.Now;

        var userRequest = Context.DefaultUser();
        var user = userRequest.ExecTest(server.Client);
        var password = server.Settings.SendSmsMock.GetLastPassword(user.Login);

        var token = new Login
        {
            UserLogin = user.Login,
            Password = password
        }.ExecTest(server.Client);
        
        server.ClientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        
        server.Login(user.Login, password);

        var planRequest = new Plan.Create
        {
            Name = "Test plan",
            Price = 125.50m,
            Duration = 20,
            Type = Plan.PlanType.Monthly
        };

        var plan = planRequest.ExecTest(server.Client);

        plan.Id.Should().BeGreaterThan(0);
        m_planIds.Add(plan.Id);

        var request = new Organization.Create
        {
            PlanId = plan.Id,
            OwnerId = user.Id,
            OrgName = "Test organization",
            OrgDescription = "Test description",
            OrgPhone = "992123456789",
            OrgAddress = "Dushanbe, Rudaky 17",
            OrgEmail = "test@mail.com",
            OwnerFullName = "Test Testov",
            OwnerEmail = "test@mail.com",
            OwnerPhone = "992123456789",
            Status = Organization.OrgStatus.Acitve
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_organizationIds.Add(result.Id);

        var updateRequest = new Organization.Update
        {
            Id = result.Id,
            OwnerId = result.OwnerId,
            OrgName = "Update " + result.OrgName,
            OrgDescription = "Update " + result.OrgDescription,
            OrgPhone = result.OrgPhone,
            OrgEmail = "up_" + result.OrgEmail,
            OrgAddress = "Update " + result.OrgAddress,
            Timestamp = result.Timestamp,
            OwnerFullName = "Udpate " + result.OwnerFullName,
            OwnerEmail = "update" + result.OwnerEmail,
            OwnerPhone = result.OwnerPhone,
            Status = Organization.OrgStatus.InActive
        };

        var updateResult = updateRequest.ExecTest(server.Client);

        updateResult.Id.Should().Be(updateRequest.Id);
        updateResult.OrgName.Should().Be(updateRequest.OrgName);
        updateResult.OrgPhone.Should().Be(updateRequest.OrgPhone);
        updateResult.OrgEmail.Should().Be(updateRequest.OrgEmail);
        updateResult.OwnerFullName.Should().Be(updateRequest.OwnerFullName);
        updateResult.OwnerPhone.Should().Be(updateRequest.OwnerPhone);
        updateResult.OwnerEmail.Should().Be(updateRequest.OwnerEmail);
        updateResult.OrgAddress.Should().Be(updateRequest.OrgAddress);
        updateResult.CreatedAt.Should().Be(result.CreatedAt);
        updateResult.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        updateResult.Timestamp.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void Search()
    {
        using var server = new Starter().TestLogin();

        InitCleaner(server.Sql);

        var createList = new List<Organization.Create>();

        var planRequest = new Plan.Create
        {
            Name = "Test plan",
            Price = 125.50m,
            Duration = 20,
            Type = Plan.PlanType.Monthly
        };

        var plan = planRequest.ExecTest(server.Client);

        plan.Id.Should().BeGreaterThan(0);
        m_planIds.Add(plan.Id);
        
        var userRequest = Context.DefaultUser();
        var user = userRequest.ExecTest(server.Client);

        for (int i = 0; i < 5; i++)
        {
            var request = new Organization.Create
            {
                PlanId = plan.Id,
                OwnerId = user.Id,
                OrgName = "Test organization",
                OrgDescription = "Test description",
                OrgPhone = "992123456789",
                OrgAddress = "Dushanbe, Rudaky 17",
                OrgEmail = "test@mail.com",
                OwnerFullName = "Test Testov",
                OwnerEmail = "test@mail.com",
                OwnerPhone = "992123456789",
                Status = Organization.OrgStatus.Acitve
            };

            createList.Add(request);

            var result = request.ExecTest(server.Client);

            result.Id.Should().BeGreaterThan(0);
            m_organizationIds.Add(result.Id);
        }

        var searchRequest = new Organization.Search
        {
            Ids = m_organizationIds.ToArray()
        };

        var searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Should().NotBeNull();
        searchResult.Total.Should().Be(5);
        foreach (var item in searchResult.Items)
        {
            createList.Select(x => x.OrgName).Should().Contain(item.OrgName);
            createList.Select(x => x.OrgPhone).Should().Contain(item.OrgPhone);
            createList.Select(x => x.OrgEmail).Should().Contain(item.OrgEmail);
            createList.Select(x => x.OrgAddress).Should().Contain(item.OrgAddress);
        }
        
        searchRequest = new Organization.Search
        {
            Ids = m_organizationIds.ToArray(),
            PageInfo = new PageInfo
            {
                PageNumber = 1000
            }
        };

        searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Should().BeEmpty();

        searchRequest = new Organization.Search
        {
            Filters = new Organization.Search.Filter
            {
                OrgName = "тест_01234"
            }
        };

        searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Should().BeEmpty();
    }
}