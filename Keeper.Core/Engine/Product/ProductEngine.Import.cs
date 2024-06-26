using System.Net;
using Keeper.Client.Product;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Row.Common1.Client1;
using File = System.IO.File;

namespace Keeper.Core;

public partial class ProductEngine
{
    public async Task<List<Product.Update>> ImportProduct(IFormFile file)
    {
        using var stream = new MemoryStream();

        await file.CopyToAsync(stream);

        const string url = @"D:\Книга2.xlsx";

        var fileInfo = new FileInfo(url);

        using var package = new ExcelPackage(fileInfo);

        await using var usageExcel = File.Open(url, FileMode.Open);

        var workSheet = package.Workbook.Worksheets.FirstOrDefault();

        if (workSheet == null)
            throw new ApiException("Empty file", HttpStatusCode.BadRequest);

        workSheet.Cells[1, 1].Value = "Ina nav dobavit kardem";
        
        // await package.LoadAsync(usageExcel);

        // var file2 = new FileInfo(@"D:\Книга2.xlsx");
        // await package.SaveAsAsync(file2);
        
        await package.SaveAsync();

        int row = 3;
        int col = 2;

        var updateList = new List<Product.Update>();

        while (!string.IsNullOrWhiteSpace(workSheet.Cells[row, col].Value?.ToString()))
        {
            var update = new Product.Update
            {
                UPC = workSheet.Cells[row, col].Value.ToString()!,
                Name = workSheet.Cells[row, col + 1].Value.ToString()!,
                Quantity = Convert.ToInt32(workSheet.Cells[row, col + 2].Value.ToString()!),
                Price = Convert.ToDecimal(workSheet.Cells[row, col + 3].Value.ToString()!),
                ExpiredDate = Convert.ToDateTime(workSheet.Cells[row, col + 4].Value.ToString()!),
            };

            workSheet.Cells[row, col + 5].Value = 10 + row;

            updateList.Add(update);

            row++;
        }

        await package.SaveAsync();

        return updateList;
    }
}