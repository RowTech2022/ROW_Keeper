using System.Data;
using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;
using Keeper.Client;

namespace Keeper.Core;

public partial class Db
{
    public partial class Discount
    {
        [BindStruct]
        public class Update
        {
            [Bind("Id")]
            public int Id { get; set; }
            
            [Bind("ProductId")] 
            public int ProductId { get; set; }
            
            [Bind("CategoryId")] 
            public int CategoryId { get; set; }
            
            [Bind("Percent")]
            public double Percent { get; set; }
            
            [NVarChar("Comment", 5000)]
            public string? Comment { get; set; }
            
            [Bind("FromDate")]
            public DateTimeOffset FromDate { get; set; }
            
            [Bind("ToDate")]
            public DateTimeOffset ToDate { get; set; }
            
            [Bind("Type")] 
            public DiscountType Type { get; set; }

            [Bind("Acitve")]
            public bool Active { get; set; }

            [Bind("ResultCount", Direction = ParameterDirection.Output)]
            public int ResultCount { get; set; }

            public string[]? UpdateList { get; set; }

            #region c_query

            private const string c_query = @"
{update}
where [Id] = @Id

set @ResultCount = @@rowcount
";

            #endregion

            #region c_updateList

            private static HashSet<string> c_updateList =
            [
                nameof(ProductId),
                nameof(CategoryId),
                nameof(Percent),
                nameof(Comment),
                nameof(FromDate),
                nameof(ToDate),
                nameof(Type),
                nameof(Active)
            ];

            #endregion

            private IEnumerable<string> GetDefaultUpdateList()
            {
                yield return nameof(ProductId);
                yield return nameof(CategoryId);
                yield return nameof(Percent);
                yield return nameof(Comment);
                yield return nameof(FromDate);
                yield return nameof(ToDate);
                yield return nameof(Type);
            }

            public void SetDefaultUpdateList()
            {
                UpdateList = GetDefaultUpdateList().ToArray();
            }
            

            public void Exec(ISqlExecutor sql)
            {
                var query = GetQuery();
                
                sql.Query(query, this);

                if (ResultCount == 0)
                    throw new Exception("The product discount cannot be wrote because it changed.");
            }

            private string GetQuery()
            {
                var query = c_query;

                var updatePart = new SqlUpdateQueryFormater(this, "[new-keeper].[ProductDiscounts]")
                    .AddUpdateList(UpdateList?.Where(c_updateList.Contains).ToArray())
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