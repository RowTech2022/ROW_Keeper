using System.Data;
using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Product
    {
        [BindStruct]
        public class Create
        {
            [Bind("ReqUserId")]
            public int ReqUserId { get; set; }
            
            [Bind("BranchId")]
            public int BranchId { get; set; }

            [Bind("SupplierId")]
            public int SupplierId { get; set; }

            [Bind("CategoryId")]
            public int CategoryId { get; set; }

            [Bind("TaxId")]
            public int TaxId { get; set; }

            [NVarChar("UPC", 100)]
            public string UPC { get; set; } = null!;
    
            [NVarChar("Name", 500)]
            public string Name { get; set; } = null!;

            [Bind("AgeLimit")]
            public int AgeLimit { get; set; }

            [Bind("Quantity")]
            public int Quantity { get; set; }

            [Bind("BuyingPrice")]
            public decimal BuyingPrice { get; set; }

            [Bind("Price")]
            public decimal Price { get; set; }

            [Bind("TotalPrice")]
            public decimal TotalPrice { get; set; }

            [Bind("Margin")]
            public int Margin { get; set; }

            [Bind("ExpiredDate")]
            public DateTimeOffset? ExpiredDate { get; set; }
            
            [Bind("ResultId", Direction = ParameterDirection.Output)] 
            public int ResultId { get; set; }

            #region c_query

            private const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()

insert into [new-keeper].[Products] (
     [ReqUserId]
    ,[BranchId]
    ,[SupplierId]
    ,[CategoryId]
    ,[TaxId]
    ,[UPC]
    ,[Name]
    ,[AgeLimit]
    ,[Quantity]
    ,[BuyingPrice]
    ,[Price]
    ,[DiscountPrice]
    ,[TotalPrice]
    ,[Margin]
    ,[ExpiredDate]
    ,[CreatedAt]
    ,[UpdatedAt] )
select
    @ReqUserId,
    @BranchId,
    @SupplierId,
    @CategoryId,
    @TaxId,
    @UPC,
    @Name,
    @AgeLimit,
    @Quantity,
    @BuyingPrice,
    @Price,
    @DiscountPrice,
    @TotalPrice,
    @Margin,
    @ExpiredDate,
    @now,
    @now

set @ResultId = @@identity
";

            #endregion

            public int Exec(ISqlExecutor sql)
            {
                sql.Query(c_query, this);
                
                return ResultId;
            }
        }
    }
}