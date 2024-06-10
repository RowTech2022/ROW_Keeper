using System.Data;
using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core.Product;

public partial class Db
{
    public partial class Product
    {
        public class Update
        {
            [Bind("Id")] 
            public int Id { get; set; }

            [Bind("CategoryId")]
            public int CategoryId { get; set; }

            [Bind("TaxId")]
            public int TaxId { get; set; }

            [NVarChar("UPC", 100)]
            public string UPC { get; set; } = null!;
    
            [NVarChar("Name", 500)]
            public string Name { get; set; } = null!;

            [Bind("AgeLimit")]
            public int AgeLimit { get; set; }

            [Bind("Quantity")]
            public int Quantity { get; set; }

            [Bind("BuyingPrice")]
            public decimal BuyingPrice { get; set; }

            [Bind("Price")]
            public decimal Price { get; set; }

            [Bind("DiscountPrice")]
            public decimal DiscountPrice { get; set; }

            [Bind("TotalPrice")]
            public decimal TotalPrice { get; set; }

            [Bind("Margin")]
            public int Margin { get; set; }

            [Bind("HaveDiscount")]
            public bool HaveDiscount { get; set; }

            [Bind("ExpiredDate")]
            public DateTimeOffset? ExpiredDate { get; set; }

            [Bind("Active")]
            public bool Active { get; set; }
            
            [Bind("ResultCount", Direction = ParameterDirection.Output)] 
            public int ResultCount { get; set; }

            public string[] UpdateList { get; set; } = null!;

            #region c_query

            private const string c_query = @"
{update}
where
    [Id] = @Id and
    1 = 1
";

            #endregion

            #region c_updateList

            private static HashSet<string> c_updateList =
            [
                nameof(CategoryId),
                nameof(TaxId),
                nameof(UPC),
                nameof(Name),
                nameof(AgeLimit),
                nameof(Quantity),
                nameof(BuyingPrice),
                nameof(Price),
                nameof(DiscountPrice),
                nameof(TotalPrice),
                nameof(Margin),
                nameof(HaveDiscount),
                nameof(ExpiredDate),
                nameof(Active),
            ];

            #endregion

            private IEnumerable<string> GetDefaultUpdateList()
            {
                yield return nameof(CategoryId);
                yield return nameof(TaxId);
                yield return nameof(UPC);
                yield return nameof(Name);
                yield return nameof(AgeLimit);
                yield return nameof(Quantity);
                yield return nameof(BuyingPrice);
                yield return nameof(Price);
                yield return nameof(DiscountPrice);
                yield return nameof(TotalPrice);
                yield return nameof(Margin);
                yield return nameof(HaveDiscount);
                yield return nameof(ExpiredDate);
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
                    throw new Exception("The product cannot be wrote because it changed.");
            }

            private string GetQuery()
            {
                var query = c_query;

                var updatePart = new SqlUpdateQueryFormater(this, "[new-keeper].[Products]")
                    .AddUpdateList(UpdateList.Where(c_updateList.Contains).ToArray())
                    .AddNowList("UpdatedAt")
                    .Query();

                if (updatePart == null)
                    throw new Exception();

                query = SqlQueriesFormater.ReplaceConst(query, "Update", updatePart);

                query = SqlQueriesFormater.RemoveAllNullSections(query, this);

                return SqlQueriesFormater.RemoveLabels(query);
            }
        }
    }
}