using Microsoft.AspNetCore.Mvc;
using GentsOutfits_Authentication_.Models;

namespace GentsOutfits_Authentication_.Controllers
{
    public class JacketsController : Controller
    {
        private IProduct product;
        public JacketsController(IProduct p)
        {
            product = p;
        }
        public IActionResult Index()
        {
            //ProductRepository productsRepository = new ProductRepository();
            List<Product> products = product.get("Jackets");
            return View(products);
        }
        public IActionResult Specific(int Id)
        {
            //ProductRepository productsRepository = new ProductRepository();
            Product products = product.GetItem(Id);
            return View(products);
        }

    }
}
