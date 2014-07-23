using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Shuvashish.Core;
using Shuvashish.Core.Model;
using Shuvashish.Models;

namespace Shuvashish.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PagingHelper.FilterCriteria = 0;
            PagingHelper.FilterValue = 0;

            return RedirectToAction("ProductIndex");
        }

        public ActionResult ProductIndex()
        {
            IEnumerable<ProductModel> products;

            switch (PagingHelper.FilterCriteria)
            {
                case 1:
                    products = new ProductBL().GetProductsFromSupplier(PagingHelper.FilterValue);
                    break;
                case 2:
                    products = new ProductBL().GetProductsByCategory(PagingHelper.FilterValue);
                    break;
                case 3:
                    products = new ProductBL().GetProductsByName(PagingHelper.FilterValueString);
                    break;
                default:
                    products = new ProductBL().GetProducts();
                    break;
            }

            if (TempData["isSuccessful"] != null)
            {
                ViewBag.isSuccessful = TempData["isSuccessful"];
                if (TempData["Message"] != null) ViewBag.Message = TempData["Message"];
                ViewBag.ClassInfo = ViewBag.isSuccessful ? "alert alert-success" : "alert alert-danger";
            }

            return View("Index", SortAndPagedProducts(products));
        }

        private IEnumerable<ProductModel> SortAndPagedProducts(IEnumerable<ProductModel> productModels)
        {
            var products = !PagingHelper.IsDescending
                ? SortListinAscendingOrder(productModels)
                : SortListinDescendingOrder(productModels);
            var pagedList = PreparePages(products);
            return pagedList;
        }

        private static IEnumerable<ProductModel> SortListinAscendingOrder(IEnumerable<ProductModel> productModels)
        {
            IEnumerable<ProductModel> products;
            switch (PagingHelper.SortColumn)
            {
                case 2:
                    products = productModels.OrderBy(p => p.Category.Name);
                    break;
                case 3:
                    products = productModels.OrderBy(p => p.Name);
                    break;
                default:
                    products = productModels.OrderBy(p => p.Supplier.Name);
                    break;
            }
            return products;
        }

        private static IEnumerable<ProductModel> SortListinDescendingOrder(IEnumerable<ProductModel> productModels)
        {
            IEnumerable<ProductModel> products;
            switch (PagingHelper.SortColumn)
            {
                case 2:
                    products = productModels.OrderByDescending(p => p.Category.Name);
                    break;
                case 3:
                    products = productModels.OrderByDescending(p => p.Name);
                    break;
                default:
                    products = productModels.OrderByDescending(p => p.Supplier.Name);
                    break;
            }
            return products;
        }

        private IEnumerable<ProductModel> PreparePages(IEnumerable<ProductModel> products)
        {
            if (PagingHelper.PageNo < 1) PagingHelper.PageNo = 1;
            if (PagingHelper.PageNo >= PagingHelper.PageCount && PagingHelper.PageCount >= 1)
                PagingHelper.PageNo = PagingHelper.PageCount;

            var pagedList = products.ToPagedList(PagingHelper.PageNo, PagingHelper.PageSize);
            PagingHelper.PageCount = pagedList.PageCount;

            ViewBag.StartCount = ((PagingHelper.PageNo - 1)*10) + 1;
            return pagedList;
        }

        public ActionResult ShowProducts(IEnumerable<ProductModel> products)
        {
            return View("Index", products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is a sample application developed by Shuvashish Das for TouchStar";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult FindBySupplier(int supplier)
        {
            PagingHelper.FilterCriteria = 1;
            PagingHelper.FilterValue = supplier;
            PagingHelper.SortColumn = 1;
            PagingHelper.PageNo = 1;
            return RedirectToAction("ProductIndex");
        }

        public ActionResult FindByCategory(int category)
        {
            PagingHelper.FilterCriteria = 2;
            PagingHelper.FilterValue = category;
            PagingHelper.SortColumn = 2;
            PagingHelper.PageNo = 1;
            return RedirectToAction("ProductIndex");
        }

        [HttpPost]
        public ActionResult FindByProductName()
        {
            PagingHelper.FilterCriteria = 3;
            PagingHelper.FilterValueString = Request.Form["productName"];
            PagingHelper.SortColumn = 3;
            PagingHelper.PageNo = 1;
            return RedirectToAction("ProductIndex");
        }

        public ActionResult ShowDetails(int id)
        {
            var product = new ProductBL().GetProduct(id);
            return View(product);
        }

        public ActionResult SortBySupplier()
        {
            PagingHelper.IsDescending = (PagingHelper.SortColumn != 1) || !PagingHelper.IsDescending;
            PagingHelper.SortColumn = 1;
            PagingHelper.PageNo = 1;

            return RedirectToAction("ProductIndex");
        }

        public ActionResult SortByCategory()
        {
            PagingHelper.IsDescending = (PagingHelper.SortColumn != 2) || !PagingHelper.IsDescending;
            PagingHelper.SortColumn = 2;
            PagingHelper.PageNo = 1;

            return RedirectToAction("ProductIndex");
        }

        public ActionResult SortByName()
        {
            PagingHelper.IsDescending = (PagingHelper.SortColumn != 3) || !PagingHelper.IsDescending;
            PagingHelper.SortColumn = 3;
            PagingHelper.PageNo = 1;

            return RedirectToAction("ProductIndex");
        }

        public ActionResult RemoveProduct(int id)
        {
            var isdeleted = new ProductBL().RemoveProduct(id);
            TempData["isSuccessful"] = isdeleted;
            TempData["Message"] = isdeleted ? "Product information deleted successfully." : "Product information delete operation cancelled.";

            return RedirectToAction("ProductIndex");
        }

        public ActionResult AddProduct()
        {
            ViewData["categories"] = new ProductBL().GetCategories().OrderBy(c=>c.Name).ToList();
            ViewData["suppliers"] = new ProductBL().GetSuppliers().OrderBy(s => s.Name).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var category = Convert.ToInt32(Request.Form["categorySelector"]);
                var supplier = Convert.ToInt32(Request.Form["supplierSelector"]);
                var isAdded = new ProductBL().AddProduct(product, category, supplier);

                TempData["isSuccessful"] = isAdded;
                TempData["Message"] = isAdded
                    ? "New product information added."
                    : "New product information add operation not successful.";

                if (isAdded) return RedirectToAction("Index");
            }

            ViewData["categories"] = new ProductBL().GetCategories().OrderBy(c => c.Name).ToList();
            ViewData["suppliers"] = new ProductBL().GetSuppliers().OrderBy(s => s.Name).ToList();
            return View(product);
        }

        public ActionResult EditProduct(int id)
        {
            ViewData["categories"] = new ProductBL().GetCategories().OrderBy(c => c.Name).ToList();
            ViewData["suppliers"] = new ProductBL().GetSuppliers().OrderBy(s => s.Name).ToList();
            var product = new ProductBL().GetProduct(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var category = Convert.ToInt32(Request.Form["categorySelector"]);
                var supplier = Convert.ToInt32(Request.Form["supplierSelector"]);
                var isUpdated = new ProductBL().UpdateProduct(product, category, supplier);

                TempData["isSuccessful"] = isUpdated;
                TempData["Message"] = isUpdated
                    ? "Product information update successful."
                    : "Product information update operation is cancelled.";

                if (isUpdated) return RedirectToAction("Index");
            }
            ViewData["categories"] = new ProductBL().GetCategories().OrderBy(c => c.Name).ToList();
            ViewData["suppliers"] = new ProductBL().GetSuppliers().OrderBy(s => s.Name).ToList();
            //return RedirectToAction("EditProduct", new {id = product.Id});
            return View(product);
        }

        public ActionResult PreviousPagedData()
        {
            PagingHelper.PageNo--;
            return RedirectToAction("ProductIndex");
        }

        public ActionResult NextPagedData()
        {
            PagingHelper.PageNo++;
            return RedirectToAction("ProductIndex");
        }

        public ActionResult FirstPagedData()
        {
            PagingHelper.PageNo = 1;
            return RedirectToAction("ProductIndex");
        }

        public ActionResult LastPagedData()
        {
            PagingHelper.PageNo = PagingHelper.PageCount;
            return RedirectToAction("ProductIndex");
        }
    }
}