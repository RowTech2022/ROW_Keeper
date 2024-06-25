using Bibliotekaen.Sql;
using FluentAssertions;
using Keeper.Client;
using Row.Common.Dto1;

namespace Keeper.Test;

[TestClass]
public class PlanTest
{
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
            if (m_planIds.Any())
            {
                var query = $"delete from [new-keeper].[Plans] where [Id] in ({string.Join(", ", m_planIds)})";
                m_sql.Query(query);
            }
        }
    }

    public void InitCleaner(ISqlFactory sql)
    {
        m_sql = sql;
    }

    [TestMethod]
    public void Create()
    {
        using var server = new Starter().TestLogin();

        InitCleaner(server.Sql);

        var now = DateTime.Now;

        var request = new Plan.Create
        {
            Name = "Test plan",
            Price = 999.99m,
            Duration = 30,
            Type = Plan.PlanType.Monthly
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_planIds.Add(result.Id);

        result.Name.Should().Be(request.Name);
        result.Price.Should().Be(request.Price);
        result.Duration.Should().Be(request.Duration);
        result.Type.Should().Be(request.Type);
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

        var createRequest = new Plan.Create
        {
            Name = "Test plan",
            Price = 999.99m,
            Duration = 30,
            Type = Plan.PlanType.Monthly
        };

        var plan = createRequest.ExecTest(server.Client);

        plan.Id.Should().BeGreaterThan(0);
        m_planIds.Add(plan.Id);

        var request = new Plan.Update
        {
            Id = plan.Id,
            Name = "Update " + plan.Name,
            Price = plan.Price + 10,
            Duration = 21,
            Type = Plan.PlanType.Monthly
        };

        var result = request.ExecTest(server.Client);

        result.Name.Should().Be(request.Name);
        result.Price.Should().Be(request.Price);
        result.Duration.Should().Be(request.Duration);
        result.Type.Should().Be(request.Type);
        result.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.Timestamp.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void Search()
    {
        using var server = new Starter().TestLogin();

        InitCleaner(server.Sql);

        var planList = new List<Plan.Create>();

        for (int i = 0; i < 5; i++)
        {
            var createRequest = new Plan.Create
            {
                Name = "Test plan",
                Price = 999.99m,
                Duration = 30,
                Type = Plan.PlanType.Monthly
            };

            planList.Add(createRequest);

            var plan = createRequest.ExecTest(server.Client);

            plan.Id.Should().BeGreaterThan(0);
            m_planIds.Add(plan.Id);
        }

        var request = new Plan.Search
        {
            Ids = m_planIds.ToArray()
        };

        var result = request.ExecTest(server.Client);

        result.Total.Should().Be(5);
        foreach (var item in result.Items)
        {
            m_planIds.Should().Contain(item.Id);
            planList.Select(x => x.Name).Should().Contain(item.Name);
            planList.Select(x => x.Price).Should().Contain(item.Price);
            planList.Select(x => x.Duration).Should().Contain(item.Duration);
            planList.Select(x => x.Type).Should().Contain(item.Type);
        }

        request = new Plan.Search
        {
            Filters = new Plan.Search.Filter
            {
                Name = "test_plan_12345"
            }
        };

        result = request.ExecTest(server.Client);

        result.Total.Should().Be(0);
        result.Items.Should().BeEmpty();

        request = new Plan.Search
        {
            PageInfo = new PageInfo
            {
                PageNumber = 999999,
                PageSize = 10
            }
        };

        result = request.ExecTest(server.Client);

        result.Items.Should().BeEmpty();
        result.Total.Should().Be(0);
    }
}