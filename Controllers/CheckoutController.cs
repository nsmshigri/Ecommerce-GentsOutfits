using Microsoft.AspNetCore.Mvc;
using GentsOutfits_Authentication_.Models;
using System;

namespace GentsOutfits_Authentication_.Controllers
{
    public class CheckoutController : Controller
       
    {
        private IProduct p;
        private IOrder ord;
        private ICart ca;
        public CheckoutController(IProduct product, IOrder order, ICart cart)
        {
            this.p = product;
            this.ord = order;
            this.ca = cart;
        }
        string connect = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GentsOutfits;Integrated Security=True";
        //OrderRepository orderRepository =new OrderRepository();
        //ProductRepository productRepository = new ProductRepository();
        [HttpGet]
        public ViewResult OrderProcessing()
        {
            return View();
        }
        [HttpPost]
        public IActionResult OrderProcessing(string name,string address,string city,string postalcode,string country)
        {
            if(!ModelState.IsValid)
            {
                return View("orderprocessing");
            }
            OrderDetail o = new OrderDetail { Name=name,Address=address,City=city,Country=country,PostalCode=postalcode};
            
            Thread.Sleep(1000);
            long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Random random = new Random((int)timestamp);
            o.Id = random.Next();
            TempData["Order-Id"] = o.Id;
            o.PlacedAt=DateTime.Now.Date.ToString("dd-MM-yyyy");
            List<CartItems> cartItems = new List<CartItems>();

            if (User.Identity.IsAuthenticated) // Check if the user is authenticated
            {
                //CartRepository cartRepository = new CartRepository(connect);
                cartItems = ca.GetItemsByUser(User.Identity.Name);
                foreach(var c in cartItems)
                {
                    int availableStock = p.GetStockQuantity(c.ProductId);
                    if(c.Quantity>availableStock)
                    {
                        TempData["ErrorCheckout"] = $"Sorry You Are too Late...Requested quantity for {c.Name} is not available in stock.";
                        return RedirectToAction("ViewCart", "Order");


                    }
                }

            }
            else
            {
                // Fetch cart items from the session for anonymous users
                cartItems = HttpContext.Session.Get<List<CartItems>>("CartProducts") ?? new List<CartItems>();
                foreach (var c in cartItems)
                {
                    int availableStock = p.GetStockQuantity(c.ProductId);
                    if (c.Quantity > availableStock)
                    {
                        TempData["ErrorCheckout"] = $"Sorry You Are too Late...Requested quantity for {c.Name} is not available in stock.";
                        return RedirectToAction("ViewCart", "Order");
                    }
                }
            }
            foreach(var c in cartItems)
            {
                int availableStock =   p.GetStockQuantity(c.ProductId);
                p.UpdateStockQuantity(c.ProductId, availableStock - c.Quantity);
            }
            var productIds = cartItems.Select(item => item.ProductId).ToList();
            List<OrderProduct> orderProducts = new List<OrderProduct>();
            foreach(var pid in productIds)
            {
                orderProducts.Add(new OrderProduct { OrderId=o.Id, ProductId=pid });
            }
            ord.Add(o);
            ord.AddOrderProduct(orderProducts);
            return RedirectToAction("Complete", "Checkout");
        }
        public ViewResult Error()
        {
            return View();
        }
        public ViewResult Complete()
        {
            int id = Convert.ToInt32(TempData["Order-Id"]);
            CustomerOrder customerOrder = new CustomerOrder();
            customerOrder.OrderDetail= ord.GetById(id);
            List<CartItems> cartItems = new List<CartItems>();

            if (User.Identity.IsAuthenticated) // Check if the user is authenticated
            {
                
                customerOrder.Products = ord.getOrderProducts(User.Identity.Name);
                ord.deleteCartItems(User.Identity.Name);
            }
            else
            {
                // Fetch cart items from the session for anonymous users
                List<CartItems> c = new List<CartItems>();
                cartItems = HttpContext.Session.Get<List<CartItems>>("CartProducts") ?? new List<CartItems>();
                customerOrder.Products = cartItems;
                HttpContext.Session.Set("CartProducts", c);
            }
            Response.Cookies.Append("cartItemCount", "0");
            return View(customerOrder);
        }
    }
}
