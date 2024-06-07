using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class OrganizationBranch
    {
        public class Search
        {
            [NVarChar("BranchName", 500)]
            public string BranchName { get; set; } = null!;

            public int? Count { get; set; }
            public int? Start { get; set; }
            
            public class Result
            {
                [Bind("Id")]
                public int Id { get; set; }
                
                [NVarChar("BranchName", 500)]
                public string BranchName  { get; set;} = null!;

                [NVarChar("BranchPhone", 20)]
                public string BranchPhone { get; set;} = null!;

                [NVarChar("BranchEmail", 20)]
                public string? BranchEmail { get; set;}

                [NVarChar("BranchAddress", 500)] 
                public string BranchAddress { get; set; } = null!;

                [Bind("Total")]
                public int Total { get; set; }
            }

            #region c_query

            private const string c_query = @"
select 
     b.[Id]
    ,b.[BranchName]
    ,b.[BranchPhone]
    ,b.[BranchEmail]
    ,b.[BranchAddress]
    ,count(*) over() as [Total]
from [new-keeper].[OrganizationBranches] as b
where
    
    --{BranchName - start}
    lower(b.[BranchName]) like lower(N'%' + @BranchName + '%') and
    --{BranchName - end}
    
    1 = 1
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