using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class OrganizationBranch
    {
        [BindStruct]
        public class Update
        {
            [Bind("Id")]
            public int Id { get; set; }
            
            [Bind("OwnerId")]
            public int OwnerId { get; set; }

            [NVarChar("BranchName", 500)]
            public string BranchName  { get; set;} = null!;

            [NVarChar("BranchPhone", 20)]
            public string BranchPhone { get; set;} = null!;

            [NVarChar("BranchEmail", 20)]
            public string BranchEmail { get; set;} = null!;

            [NVarChar("BranchAddress", 500)] 
            public string BranchAddress { get; set; } = null!;

            [Bind("Active")]
            public bool Active { get; set; }

            [Bind("ResultCount", Direction = System.Data.ParameterDirection.Output)]
            public int ResultCount { get; set; }

            public string[] UpdationList { get; set; } = null!;
            
            #region c_query

            private const string c_query = @"
{update}
where
    [Id] = @Id and
    1 = 1

set @ResultCount = @@rowcount
";

            #endregion

            #region c_updationList

            private static HashSet<string> c_updationList = 
                [
                    nameof(OwnerId),
                    nameof(BranchName),
                    nameof(BranchPhone),
                    nameof(BranchEmail),
                    nameof(BranchAddress),
                    nameof(Active),
                ];

            #endregion

            private static IEnumerable<string> GetDefaultUpdationList()
            {
                yield return nameof(OwnerId);
                yield return nameof(BranchName);
                yield return nameof(BranchPhone);
                yield return nameof(BranchEmail);
                yield return nameof(BranchAddress);
            }

            public void SetDefaultUpdationList()
            {
                UpdationList = GetDefaultUpdationList().ToArray();
            }

            public void Exec(ISqlExecutor sql)
            {
                var query = GetQuery();

                sql.Query(query, this);

                if (ResultCount == 0)
                    throw new Exception("The Contract cannot be wrote because it changed.");
            }

            private string GetQuery()
            {
                var query = c_query;

                var updatePart = new SqlUpdateQueryFormater(this, "[new-keeper].[OrganizationBranches]")
                    .AddUpdateList(UpdationList.Where(c_updationList.Contains).ToArray())
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