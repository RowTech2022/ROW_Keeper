using Bibliotekaen.Monads;
using Bibliotekaen.Sql;
using FluentAssertions;
using Keeper.Client;
using Keeper.Client.Product;
using Keeper.Client.ProductDiscount;
using Row.Common.Dto1;

namespace Keeper.Test;

[TestClass]
public class ProductDiscountTest
{
    private readonly List<int> m_productIds = [];
    private readonly List<int> m_categoryIds = [];
    private readonly List<int> m_supplierIds = [];
    private readonly List<int> m_productDiscountIds = [];
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
            if (m_productIds.Any())
            {
                var query = $"delete from [new-keeper].[Products] where [Id] in ({string.Join(", ", m_productIds)})";
                m_sql.Query(query);
            }
            
            if (m_categoryIds.Any())
            {
                var query = $"delete from [new-keeper].[Categories] where [Id] in ({string.Join(", ", m_categoryIds)})";
                m_sql.Query(query);
            }
            
            if (m_supplierIds.Any())
            {
                var query = $"delete from [new-keeper].[Suppliers] where [Id] in ({string.Join(", ", m_supplierIds)})";
                m_sql.Query(query);
            }
            
            if (m_productDiscountIds.Any())
            {
                var query = $"delete from [new-keeper].[ProductDiscounts] where [Id] in ({string.Join(", ", m_productDiscountIds)})";
                m_sql.Query(query);
            }
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

        var categoryRequest = new Category.Create
        {
            Name = "Test category product"
        };

        var category = categoryRequest.ExecTest(server.Client);

        category.Id.Should().BeGreaterThan(0);
        m_categoryIds.Add(category.Id);

        var subCategoryRequest = new Category.Create
        {
            Name = "Test sub category product",
            ParentId = category.Id
        };

        var subCategory = subCategoryRequest.ExecTest(server.Client);

        subCategory.Id.Should().BeGreaterThan(0);
        m_categoryIds.Add(subCategory.Id);

        var supplierRequest = new Supplier.Create
        {
            CompanyName = "Test company",
            Phone = Context.GeneratePhone(),
            Email = "e@mail.com",
            Address = "Test, Test 92"
        };

        var supplier = supplierRequest.ExecTest(server.Client);

        supplier.Id.Should().BeGreaterThan(0);
        m_supplierIds.Add(supplier.Id);

        var product = new Product.Create
        {
            SupplierId = supplier.Id,
            CategoryId = subCategory.Id,
            TaxId = 6,
            UPC = Context.GenerateUPC(),
            Name = "Test product",
            AgeLimit = 18,
            Quantity = new Random().Next(20, 200),
            BuyingPrice = 100,
            Price = 110,
            TotalPrice = 110,
            Margin = 1000,
            ExpiredDate = DateTime.Now.Date.AddDays(50)
        };

        var productResult = product.ExecTest(server.Client);

        productResult.Id.Should().BeGreaterThan(0);
        m_productIds.Add(productResult.Id);

