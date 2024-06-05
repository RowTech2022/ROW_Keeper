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
                var query = $"delete from [new-keeper].[Organization] where [Id] in ({m_organizationIds})";
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
}