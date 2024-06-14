using System.Data;
using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Supplier
    {
        [BindStruct]
        public class Update
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
                nameof(CompanyName),
                nameof(Phone),
                nameof(Email),
                nameof(Address),
                nameof(Active)
            ];

            #endregion

            private IEnumerable<string> GetDefaultUpdateList()
            {
                yield return nameof(CompanyName);
                yield return nameof(Phone);
                yield return nameof(Email);
                yield return nameof(Address);
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
                    throw new Exception("The supplier cannot be wrote because it changed.");
            }

            private string GetQuery()
            {
                var query = c_query;

                var updatePart = new SqlUpdateQueryFormater(this, "[new-keeper].[Suppliers]")
                    .AddUpdateList(UpdateList.Where(c_updateList.Contains).ToArray())
                    .AddNowList("UpdateAt")
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