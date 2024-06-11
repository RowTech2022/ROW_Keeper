using System.Data;
using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Supplier
    {
        [BindStruct]
        public class Update
        {
            [Bind("Id")]
            public int Id { get; set; }
            
            [NVarChar("CompanyName", 500)]
            public string CompanyName { get; set; } = null!;

            [NVarChar("Phone", 20)]
            public string Phone { get; set; } = null!;

            [NVarChar("Email", 100)]
            public string? Email { get; set; }

            [NVarChar("Address", 300)]
            public string? Address { get; set; }

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
                nameof(CompanyName),
                nameof(Phone),
                nameof(Email),
                nameof(Address),
                nameof(Active)
            ];

            #endregion
            
            private IEnumerable<string> GetDefaultUpdateList
        }
    }
}