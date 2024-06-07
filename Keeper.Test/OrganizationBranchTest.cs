using Bibliotekaen.Sql;
using Keeper.Client;

namespace Keeper.Test;

[TestClass]
public class OrganizationBranchTest
{
    private readonly List<int> m_organizationBranchIds = [];
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
            if (m_organizationBranchIds.Any())
            {
                var query = $"delete from [new-keeper].[OrganizationBranches] where [Id] in ({string.Join(", ", m_organizationBranchIds)})";
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

        var request = new OrganizationBranch.Create
        {
            
        };
    }
}