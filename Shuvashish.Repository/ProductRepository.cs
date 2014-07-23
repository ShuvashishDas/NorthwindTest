using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Shuvashish.Repository.UnitOfWork;

namespace Shuvashish.Repository
{
    public class ProductRepository
    {
        #region Private Members

        private readonly NorthwindEntities _context;

        #endregion

        #region Constructors

        public ProductRepository()
        {
            _context = ObjectContextManager.GetObjectContext();
        }

        #endregion

        #region Product
        private IEnumerable<Product> GetAllProducts()
        {
            var products = _context.Products; //.Include("Category").Include("Supplier");
            return products;
        }
        public IEnumerable<Product> GetProducts(string name)
        {
            var products = GetAllProducts();
            var productName = name.ToLower().Trim();

            if (!string.IsNullOrEmpty(productName))
                products = products.Where(product => product.ProductName.ToLower().Trim().Contains(productName)).ToList();

            return products;
        }
        public bool RemoveProduct(int id)
        {
            var product = _context.Products.Include("Order_Details").FirstOrDefault(p => p.ProductID == id);
            if (product == null) return false;
            
            _context.Products.Remove(product);
            var retVal = _context.SaveChanges();
            return retVal > 0;
        }
        public bool AddProduct(Product product)
        {
            _context.Products.Add(product);
            var retVal = _context.SaveChanges();

            return retVal > 0;
        }
        public bool UpdateProduct(int id, Product productModel)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == id);
            if (product != null)
            {
                product.ProductName = productModel.ProductName;
                product.QuantityPerUnit = productModel.QuantityPerUnit;
                product.CategoryID = productModel.CategoryID;
                product.Discontinued = productModel.Discontinued;
                product.ReorderLevel = productModel.ReorderLevel;
                product.SupplierID = productModel.SupplierID;
                product.UnitPrice = productModel.UnitPrice;
                product.UnitsInStock = productModel.UnitsInStock;
                product.UnitsOnOrder = productModel.UnitsOnOrder;

                _context.Entry(product).State = EntityState.Modified;
                var retVal = _context.SaveChanges();
                return retVal > 0;
            }
            return false;
        }
        #endregion

        #region Category
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories;
        }
        #endregion

        #region Supplier
        public IEnumerable<Supplier> GetSuppliers()
        {
            return _context.Suppliers;
        }
        #endregion
    }
}