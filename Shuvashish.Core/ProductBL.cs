using System.Collections.Generic;
using System.Linq;
using Shuvashish.Core.Model;
using Shuvashish.Repository;

namespace Shuvashish.Core
{
    public class ProductBL
    {
        private readonly ProductRepository _repository = new ProductRepository();

        #region Product
        public IEnumerable<ProductModel> GetProducts()
        {
            var products = _repository.GetProducts(string.Empty);
            var mappedProducts = ProductMappingToModel(products);
            return mappedProducts;
        }
        public IEnumerable<ProductModel> GetProductsByName(string value)
        {
            var products = _repository.GetProducts(value);
            var mappedProducts = ProductMappingToModel(products);
            return mappedProducts;
        }
        public IEnumerable<ProductModel> GetProductsFromSupplier(int supplier)
        {
            var products = _repository.GetProducts(string.Empty).Where(p=>p.Supplier.SupplierID==supplier);
            var mappedProducts = ProductMappingToModel(products);
            return mappedProducts;
        }
        public IEnumerable<ProductModel> GetProductsByCategory(int category)
        {
            var products = _repository.GetProducts(string.Empty).Where(p => p.Category.CategoryID == category);
            var mappedProducts = ProductMappingToModel(products);
            return mappedProducts;
        }
        public ProductModel GetProduct(int id)
        {
            var product = ProductMappingToModel(_repository.GetProducts(string.Empty).Where(p => p.ProductID == id)).FirstOrDefault();
            return product;
        }
        public bool RemoveProduct(int id)
        {
            return _repository.RemoveProduct(id);
        }
        public bool AddProduct(ProductModel product, int category, int supplier)
        {
            return _repository.AddProduct(ModelMappingingToProduct(product, category, supplier));
        }
        public bool UpdateProduct(ProductModel product, int category, int supplier)
        {
            return _repository.UpdateProduct(product.Id, ModelMappingingToProduct(product, category, supplier));
        }
        #endregion

        #region Category
        public IEnumerable<CategoryModel> GetCategories()
        {
            return CategoryMappingToModel(_repository.GetCategories());
        }
        #endregion
        
        #region Supplier
        public IEnumerable<SupplierModel> GetSuppliers()
        {
            return SupplierMappingToModel(_repository.GetSuppliers());
        }
        #endregion

        #region Mapping Functions
        private static IEnumerable<ProductModel> ProductMappingToModel(IEnumerable<Product> products)
        {
            var mappedProducts = new List<ProductModel>();
            foreach (var product in products)
            {
                mappedProducts.Add(new ProductModel
                {
                    Id = product.ProductID,
                    Name = product.ProductName,
                    Discontinued = product.Discontinued,
                    QuantityPerUnit = product.QuantityPerUnit,
                    ReorderLevel = product.ReorderLevel,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder,
                    Category = new CategoryModel
                    {
                        Id = product.Category.CategoryID,
                        Name = product.Category.CategoryName,
                        Description = product.Category.Description
                    },
                    Supplier = new SupplierModel
                    {
                        Id = product.Supplier.SupplierID,
                        Name = product.Supplier.CompanyName,
                        ContactPerson = product.Supplier.ContactName
                    }
                });
            }
            return mappedProducts;
        }
        private IEnumerable<SupplierModel> SupplierMappingToModel(IEnumerable<Supplier> suppliers)
        {
            return suppliers.Select(supplier => new SupplierModel
            {
                Id = supplier.SupplierID,
                Name = supplier.CompanyName,
                ContactPerson = supplier.ContactName
            }).ToList();
        }
        private IEnumerable<CategoryModel> CategoryMappingToModel(IEnumerable<Category> categories)
        {
            return categories.Select(category => new CategoryModel
            {
                Id = category.CategoryID,
                Name = category.CategoryName,
                Description = category.Description
            }).ToList();
        }
        private Product ModelMappingingToProduct(ProductModel model, int category, int supplier)
        {
            return new Product
            {
                Discontinued = model.Discontinued,
                ProductName = model.Name,
                QuantityPerUnit = model.QuantityPerUnit,
                ReorderLevel = model.ReorderLevel,
                UnitPrice = model.UnitPrice,
                UnitsInStock = model.UnitsInStock,
                UnitsOnOrder = model.UnitsOnOrder,
                CategoryID = category,
                SupplierID = supplier,
                Category = _repository.GetCategories().FirstOrDefault(c => c.CategoryID == category),
                Supplier = _repository.GetSuppliers().FirstOrDefault(c => c.SupplierID == supplier)
            };
        }
        #endregion
    }
}