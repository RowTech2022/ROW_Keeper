using Bibliotekaen.Sql;
using FluentAssertions;
using Keeper.Client;
using Keeper.Client.Product;
using Row.Common.Dto1;

namespace Keeper.Test;

[TestClass]
public class ProductTest
{
    private readonly List<int> m_productIds = [];
    private readonly List<int> m_categoryIds = [];
    private readonly List<int> m_supplierIds = [];
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

        var request = new Product.Create
        {
            SupplierId = supplier.Id,
            CategoryId = category.Id,
            TaxId = 6,
            UPC = GenerateUPC(),
            Name = "Test product",
            AgeLimit = 18,
            Quantity = new Random().Next(20, 200),
            BuyingPrice = 100,
            Price = 110,
            DiscountPrice = 108,
            TotalPrice = 110,
            Margin = 1000,
            HaveDiscount = false,
            ExpiredDate = DateTime.Now.Date.AddDays(50)
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_productIds.Add(result.Id);

        result.SupplierId.Should().Be(request.SupplierId);
        result.CategoryName.Should().Be(category.Name);
        result.TaxId.Should().Be(request.TaxId);
        result.UPC.Should().Be(request.UPC);
        result.Name.Should().Be(request.Name);
        result.AgeLimit.Should().Be(request.AgeLimit);
        result.Quantity.Should().Be(request.Quantity);
        result.BuyingPrice.Should().Be(request.BuyingPrice);
        result.Price.Should().Be(request.Price);
        result.DiscountPrice.Should().Be(request.DiscountPrice);
        result.TotalPrice.Should().Be(request.TotalPrice);
        result.Margin.Should().Be(request.Margin);
        result.HaveDiscount.Should().Be(request.HaveDiscount);
        result.ExpiredDate.Should().Be(request.ExpiredDate);
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

        var request = new Product.Create
        {
            SupplierId = supplier.Id,
            CategoryId = category.Id,
            TaxId = 6,
            UPC = GenerateUPC(),
            Name = "Test product",
            AgeLimit = 18,
            Quantity = new Random().Next(20, 200),
            BuyingPrice = 100,
            Price = 110,
            DiscountPrice = 108,
            TotalPrice = 110,
            Margin = 1000,
            HaveDiscount = false,
            ExpiredDate = DateTime.Now.Date.AddDays(50)
        };

        var result = request.ExecTest(server.Client);

        result.Id.Should().BeGreaterThan(0);
        m_productIds.Add(result.Id);

        result.SupplierId.Should().Be(request.SupplierId);
        result.CategoryName.Should().Be(category.Name);
        result.TaxId.Should().Be(request.TaxId);
        result.UPC.Should().Be(request.UPC);
        result.Name.Should().Be(request.Name);
        result.AgeLimit.Should().Be(request.AgeLimit);
        result.Quantity.Should().Be(request.Quantity);
        result.BuyingPrice.Should().Be(request.BuyingPrice);
        result.Price.Should().Be(request.Price);
        result.DiscountPrice.Should().Be(request.DiscountPrice);
        result.TotalPrice.Should().Be(request.TotalPrice);
        result.Margin.Should().Be(request.Margin);
        result.HaveDiscount.Should().Be(request.HaveDiscount);
        result.ExpiredDate.Should().Be(request.ExpiredDate);
        result.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
        result.Timestamp.Should().NotBeNullOrEmpty();

        var updateRequest = new Product.Update
        {
            Id = result.Id,
            CategoryId = category.Id,
            TaxId = 10,
            UPC = GenerateUPC(),
            Name = "Update " + result.Name,
            AgeLimit = 18,
            Quantity = new Random().Next(20, 200),
            BuyingPrice = 200,
            Price = 220,
            DiscountPrice = 215,
            TotalPrice = 220,
            Margin = 200,
            HaveDiscount = true,
            ExpiredDate = DateTime.Now.Date.AddDays(30)
        };

        var updateResult = updateRequest.ExecTest(server.Client);
        
        updateResult.Id.Should().Be(updateRequest.Id);
        updateResult.SupplierId.Should().Be(request.SupplierId);
        updateResult.CategoryName.Should().Be(category.Name);
        updateResult.TaxId.Should().Be(updateRequest.TaxId);
        updateResult.UPC.Should().Be(updateRequest.UPC);
        updateResult.Name.Should().Be(updateRequest.Name);
        updateResult.AgeLimit.Should().Be(updateRequest.AgeLimit);
        updateResult.Quantity.Should().Be(updateRequest.Quantity);
        updateResult.BuyingPrice.Should().Be(updateRequest.BuyingPrice);
        updateResult.Price.Should().Be(updateRequest.Price);
        updateResult.DiscountPrice.Should().Be(updateRequest.DiscountPrice);
        updateResult.TotalPrice.Should().Be(updateRequest.TotalPrice);
        updateResult.Margin.Should().Be(updateRequest.Margin);
        updateResult.HaveDiscount.Should().Be(updateRequest.HaveDiscount);
        updateResult.ExpiredDate.Should().Be(updateRequest.ExpiredDate);
        updateResult.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromMinutes(1));
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

