using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class OrganizationBranch
    {
        public class List(int[] ids)
        {
            public int[] Ids { get; set; } = ids;

            public class Result : BaseProp
            {
                [Bind("Id")]
                public int Id { get; set; }
                
                [Bind("OwnerId")]
                public int OwnerId { get; set; }
                
                [NVarChar("BranchName", 500)]
                public string BranchName { get; set; } = null!;
                
                [NVarChar("BranchPhone", 20)]
                public string BranchPhone { get; set; } = null!;
                
                [NVarChar("BranchAddress", 500)]
                public string BranchAddress { get; set; } = null!;
            }

            #region c_query

            private const string Query = @"
select 
     o.[Id]
    ,o.[OwnerId]
    ,o.[BranchName]
    ,o.[BranchPhone]
    ,o.[BranchAddress]
    ,o.[Active]
    ,o.[CreatedAt]
    ,o.[UpdatedAt]
    ,o.[Timestamp]
from [new-keeper].[OrganizationBranches] as o
where
    
    --{Ids - start}
    o.[Id] in ({Ids}) and
    --{Ids - end}
    
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
                var query = Query;

                query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(", ", x))
                    .Format(query);

                return query;
            }
        }
    }
}