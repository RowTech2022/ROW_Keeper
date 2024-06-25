using System.Data;
using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Organization
    {
        [BindStruct]
        public class Update
        {
            [Bind("Id")]
            public int Id { get; set; }
            
            [Bind("OwnerId")]
            public int OwnerId { get; set; }

            [NVarChar("OrgName", 500)]
            public string OrgName  { get; set;} = null!;

            [NVarChar("OrgDescription", 5000)]
            public string? OrgDescription  { get; set;} = null!;

            [NVarChar("OrgPhone", 20)]
            public string OrgPhone { get; set;} = null!;

            [NVarChar("OrgEmail", 20)]
            public string? OrgEmail { get; set;}

            [NVarChar("OrgAddress", 500)] 
            public string OrgAddress { get; set; } = null!;

            [NVarChar("OwnerFullName", 100)]
            public string OwnerFullName { get; set; } = null!;

            [NVarChar("OwnerEmail", 100)]
            public string? OwnerEmail { get; set; } = null!;

            [NVarChar("OwnerPhone", 20)]
            public string OwnerPhone { get; set; } = null!;

            [Bind("Status")]
            public Client.Organization.OrgStatus Status { get; set; }

            [Bind("Active")]
            public bool Active { get; set; }

            [Bind("ResultCount", Direction = ParameterDirection.Output)]
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
                    nameof(OrgName),
                    nameof(OrgDescription),
                    nameof(OrgPhone),
                    nameof(OrgEmail),
                    nameof(OrgAddress),
                    nameof(OwnerFullName),
                    nameof(OwnerEmail),
                    nameof(OwnerPhone),
                    nameof(Status),
                    nameof(Active),
                ];

            #endregion

            private static IEnumerable<string> GetDefaultUpdationList()
            {
                yield return nameof(OwnerId);
                yield return nameof(OrgName);
                yield return nameof(OrgDescription);
                yield return nameof(OrgPhone);
                yield return nameof(OrgEmail);
                yield return nameof(OrgAddress);
                yield return nameof(OwnerFullName);
                yield return nameof(OwnerPhone);
                yield return nameof(OwnerEmail);
                yield return nameof(Status);
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
                    throw new Exception("The organization cannot be wrote because it changed.");
            }

            private string GetQuery()
            {
                var query = c_query;

                var updatePart = new SqlUpdateQueryFormater(this, "[new-keeper].[Organizations]")
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