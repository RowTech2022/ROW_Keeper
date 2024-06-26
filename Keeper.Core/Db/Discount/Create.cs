using System.Data;
using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;
using Keeper.Client;

namespace Keeper.Core;

public partial class Db
{
    public partial class Discount
    {
        [BindStruct]
        public class Create
        {
            [Bind("ReqUserId")]
            public int ReqUserId { get; set; }
            
            [Bind("ProductId")]
            public int? ProductId { get; set; }
            
            [Bind("CategoryId")]
            public int? CategoryId { get; set; }
            
            [Bind("Percent")]
            public double Percent { get; set; }
            
            [NVarChar("Comment", 5000)]
            public string? Comment { get; set; }
            
            [Bind("FromDate")]
            public DateTimeOffset FromDate { get; set; }
            
            [Bind("ToDate")]
            public DateTimeOffset ToDate { get; set; }
            
            [Bind("Type")] 
            public DiscountType Type { get; set; }

            [Bind("ResultId", Direction = ParameterDirection.Output)]
            public int ResultId { get; set; }

            #region c_query

            private const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()

insert into [new-keeper].[ProductDiscounts] (
     [ReqUserId]
    ,[ProductId]
    ,[CategoryId]
    ,[Percent]
    ,[Comment]
    ,[FromDate]
    ,[ToDate]
    ,[Type]
    ,[CreatedAt]
    ,[UpdatedAt] )
select
    @ReqUserId,
    @ProductId,
    @CategoryId,
    @Percent,
    @Comment,
    @FromDate,
    @ToDate,
    @Type,
    @now,
    @now

set @ResultId = @@identity
";

            #endregion

            public int Ecec(ISqlExecutor sql)
            {
                sql.Query(c_query, this);

                return ResultId;
            }
        }
    }
}