using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Subscription
    {
        [BindStruct]
        public class Update
        {
            [Bind("Id")]
            public int Id { get; set; }
            
            [Bind("OrgId")]
            public int OrgId { get; set; }
            
            [Bind("PlanId")]
            public int PlanId { get; set; }
            
            [Bind("StartDate")]
            public DateTimeOffset StartDate { get; set; }
            
            [Bind("EndDate")] 
            public DateTimeOffset EndDate { get; set; }

            [Bind("Active")]
            public bool Active { get; set; }

            [Bind("ResultCount")]
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
                nameof(OrgId),
                nameof(PlanId),
                nameof(StartDate),
                nameof(EndDate),
                nameof(Active)
            ];

            #endregion

            private IEnumerable<string> GetDefaultUpdateList()
            {
                yield return nameof(OrgId);
                yield return nameof(PlanId);
                yield return nameof(StartDate);
                yield return nameof(EndDate);
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
                    throw new Exception("The product cannot be wrote because it changed.");
            }

            private string GetQuery()
            {
                var query = c_query;

                var updatePart = new SqlUpdateQueryFormater(this, "[new-keeper].[Subscription")
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