using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class OrganizationBranch
    {
        [BindStruct]
        public class Create
        {
            [Bind("ReqUserId")]
            public int ReqUserId { get; set; }

            [Bind("OwnerId")]
            public int OwnerId { get; set; }

            [NVarChar("BranchName", 500)]
            public string BranchName  { get; set;} = null!;

            [NVarChar("BranchPhone", 20)]
            public string BranchPhone { get; set;} = null!;

            [NVarChar("BranchEmail", 20)]
            public string? BranchEmail { get; set;} = null!;

            [NVarChar("BranchAddress", 500)] 
            public string BranchAddress { get; set; } = null!;

            [Bind("ResultId", Direction = System.Data.ParameterDirection.Output)]
            public int ResultId { get; set; } 

            #region c_query

            const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()

insert into [new-keeper].[OrganizationBranches]
        (
         [ReqUserId]
        ,[OwnerId]
        ,[BranchName]
        ,[BranchPhone]
        ,[BranchEmail]
        ,[BranchAddress]
        ,[CreatedAt]
        ,[UpdatedAt]
        )
select
        @ReqUserId,
        @OwnerId,
        @BranchName,
        @BranchPhone,
        @BranchEmail,
        @BranchAddress,       
        @now,
        @now

 set @ResultId = @@identity
";
            #endregion c_query

            public int Exec(ISqlExecutor sql)
            {
                sql.Query(c_query, this);
                return ResultId;
            }
        }
    }
}