using Bibliotekaen.Sql;
using FluentAssertions;
using Keeper.Client;

namespace Keeper.Test;

[TestClass]
public class OrganizationBranchTest
{
    private readonly List<int> m_organizationBranchIds = [];
    private readonly List<int> m_userIds = [];
    
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
            if (m_organizationBranchIds.Count != 0)
            {
                var query = $"delete from [new-keeper].[OrganizationBranches] where [Id] in ({string.Join(", ", m_organizationBranchIds)})";
                m_sql.Query(query);
            }

            if (m_userIds.Count != 0)
            {
                var query = $"delete from [new-keeper].[Users] where [Id] in ({string.Join(", ", m_userIds)})";
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
        m_organizationBranchIds.Add(id);
    }

    [TestMethod]
    public void Create()
    {
        using var server = new Starter().TestLogin();

        InitCleaner(server.Sql);

        var now = DateTime.Now;

        var userRequest = Context.DefaultUser();
        var user = userRequest.ExecTest(server.Client);
        m_userIds.Add(user.Id);

        var request = new OrganizationBranch.Create
        {
            OwnerId = user.Id,
            BranchName = "Test branch name",
            BranchPhone = "Test branch phone",
            BranchEmail = "Test branch email",
            BranchAddress = "Test branch address"
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_organizationBranchIds.Add(result.Id);
        
        result.OwnerId.Should().Be(user.Id);
        result.BranchName.Should().Be(request.BranchName);
        result.BranchPhone.Should().Be(request.BranchPhone);
        result.BranchEmail.Should().Be(request.BranchEmail);
        result.BranchAddress.Should().Be(request.BranchAddress);
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
        m_userIds.Add(user.Id);

        var request = new OrganizationBranch.Create
        {
            OwnerId = user.Id,
            BranchName = "Test branch name",
            BranchPhone = "Test branch phone",
            BranchEmail = "Test branch email",
            BranchAddress = "Test branch address"
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_organizationBranchIds.Add(result.Id);
        
        result.OwnerId.Should().Be(user.Id);
        result.BranchName.Should().Be(request.BranchName);
        result.BranchPhone.Should().Be(request.BranchPhone);
        result.BranchEmail.Should().Be(request.BranchEmail);
        result.BranchAddress.Should().Be(request.BranchAddress);
        result.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.Timestamp.Should().NotBeNullOrEmpty();

        var updateRequest = new OrganizationBranch.Update
        {
            Id = result.Id,
            OwnerId = user.Id,
            BranchName = "Update " + result.BranchName,
            BranchPhone = result.BranchPhone,
            BranchEmail = "up_" + result.BranchEmail,
            BranchAddress = "Update " + result.BranchAddress,
            Timestamp = result.Timestamp
        };

        var updateResult = updateRequest.ExecTest(server.Client);

        updateResult.Id.Should().Be(updateRequest.Id);
        updateResult.OwnerId.Should().Be(updateRequest.OwnerId);
        updateResult.BranchName.Should().Be(updateRequest.BranchName);
        updateResult.BranchPhone.Should().Be(updateRequest.BranchPhone);
        updateResult.BranchEmail.Should().Be(updateRequest.BranchEmail);
        updateResult.BranchAddress.Should().Be(updateRequest.BranchAddress);
        updateResult.CreatedAt.Should().Be(result.CreatedAt);
        updateResult.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        updateResult.Timestamp.Should().NotBeNullOrEmpty();
    }
}