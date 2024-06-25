using Bibliotekaen.Sql;
using FluentAssertions;
using Keeper.Client;
using Row.Common.Dto1;

namespace Keeper.Test;

[TestClass]
public class SupplierTest
{
    private readonly List<int> m_supplierIds = [];
    private ISqlFactory? m_sql;

    [TestInitialize]
    public void Init()
    {
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (m_sql == null) return;
        
        if (m_supplierIds.Count != 0)
        {
            var query = $"delete from [new-keeper].[Suppliers] where [Id] in ({string.Join(", ", m_supplierIds)})";
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

        var now = DateTime.Now;

        var request = new Supplier.Create
        {
            CompanyName = "Test supplier",
            Phone = Context.GeneratePhone(),
            Email = "test@mail.com",
            Address = "Dushanbe, 734000"
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_supplierIds.Add(result.Id);
        
        result.CompanyName.Should().Be(request.CompanyName);
        result.Phone.Should().Be(request.Phone);
        result.Email.Should().Be(request.Email);
        result.Address.Should().Be(request.Address);
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

        var request = new Supplier.Create
        {
            CompanyName = "Test supplier",
            Phone = Context.GeneratePhone(),
            Email = "test@mail.com",
            Address = "Dushanbe, 734000"
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_supplierIds.Add(result.Id);
        
        result.CompanyName.Should().Be(request.CompanyName);
        result.Phone.Should().Be(request.Phone);
        result.Email.Should().Be(request.Email);
        result.Address.Should().Be(request.Address);
        result.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.Timestamp.Should().NotBeNullOrEmpty();

        var updateRequest = new Supplier.Update
        {
            Id = result.Id,
            CompanyName = "Test " + result.CompanyName,
            Phone = result.Phone,
            Email = result.Email,
            Address = "Update " + result.Address
        };

        var updateResult = updateRequest.ExecTest(server.Client);

        updateResult.Id.Should().Be(result.Id);
        updateResult.CompanyName.Should().Be(updateRequest.CompanyName);
        updateResult.Phone.Should().Be(updateRequest.Phone);
        updateResult.Email.Should().Be(updateRequest.Email);
        updateResult.Address.Should().Be(updateRequest.Address);
        updateResult.CreatedAt.Should().Be(result.CreatedAt);
        updateResult.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        updateResult.Timestamp.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void Search()
    {
        using var server = new Starter().TestLogin();
        
        InitCleaner(server.Sql);

        var createList = new List<Supplier.Create>();

        for (int i = 0; i < 5; i++)
        {
            var request = new Supplier.Create
            {
                CompanyName = $"Test supplier_{i}",
                Phone = Context.GeneratePhone(),
                Email = $"test_{i}@mail.com",
                Address = $"Dushanbe, 73400{i}"
            };
            
            createList.Add(request);

            var result = request.ExecTest(server.Client);

            result.Id.Should().BeGreaterThan(0);
            m_supplierIds.Add(result.Id);
        }

        var searchRequest = new Supplier.Search
        {
            Ids = m_supplierIds.ToArray()
        };

        var searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Total.Should().Be(5);

        foreach (var item in searchResult.Items)
        {
            m_supplierIds.Should().Contain(item.Id);
            createList.Select(x => x.CompanyName).Should().Contain(item.CompanyName);
            createList.Select(x => x.Phone).Should().Contain(item.Phone);
            createList.Select(x => x.Email).Should().Contain(item.Email);
            createList.Select(x => x.Address).Should().Contain(item.Address);
        }

        searchRequest = new Supplier.Search
        {
            PageInfo = new PageInfo
            {
                PageNumber = 10000
            }
        };

        searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Should().BeEmpty();

        searchRequest = new Supplier.Search
        {
            Filters = new Supplier.Search.Filter
            {
                CompanyName = "Comapany_012345"
            }
        };

        searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Should().BeEmpty();
    }
}