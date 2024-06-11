using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Supplier
    {
        public class Search
        {
            [NVarChar("CompanyName", 500)]
            public string? CompanyName { get; set; }

            public int? Count { get; set; }
            public int? Start { get; set; }
            
            public class Result
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

                [Bind("Total")]
                public int Total { get; set; }
            }

            #region c_query

            private const string c_query = @"
select
{topPaging}
     s.[Id]
    ,s.[CompanyName]
    ,s.[Phone]
    ,s.[Email]
    ,s.[Address]
    ,count(*) over() as [Total]
from [new-keeper].[Suppliers] as s
where
    
    --{CompanyName - start}
    lower(s.[CompanyName]) like lower(N'%' + @CompanyName + '%') and
    --{CompanyName - end}
    
    1 = 1

order by s.[Id] desc
{offsetPaging}
";

            #endregion

            public List<Result> Exec(ISqlExecutor sql)
            {
                var query = GetQuery();

                return sql.Query<Result>(query, this).ToList();
            }

            private string GetQuery()
            {
                var query = c_query;

                query = SqlQueriesFormater.Page("topPaging", "offsetPaging")
                    .Top(Count).Offset(Start)
                    .Format(query);

                return query;
            }
        }
    }
}