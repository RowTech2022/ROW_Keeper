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
            public class Create
            {
				[Bind("ReqUserId")]
				public int ReqUserId  { get; set;}

				[Bind("BranchId")]
				public int BranchId  { get; set;}

				[NVarChar("Name", 50)]
				public string Name { get; set; } = null!;

				[NVarChar("Surname", 50)]
				public string Surname { get; set; } = null!;

				[Bind("UserType")]
				public UserType UserType { get; set;}

				[NVarChar("Phone", 20)]
				public string Phone { get; set; } = null!;

				[NVarChar("Email", 100)]
				public string? Email  { get; set;}

				[NVarChar("Login", 50)] 
				public string Login { get; set; } = null!;

				[Bind("ResultId", Direction = System.Data.ParameterDirection.Output)]
				public int ResultId { get; set; } 

				#region c_query

				const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()

---

INSERT INTO [new-keeper].[Users]
        (
         [ReqUserId]
        ,[BranchId]
        ,[Name]
        ,[Surname]
        ,[Phone]
        ,[Email]
        ,[UserType]
        ,[Login]
        ,[CreatedAt]
        ,[UpdatedAt]
        )
select
        @ReqUserId,
        @BranchId,
        @Name,
        @Surname,
        @Phone,
        @Email,
        @UserType,
        @Login,
        @now,
        @now        

---

 set @ResultId = @@identity
";
				#endregion c_query

				public int Exec(ISqlExecutor scopeSql)
				{
				    scopeSql.Query(c_query, this);
				    return ResultId;
				}
            }
        }
    }
}
