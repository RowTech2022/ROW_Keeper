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

				[NVarChar("OrgDescription", 500)]
				public string? OrgDescription  { get; set;} = null!;

				[NVarChar("OrgPhone", 20)]
				public string OrgPhone { get; set;} = null!;

				[NVarChar("OrgEmail", 20)]
				public string? OrgEmail { get; set;}

				[NVarChar("OrgAddress", 5000)] 
				public string OrgAddress { get; set; } = null!;

				[NVarChar("OwnerFullName", 100)]
				public string OwnerFullName { get; set; } = null!;

				[NVarChar("OwnerEmail", 100)]
				public string? OwnerEmail { get; set; } = null!;

				[NVarChar("OwnerPhone", 20)]
				public string OwnerPhone { get; set; } = null!;

				[Bind("Status")]
				public Client.Organization.OrgStatus Status { get; set; }

				[Bind("ResultId", Direction = System.Data.ParameterDirection.Output)]
				public int ResultId { get; set; } 

				#region c_query

				const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()

---

insert into [new-keeper].[Organizations]
        (
         [ReqUserId]
        ,[OwnerId]
        ,[OrgName]
        ,[OrgDescription]
        ,[OrgPhone]
        ,[OrgEmail]
        ,[OrgAddress]
        ,[OwnerFullName]
        ,[OwnerEmail]
        ,[OwnerPhone]
        ,[Status]
        ,[CreatedAt]
        ,[UpdatedAt]
        )
select
        @ReqUserId,
        @OwnerId,
        @OrgName,
        @OrgDescription,
        @OrgPhone,
        @OrgEmail,
        @OrgAddress,    
        @OwnerFullName,
        @OwnerEmail,
        @OwnerPhone,
        @Status,
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
