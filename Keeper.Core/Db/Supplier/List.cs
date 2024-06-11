using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Supplier
    {
        public class List(params int[] ids)
        {
            public int[] Ids { get; set; } = ids;
            
            public class Result : BaseProp
            {
                [Bind("Id")]
                public int Id { get; set; }

                [NVarChar("CompanyName", 500)]
                public string CompanyName { get; set; } = null!;

                [NVarChar("Phone", 20)]
                public string Phone { get; set; } = null!;
    
                [NVarChar("Email", 100)]
                public string? Email { get; set; }

                [NVarChar("Address", 300)]
                public string? Address { get; set; }
            }

            #region c_query

            private const string c_query = @"
select 
     s.[Id]
    ,s.[CompanyName]
    ,s.[Phone]
    ,s.[Email]
    ,s.[Address]
    ,s.[CreatedAt]
    ,s.[UpdatedAt]
    ,s.[Timestamp]
from [new-keeper].[Suppliers] as s
where
    
    --{Ids - start}
    s.[Id] in ({Ids}) and
    --{Ids - end}
    
    s.[Active] = 1 and
    
    1 = 1
";

            #endregion

            public List<Result> Exec(ISqlExecutor sql)
            {
                var query = GetQuery();

                return sql.Query<Result>(query).ToList();
            }
            
            private string GetQuery()
            {
                var query = c_query;

                query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(", ", x)).Format(query);

                return query;
            }
        }
    }
}