using System.Net.Http.Headers;
using Bibliotekaen.Sql;
using FluentAssertions;
using Keeper.Client;

namespace Keeper.Test;

[TestClass]
public class OrganizationTest
{
    private readonly List<int> m_organizationIds = [];
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
        }
    }

    public void InitCleaner(ISqlFactory sql)
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
        using var server = new Starter();

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

        var request = new Organization.Create
        {
            OwnerId = user.Id,
            OrgName = "Test organization",
            OrgPhone = "992123456789",
            OrgAddress = "Dushanbe, Rudaky 17"
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_organizationIds.Add(result.Id);
        
        result.OwnerId.Should().BeGreaterThan(0);
        result.OrgName.Should().Be(request.OrgName);
        result.OrgPhone.Should().Be(request.OrgPhone);
        result.OrgAddress.Should().Be(request.OrgAddress);
        result.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.Timestamp.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void Update()
    {
        using var server = new Starter();

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

        var request = new Organization.Create
        {
            OwnerId = user.Id,
            OrgName = "Test organization",
            OrgPhone = "992123456789",
            OrgAddress = "Dushanbe, Rudaky 17"
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_organizationIds.Add(result.Id);
        
        result.OwnerId.Should().BeGreaterThan(0);
        result.OrgName.Should().Be(request.OrgName);
        result.OrgPhone.Should().Be(request.OrgPhone);
        result.OrgAddress.Should().Be(request.OrgAddress);
        result.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.Timestamp.Should().NotBeNullOrEmpty();

        var updateRequest = new Organization.Update
        {
            Id = result.Id,
            OwnerId = result.OwnerId,
            OrgName = "Update " + result.OrgName,
            OrgPhone = result.OrgPhone,
            OrgEmail = "up_" + result.OrgEmail,
            OrgAddress = "Update " + result.OrgAddress,
            Timestamp = result.Timestamp
        };

        var updateResult = updateRequest.ExecTest(server.Client);

        updateResult.Id.Should().Be(updateRequest.Id);
        updateResult.OrgName.Should().Be(updateRequest.OrgName);
        updateResult.OrgPhone.Should().Be(updateRequest.OrgPhone);
        updateResult.OrgEmail.Should().Be(updateRequest.OrgEmail);
        updateResult.OrgAddress.Should().Be(updateRequest.OrgAddress);
        updateResult.CreatedAt.Should().Be(result.CreatedAt);
        updateResult.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        updateResult.Timestamp.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void Search()
    {
        using var server = new Starter();
        
        
    }
}