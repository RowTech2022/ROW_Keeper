using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Product
    {
        [BindStruct]
        public class CheckUPC
        {
            [NVarChar("UPC", 100)]
            public string UPC { get; set; } = null!;

            [Bind("BranchId")]
            public int BranchId { get; set; }
            
            [BindStruct]
            public class Result
            {
                [Bind("Id")]
                public int Id { get; set; }
    
                [NVarChar("UPC", 100)]
                public string UPC { get; set; } = null!;
    
                [NVarChar("Name", 500)]
                public string Name { get; set; } = null!;
            }

            #region c_query

            private const string c_query = @"
select
     p.[Id]
    ,p.[UPC]
    ,p.[Name]
from [new-keeper].[Products] as p
join [new-keeper].[Categories] as c on p.[CategoryId] = c.[Id]
where
    
    p.[UPC] = @UPC and
    
    p.[BranchId] = @BranchId and
    
    1 = 1
";

            #endregion

            public List<Result> Exec(ISqlExecutor sql)
            {
                return sql.Query<Result>(c_query, this).ToList();
            }
        }
    }
}