using System.ComponentModel.DataAnnotations;

namespace Shuvashish.Core.Model
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Product name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Quantity per unit is required")]
        [Display(Name = "Quantity per unit")]
        public string QuantityPerUnit { get; set; }

        [Required(ErrorMessage = "Reorder level is required")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Number required for Reorder Level")]
        [Range(0, 99999999, ErrorMessage = "Value must be between 0 - 99,999,999")]
        [Display(Name = "Reorder level")]
        public short? ReorderLevel { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Unit price is required")]
        [RegularExpression(@"\A\d+(\.\d{1,2})?\Z", ErrorMessage = "Number required for Unit Price")]
        [Range(0.0, 99999999.99, ErrorMessage = "Value must be between 0 - 99,999,999.99")]
        [Display(Name = "Unit price")]
        public decimal? UnitPrice { get; set; }

        [Required(ErrorMessage = "Units in stock is required")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Number required for Units in Stock")]
        [Range(0, 99999999, ErrorMessage = "Value must be between 0 - 99,999,999")]
        [Display(Name = "Units in stock")]
        public short? UnitsInStock { get; set; }

        [Required(ErrorMessage = "Units on order is required")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Number required for Unit on Order")]
        [Range(0, 9999, ErrorMessage = "Value must be between 0 - 9,999")]
        [Display(Name = "Units on order")]
        public short? UnitsOnOrder { get; set; }

        [Display(Name = "Catergory")]
        public CategoryModel Category { get; set; }

        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }

        [Display(Name = "Supplier")]
        public SupplierModel Supplier { get; set; }
    }

    public class SupplierModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
    }

    public class CategoryModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}