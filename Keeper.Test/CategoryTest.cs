using Bibliotekaen.Sql;
using FluentAssertions;
using Keeper.Client;
using Row.Common.Dto1;

namespace Keeper.Test;

[TestClass]
public class CategoryTest
{
    private readonly List<int> m_categoryIds = [];
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
            if (m_categoryIds.Any())
            {
                var query = $"delete from [new-keeper].[Categories] where [Id] in ({string.Join(", ", m_categoryIds)})";
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

        var request = new Category.Create
        {
            Name = "Test category",
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_categoryIds.Add(result.Id);

        result.Name.Should().Be(request.Name);
        result.ParentId.Should().Be(request.ParentId);
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

        var request = new Category.Create
        {
            Name = "Test category",
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_categoryIds.Add(result.Id);

        result.Name.Should().Be(request.Name);
        result.ParentId.Should().Be(request.ParentId);

        var updateRequest = new Category.Update
        {
            Id = result.Id,
            Name = "Update " + result.Name,
            ParentId = result.Id
        };

        var updateResult = updateRequest.ExecTest(server.Client);

        updateResult.Id.Should().Be(result.Id);
        updateResult.Name.Should().Be(updateRequest.Name);
        updateResult.ParentId.Should().Be(updateRequest.ParentId);
        updateResult.CreatedAt.Should().Be(result.CreatedAt);
        updateResult.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        updateResult.Timestamp.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void SearchCategory()
    {
        using var server = new Starter().TestLogin();

        InitCleaner(server.Sql);

        var createList = new List<Category.Create>();

        for (int i = 0; i < 5; i++)
        {
            var request = new Category.Create
            {
                Name = "Test category",
            };

            createList.Add(request);

            var result = request.ExecTest(server.Client);

            result.Id.Should().BeGreaterThan(0);
            m_categoryIds.Add(result.Id);
        }

        var searchRequest = new Category.Search
        {
            Ids = m_categoryIds.ToArray()
        };

        var searchResult = searchRequest.ExecCategoryTest(server.Client);

        searchResult.Total.Should().Be(5);
        foreach (var item in searchResult.Items)
        {
            createList.Select(x => x.Name).Should().Contain(item.Name);
        }

        searchRequest = new Category.Search
        {
            PageInfo = new PageInfo
            {
                PageNumber = 1000
            }
        };

        searchResult = searchRequest.ExecCategoryTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Should().BeEmpty();

        searchRequest = new Category.Search
        {
            Filters = new Category.Search.Filter
            {
                Name = "name_012345"
            }
        };

        searchResult = searchRequest.ExecCategoryTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Should().BeEmpty();
    }

    [TestMethod]
    public void SearchSubCategory()
    {
        using var server = new Starter().TestLogin();

        InitCleaner(server.Sql);

        var subCategories = new List<Category.Create>();

        var create = new Category.Create
        {
            Name = "Test category",
        };

        var category = create.ExecTest(server.Client);

        category.Id.Should().BeGreaterThan(0);
        m_categoryIds.Add(category.Id);

        var parentId = category.Id;

        for (int j = 0; j < 5; j++)
        {
            var request = new Category.Create
            {
                Name = "Test sub-category",
                ParentId = parentId
            };

            subCategories.Add(request);

            var result = request.ExecTest(server.Client);

            result.Id.Should().BeGreaterThan(0);
            m_categoryIds.Add(result.Id);
        }

        var searchRequest = new Category.Search
        {
            Ids = [parentId]
        };

        var searchResult = searchRequest.ExecSubCategoryTest(server.Client);

        searchResult.Total.Should().Be(5);
        foreach (var item in searchResult.Items)
        {
            item.CategoryName.Should().Be(create.Name);
            subCategories.Select(x => x.Name).Should().Contain(item.SubCategoryName);
        }

        searchRequest = new Category.Search
        {
            PageInfo = new PageInfo
            {
                PageNumber = 1000
            }
        };

        searchResult = searchRequest.ExecSubCategoryTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Should().BeEmpty();

        searchRequest = new Category.Search
        {
            Filters = new Category.Search.Filter
            {
                Name = "name_012345"
            }
        };

        searchResult = searchRequest.ExecSubCategoryTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Should().BeEmpty();
    }
}