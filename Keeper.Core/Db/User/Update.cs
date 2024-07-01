using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;
using Keeper.Client;

namespace Keeper.Core
{
    public partial class Db
    {
        public partial class User
        {
            [BindStruct]
            public class Update
            {
				[Bind("Id")]
				public int Id  { get; set;}

				[Bind("OrgId")]
				public int OrgId { get; set; }

				[NVarChar("FullName", 50)] 
				public string FullName { get; set; } = null!;

				[Bind("UserType")]
				public UserType UserType { get; set; }

				[NVarChar("Phone", 20)]
				public string Phone  { get; set;} = null!;

				[NVarChar("Email", 100)]
				public string? Email  { get; set;}

				[NVarChar("Login", 50)] 
				public string Login { get; set; } = null!;

				[Bind("State")]
				public Client.User.Status State { get; set; }
				
				[Bind("PasswordHash")]
				public byte[]? PasswordHash  { get; set;}  
				
				[Bind("Active")] 
				public bool Active { get; set; }

				[Bind("Timestamp")]
				public byte[]? Timestamp  { get; set;} 

				[Bind("ResultCount", Direction = System.Data.ParameterDirection.Output)]
				public int ResultCount { get; set; } 


				public string[]? UpdationList { get; set; }

				#region c_query

				const string c_query = @"
{update}
where
     [Id]=@Id AND

     1 = 1

---

 set @ResultCount = @@rowcount
";
				#endregion c_query

				#region s_updationList

				static HashSet<string> s_updationList = new HashSet<string>(
					new[]
					{
						nameof(OrgId),
						nameof(FullName),
						nameof(UserType),
						nameof(Phone),
						nameof(Email),
						nameof(Login),
						nameof(State),
						nameof(PasswordHash),
						nameof(Active)
					},
					StringComparer.InvariantCultureIgnoreCase);

				#endregion

				private static IEnumerable<string> GetDefaultUpdionlist()
				{
				    yield return nameof(OrgId);
				    yield return nameof(FullName);
				    yield return nameof(UserType);
				    yield return nameof(Phone);
				    yield return nameof(Email);
				    yield return nameof(Login);
				    yield return nameof(State);
				    yield return nameof(PasswordHash);
				}

				public Update SetDefaultUpdionlist()
				{
				    UpdationList = GetDefaultUpdionlist().ToArray();
				    return this;
				}

				public void Exec(ISqlExecutor sqlScope)
				{
				    var tQuery = GetQuery();
				    if (tQuery != null)
				         sqlScope.Query(tQuery, this);

				    if (ResultCount == 0)
				         throw new Exception("The Contract cannot be wrote because it changed.");
				}

				string? GetQuery()
				{
				    var query = c_query;

				    var updatePart = new SqlUpdateQueryFormater(this, "[new-keeper].[Users]")
				         .AddUpdateList(UpdationList?.Where(x => s_updationList.Contains(x)).ToArray())
				         .AddNowList("UpdatedAt")
				         .Query();

				    if (updatePart == null)
				         return null;

				    query = SqlQueriesFormater.ReplaceConst(query, "update", updatePart);

				    query = SqlQueriesFormater.RemoveAllNullSections(query, this);

				    return SqlQueriesFormater.RemoveLabels(query);

				}

            }
        }
    }
}
