using System.Data;
using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Plan
    {
        [BindStruct]
        public class Update
        {
            [Bind("Id")] 
            public int Id { get; set; }

            [NVarChar("Name", 500)] 
            public string Name { get; set; } = null!;

            [Bind("Price")] 
            public decimal Price { get; set; }

            [Bind("Duration")] 
            public int Duration { get; set; }

            [Bind("Type")]
            public Client.Plan.PlanType Type { get; set; }

            [Bind("Active")]
            public bool Active { get; set; }

            [Bind("ResultCount", Direction = ParameterDirection.Output)]
            public int ResultCount { get; set; }

            public string[] UpdateList { get; set; } = null!;

            #region c_query

            private const string c_query = @"
{update}
where
    [Id] = @Id and
    1 = 1

set @ResultCount = @@rowcount
";

            #endregion

            #region c_updateList

            private static HashSet<string> c_updateList =
            [
                nameof(Name),
                nameof(Price),
                nameof(Duration),
                nameof(Type),
                nameof(Active)
            ];

            #endregion

            private IEnumerable<string> GetDefaultUpdateList()
            {
                yield return nameof(Name);
                yield return nameof(Price);
                yield return nameof(Duration);
                yield return nameof(Type);
            }

            public void SetDefaultUpdateList()
            {
                UpdateList = GetDefaultUpdateList().ToArray();
            }

            public void Exec(ISqlExecutor sql)
            {
                var query = GetQuery();
                
                sql.Query(query, this);

                if (ResultCount == 0)
                    throw new Exception("The plan cannot be wrote because it changed.");
            }

            private string GetQuery()
            {
                var query = c_query;

                var updatePart = new SqlUpdateQueryFormater(this, "[new-keeper].[Plans]")
                    .AddUpdateList(UpdateList.Where(c_updateList.Contains).ToArray())
                    .AddNowList("UpdatedAt")
                    .Query();

                if (updatePart == null)
                    throw new Exception();

                query = SqlQueriesFormater.ReplaceConst(query, "update", updatePart);

                query = SqlQueriesFormater.RemoveAllNullSections(query, this);

                return SqlQueriesFormater.RemoveLabels(query);
            }
        }
    }
}