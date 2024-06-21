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
            public class List
            {
                public int[] Ids { get; set; }

                public List() { }
                public List(params int[] ids)
                {
                    Ids = ids;
                }
                
                [BindStruct]
                public class Result
                {
                    [Bind("Id")]
                    public int Id { get; set; }

                    [NVarChar("FullName", 50)] 
                    public string FullName { get; set; } = null!;

                    [Bind("UserType")]
                    public UserType UserType { get; set; }

                    [NVarChar("Email", 500)]
                    public string? Email { get; set; }

                    [NVarChar("Phone", 20)]
                    public string Phone { get; set; } = null!;

                    [NVarChar("Login", 50)]
                    public string Login { get; set; } = null!;

                    [Bind("Status")]
                    public Client.User.Status Status { get; set; }

                    [Bind("PasswordHash")]
                    public byte[]? PasswordHash { get; set; }

                    [Bind("CreatedAt")]
                    public DateTimeOffset CreatedAt { get; set; }

                    [Bind("UpdatedAt")]
                    public DateTimeOffset UpdatedAt { get; set; }

                    [Bind("Timestamp")] 
                    public byte[] Timestamp { get; set; } = null!;

                }

                #region c_query

                const string c_query = @"
select
      u.[Id]
     ,u.[FullName]
     ,u.[UserType]
     ,u.[Phone]
     ,u.[Email]
     ,u.[Login]
     ,u.[Status]
     ,u.[PasswordHash]
     ,u.[CreatedAt]
     ,u.[UpdatedAt]
     ,u.[Timestamp]
FROM [new-keeper].[Users] as u
WHERE
    --{Ids - start}
    u.Id in ({Ids}) and
    --{Ids - end}
    
    u.[Active] = 1 and

    1=1 

";

                #endregion c_query

                public List<Result> Exec(ISqlExecutor sql)
                {
                    var query = GetQuery();

                    return sql.Query<Result>(query, this).ToList();
                }

                private string GetQuery()
                {
                    var query = c_query;

                    query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(",", x)).Format(query);

                    return query;
                }
            }
        }
    }
}
