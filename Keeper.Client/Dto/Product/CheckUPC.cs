using System.ComponentModel.DataAnnotations;
using Keeper.Client.Validation;

namespace Keeper.Client.Product;

public partial class Product
{
    public class CheckUPC
    {
        [Required]
        [TrimWhitespace(100)]
        public string UPC { get; set; } = null!;
    }
}