        var request = new ProductDiscount.Create
        {
            ProductId = productResult.Id,
            Percent = 10,
            Comment = "This test for discount product.",
            FromDate = DateTimeOffset.Now.AddDays(-5),
            ToDate = DateTimeOffset.Now.AddDays(5)
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_productDiscountIds.Add(result.Id);

        result.UPC.Should().Be(productResult.UPC);
        result.ProductName.Should().Be(productResult.Name);
        result.Category.Should().Be(category.Name);
        result.SubCategory.Should().Be(subCategory.Name);
        result.Comment.Should().Be(request.Comment);
        result.Percent.Should().Be(request.Percent);
        result.SubCategory.Should().NotBeNullOrWhiteSpace();
        result.FromDate.Should().Be(request.FromDate);
        result.ToDate.Should().Be(request.ToDate);
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

        var categoryRequest = new Category.Create
        {
            Name = "Test category product"
        };

        var category = categoryRequest.ExecTest(server.Client);

        category.Id.Should().BeGreaterThan(0);
        m_categoryIds.Add(category.Id);

        var subCategoryRequest = new Category.Create
        {
            Name = "Test sub category product",
            ParentId = category.Id
        };

        var subCategory = subCategoryRequest.ExecTest(server.Client);

        subCategory.Id.Should().BeGreaterThan(0);
        m_categoryIds.Add(subCategory.Id);

        var supplierRequest = new Supplier.Create
        {
            CompanyName = "Test company",
            Phone = Context.GeneratePhone(),
            Email = "e@mail.com",
            Address = "Test, Test 92"
        };

        var supplier = supplierRequest.ExecTest(server.Client);

        supplier.Id.Should().BeGreaterThan(0);
        m_supplierIds.Add(supplier.Id);

        var product = new Product.Create
        {
            SupplierId = supplier.Id,
            CategoryId = subCategory.Id,
            TaxId = 6,
            UPC = Context.GenerateUPC(),
            Name = "Test product",
            AgeLimit = 18,
            Quantity = new Random().Next(20, 200),
            BuyingPrice = 100,
            Price = 110,
            TotalPrice = 110,
            Margin = 1000,
            ExpiredDate = DateTime.Now.Date.AddDays(50)
        };

        var productResult = product.ExecTest(server.Client);

        productResult.Id.Should().BeGreaterThan(0);
        m_productIds.Add(productResult.Id);

        var request = new ProductDiscount.Create
        {
            ProductId = productResult.Id,
            Percent = 10,
            Comment = "This test for discount product.",
            FromDate = DateTimeOffset.Now.AddDays(-5),
            ToDate = DateTimeOffset.Now.AddDays(5)
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_productDiscountIds.Add(result.Id);

        var updateRequest = new ProductDiscount.Update
        {
            Id = result.Id,
            Percent = 12,
            Comment = "Update " + request.Comment,
            FromDate = result.FromDate.AddDays(2),
            ToDate = request.ToDate.AddDays(2)
        };

        var updateResult = updateRequest.ExecTest(server.Client);

        updateResult.Id.Should().Be(result.Id);
        updateResult.Percent.Should().Be(updateRequest.Percent);
        updateResult.Comment.Should().Be(updateRequest.Comment);
        updateResult.FromDate.Should().Be(updateRequest.FromDate);
        updateResult.ToDate.Should().Be(updateRequest.ToDate);
        updateResult.Category.Should().Be(category.Name);
        updateResult.SubCategory.Should().Be(subCategory.Name);
        updateResult.ProductName.Should().Be(product.Name);
        updateResult.UPC.Should().Be(product.UPC);
        updateResult.CreatedAt.Should().Be(result.CreatedAt);
        updateResult.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        updateResult.Timestamp.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void Search()
    {
        using var server = new Starter().TestLogin();

        InitCleaner(server.Sql);

        var categoryRequest = new Category.Create
        {
            Name = "Test category product"
        };

        var category = categoryRequest.ExecTest(server.Client);

        category.Id.Should().BeGreaterThan(0);
        m_categoryIds.Add(category.Id);

        var subCategoryRequest = new Category.Create
        {
            Name = "Test sub category product",
            ParentId = category.Id
        };

        var subCategory = subCategoryRequest.ExecTest(server.Client);

        subCategory.Id.Should().BeGreaterThan(0);
        m_categoryIds.Add(subCategory.Id);

        var supplierRequest = new Supplier.Create
        {
            CompanyName = "Test company",
            Phone = Context.GeneratePhone(),
            Email = "e@mail.com",
            Address = "Test, Test 92"
        };

        var supplier = supplierRequest.ExecTest(server.Client);

        supplier.Id.Should().BeGreaterThan(0);
        m_supplierIds.Add(supplier.Id);

        var product = new Product.Create
        {
            SupplierId = supplier.Id,
            CategoryId = subCategory.Id,
            TaxId = 6,
            UPC = Context.GenerateUPC(),
            Name = "Test product",
            AgeLimit = 18,
            Quantity = new Random().Next(20, 200),
            BuyingPrice = 100,
            Price = 110,
            TotalPrice = 110,
            Margin = 1000,
            ExpiredDate = DateTime.Now.Date.AddDays(50)
        };

        var productResult = product.ExecTest(server.Client);

        productResult.Id.Should().BeGreaterThan(0);
        m_productIds.Add(productResult.Id);

        for (int i = 0; i < 5; i++)
        {
            var request = new ProductDiscount.Create
            {
                ProductId = productResult.Id,
                Percent = 10,
                Comment = "This test for discount product.",
                FromDate = DateTimeOffset.Now.AddDays(-5),
                ToDate = DateTimeOffset.Now.AddDays(5)
            };

            var result = request.ExecTest(server.Client);

            result.Id.Should().BeGreaterThan(0);
            m_productDiscountIds.Add(result.Id);
        }

        var searchRequest = new ProductDiscount.Search
        {
            Ids = m_productDiscountIds.ToArray()
        };

        var searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Total.Should().Be(5);
        searchResult.Items.Count.Should().Be(5);
        foreach (var item in searchResult.Items)
        {
            m_productDiscountIds.Should().Contain(item.Id);
        }

        searchRequest = new ProductDiscount.Search
        {
            Filters = new ProductDiscount.Search.Filter
            {
                UPC = "1234567890"
            }
        };

        searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Count.Should().Be(0);

        searchRequest = new ProductDiscount.Search
        {
            PageInfo = new PageInfo
            {
                PageNumber = 100000,
                PageSize = 10
            }
        };

        searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Count.Should().Be(0);
    }
}