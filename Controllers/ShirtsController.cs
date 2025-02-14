using Microsoft.AspNetCore.Mvc;
using GentsOutfits_Authentication_.Models;

namespace GentsOutfits_Authentication_.Controllers
{
    public class ShirtsController : Controller
    {
        private IProduct prod;
        public ShirtsController(IProduct p)
        {
            prod = p;
        }
        public IActionResult Index()
        {
            //ProductRepository productsRepository = new ProductRepository();
            List<Product> products = prod.get("Shirts");
            return View(products);
        
        }
        public IActionResult Specific(int ID)
        {
            //ProductRepository productsRepository = new ProductRepository();
            Product products = prod.GetItem(ID);
            return View(products);
        }
    }
}
