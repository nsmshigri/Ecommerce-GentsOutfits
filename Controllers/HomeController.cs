using Microsoft.AspNetCore.Mvc;
using GentsOutfits_Authentication_.Models;
using System.Diagnostics;

namespace GentsOutfits_Authentication_.Controllers
{
    public class HomeController : Controller
    {
        private IProduct prod;
        public HomeController(IProduct p)
        {
            this.prod = p;
        }
        
        public IActionResult Index()
        {
            //ProductRepository productRepository = new ProductRepository();
            List<Product> sm=prod.get("Shirts");
            List<Product> ap=prod.get("Jeans");
            int i = 0;
            List<Product> products = new List<Product>();
            foreach (Product p in sm)
            {
                if(i<=3)
                {
                    products.Add(p);
                }
                i++;
            }
            i = 0;
            foreach (Product p in ap)
            {
                if (i <= 3)
                {
                    products.Add(p);
                }
                i++;
            }
            ViewBag.Title = "Home";
            return View(products);
        }
        
        public ActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return PartialView("_mypartialview", new List<Product>());
            }
            //ProductRepository productRepository = new ProductRepository();

            IEnumerable<Product> Products = prod.GetAll();
            var filteredProducts = Products
                .Where(p => p.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            return PartialView("_mypartialview", filteredProducts);

        }
        public IActionResult Specific(int ID)
        {
            //ProductRepository productsRepository = new ProductRepository();
            Product products = prod.GetItem(ID);
            return View(products);
        }

    }
}