        var createList = new List<Product.Create>();

        for (int i = 0; i < 5; i++)
        {
            var request = new Product.Create
            {
                SupplierId = supplier.Id,
                CategoryId = category.Id,
                TaxId = 6,
                UPC = GenerateUPC(),
                Name = $"Test product_{i}",
                AgeLimit = 18,
                Quantity = new Random().Next(20, 200),
                BuyingPrice = 100,
                Price = 110,
                DiscountPrice = 108,
                TotalPrice = 110,
                Margin = 1000,
                HaveDiscount = false,
                ExpiredDate = DateTime.Now.Date.AddDays(50)
            };

            createList.Add(request);

            var result = request.ExecTest(server.Client);

            result.Id.Should().BeGreaterThan(0);
            m_productIds.Add(result.Id);
        }

        var searchRequest = new Product.Search
        {
            Ids = m_productIds.ToArray()
        };

        var searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Total.Should().Be(5);
        foreach (var item in searchResult.Items)
        {
            m_productIds.Should().Contain(item.Id);
            item.CategoryName.Should().Be(category.Name);
            createList.Select(x => x.UPC).Should().Contain(item.UPC);
            createList.Select(x => x.Name).Should().Contain(item.Name);
            createList.Select(x => x.AgeLimit).Should().Contain(item.AgeLimit);
            createList.Select(x => x.Quantity).Should().Contain(item.Quantity);
            createList.Select(x => x.Price).Should().Contain(item.Price);
            createList.Select(x => x.DiscountPrice).Should().Contain(item.DiscountPrice);
            createList.Select(x => x.ExpiredDate).Should().Contain(item.ExpiredDate);
            createList.Select(x => x.HaveDiscount).Should().Contain(item.HaveDiscount);
        }

        searchRequest = new Product.Search
        {
            PageInfo = new PageInfo
            {
                PageNumber = 1000
            }
        };

        searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Should().BeEmpty();

        searchRequest = new Product.Search
        {
            Filters = new Product.Search.Filter
            {
                NameOrUPC = "test_12345"
            }
        };

        searchResult = searchRequest.ExecTest(server.Client);

        searchResult.Total.Should().Be(0);
        searchResult.Items.Should().BeEmpty();
    }

    private static string GenerateUPC()
    {
        return "" +
               new Random().Next(0, 10) +
               new Random().Next(0, 10) +
               new Random().Next(0, 10) +
               new Random().Next(0, 10) +
               new Random().Next(0, 10) +
               new Random().Next(0, 10) +
               new Random().Next(0, 10) +
               new Random().Next(0, 10) +
               new Random().Next(0, 10) +
               new Random().Next(0, 10) +
               new Random().Next(0, 10) +
               new Random().Next(0, 10);
    }
}