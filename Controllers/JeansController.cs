using Microsoft.AspNetCore.Mvc;
using GentsOutfits_Authentication_.Models;

namespace GentsOutfits_Authentication_.Controllers
{
    public class JeansController : Controller
    {
        private IProduct product;
        public JeansController(IProduct p)
        {
            product = p;
        }
        public IActionResult Index()
        {
            //ProductRepository productsRepository = new ProductRepository();
            List<Product> products = product.get("Jeans");
            return View(products);
        }
        public IActionResult Specific(int ID)
        {
            //ProductRepository productsRepository = new ProductRepository();
            Product products = product.GetItem(ID);
            return View(products);
        }
    }
}
