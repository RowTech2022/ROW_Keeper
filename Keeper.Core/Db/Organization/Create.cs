using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core
{
    public partial class Db
    {
        public partial class Organization
        {
            [BindStruct]
            public class Create
            {
				[Bind("ReqUserId")]
				public int ReqUserId { get; set; }

				[Bind("OwnerId")]
				public int OwnerId { get; set; }

				[NVarChar("OrgName", 500)]
				public string OrgName  { get; set;} = null!;

				[NVarChar("OrgPhone", 20)]
				public string OrgPhone { get; set;} = null!;

				[NVarChar("OrgEmail", 20)]
				public string? OrgEmail { get; set;}

				[NVarChar("OrgAddress", 500)] 
				public string OrgAddress { get; set; } = null!;

				[Bind("ResultId", Direction = System.Data.ParameterDirection.Output)]
				public int ResultId { get; set; } 

				#region c_query

				const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()

---

INSERT INTO [new-keeper].[Organizations]
        (
         [ReqUserId]
        ,[OwnerId]
        ,[OrgName]
        ,[OrgPhone]
        ,[OrgEmail]
        ,[OrgAddress]
        ,[CreatedAt]
        ,[UpdatedAt]
        )
select
        @ReqUserId,
        @OwnerId,
        @OrgName,
        @OrgPhone,
        @OrgEmail,
        @OrgAddress,       
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
