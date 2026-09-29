using Microsoft.AspNetCore.Mvc;
using firstASP.Data;
using firstASP.Models;

namespace firstASP.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // SHOW PRODUCTS
        public IActionResult Index()
        {
            var products = _db.Products.ToList();

            return View(products);
        }

        // SHOW ADD FORM
        public IActionResult Create()
        {
            return View();
        }

        // SAVE NEW PRODUCT
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // SHOW EDIT FORM
        public IActionResult Edit(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // SAVE EDITED PRODUCT
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var existingProduct = _db.Products.FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE PRODUCT
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}