using System.Data;
using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Category
    {
        [BindStruct]
        public class Create
        {
            [Bind("ReqUserId")]
            public int ReqUserId { get; set; }

            [Bind("OrgId")]
            public int OrgId { get; set; }
    
            [Bind("ParentId")]
            public int? ParentId { get; set; }
    
            [NVarChar("Name", 300)]
            public string Name { get; set; } = null!;

            [Bind("ResultId", Direction = ParameterDirection.Output)]
            public int ResultId { get; set; }

            #region c_query

            private const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()

insert into [new-keeper].[Categories] (
     [ReqUserId]
    ,[OrgId]
    ,[ParentId]
    ,[Name]
    ,[CreatedAt]
    ,[UpdatedAt] )
select
    @ReqUserId,
    @OrgId,
    @ParentId,
    @Name,
    @now,
    @now

set @ResultId = @@identity
";

            #endregion

            public int Exec(ISqlExecutor sql)
            {
                sql.Query(c_query, this);

                return ResultId;
            }
        }
    }
}