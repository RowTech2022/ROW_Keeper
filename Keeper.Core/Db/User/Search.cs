using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;
using Keeper.Client;
using Row.Common.Dto1;

namespace Keeper.Core
{
    public partial class Db
    {
        public partial class User
        {
            [BindStruct]
            public class Search
            {
                public int[]? Ids { get; set; }
                public int? Start { get; set; }
                public int? Count { get; set; }
                
                [NVarChar("Email", 20)]
                public string? Email { get; set; }
                
                [NVarChar("Login", 20)]
                public string? Login { get; set; }
                public Client.User.Search.SearchByOrder? OrderColumn { get; set; }
                public SearchByDirection Direction { get; set; } = SearchByDirection.Desc;

                public Search() { }
                public Search(params int[] ids)
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
                    public string? Email { get; set; } = null!;

                    [NVarChar("Phone", 20)]
                    public string Phone { get; set; } = null!;

                    [NVarChar("Login", 50)] 
                    public string Login { get; set; } = null!;

                    [Bind("Status")]
                    public Client.User.Status Status { get; set; }

                    [Bind("Total")]
                    public int Total { get; set; }

                }

                #region c_query

                const string c_query = @"
select
     {topPaging}
      u.[Id]
     ,u.[Name]
     ,u.[Surname]
     ,u.[UserType]
     ,u.[Phone]
     ,u.[Email]
     ,u.[Login]
     ,u.[Status]
     ,COUNT(*) OVER() as Total
FROM [new-keeper].[Users] as u
WHERE
    --{Ids - start}
    u.Id in ({Ids}) and
    --{Ids - end}

    --{Login - start}
    u.[Login]  like N'%'+ @Login +'%'  and
    --{Login - end}

    --{Email - start}
    u.[Email]  like N'%'+ @Email +'%'  and
    --{Email - end}
    
    u.[Active] = 1 and

    1=1 

order by
    {orderBy}

{offsetPaging}
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

                    query = SqlQueriesFormater.Page("topPaging", "offsetPaging")
                        .Top(Count).Offset(Start)
                        .Format(query);

                    query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(",", x)).Format(query);

                    if (String.IsNullOrWhiteSpace(Login))
                        query = SqlQueriesFormater.RemoveSubString(query, "Login");
                    if (String.IsNullOrWhiteSpace(Email))
                        query = SqlQueriesFormater.RemoveSubString(query, "Email");

                    query = SetOrderBy(query);

                    return query;

                }

                private string SetOrderBy(string query)
                {
                    string orderBy = "";
                    if (OrderColumn == null)
                        orderBy = "u.Id {dir}";

                    switch (OrderColumn)
                    {
                        case Client.User.Search.SearchByOrder.Id:
                            orderBy = "u.Id {dir}";
                            break;
                    }

                    orderBy = SqlQueriesFormater.ReplaceConst(orderBy, "dir", Direction.ToString());

                    query = SqlQueriesFormater.ReplaceConst(query, "orderBy", orderBy);

                    return query;
                }
            }
        }
    }
}
