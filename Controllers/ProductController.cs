﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using GentsOutfits_Authentication_.Models;
using System.Security.Claims;

namespace GentsOutfits_Authentication_.Controllers
{
    [Authorize(Policy = "adminonly")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IWebHostEnvironment _env;
        private IProduct prod;
        public ProductController(IProduct p, ILogger<ProductController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
            prod = p;

        }
        public ViewResult viewproduct()
        {
            //ProductRepository productsRepository = new ProductRepository();
            return View(prod.GetAll());
        }

        [HttpGet]
        public ViewResult addproduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addproduct(string name, string pakagedetail, decimal price, int quantity, string category, string feature, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return View("addproduct");
            }


            Product product = new Product();
            product.Name = name;
            product.PakageDetail = pakagedetail;
            product.Price = price;
            product.Quantity = quantity;
            product.Category = category;
            product.Feature = feature;
            string wwwrootPath = _env.WebRootPath;
            string imagesFolderPath = Path.Combine(wwwrootPath, "ProductImages");

            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }
            string allimages = string.Empty;
            int i = 0;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(imagesFolderPath, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    string imagepath = Path.Combine("~/ProductImages/", fileName);
                    i++;
                    if (i != files.Count)
                    {
                        allimages = allimages + imagepath + ",";
                    }
                    else
                    {
                        allimages = allimages + imagepath;

                    }
                }
            }
            product.Images = allimages;
            //ProductRepository productRepository = new ProductRepository();
            prod.Add(product);
            return RedirectToAction("viewproduct", "Product");

        }
        public IActionResult Remove(int ID)
        {
            //ProductRepository productRepository = new ProductRepository();
            prod.Delete(ID);
            return RedirectToAction("viewproduct");
        }
        public ViewResult Edit(int id)
        {

            //ProductRepository productRepository = new ProductRepository();
            Product p = prod.Edit(id);
            return View("Edit", p);
        }
        [HttpPost]
        public IActionResult Edit(int id, string name, string pakagedetail, decimal price, int quantity, string category, string features, string files)

        {
            Product product = new Product();
            product.Id = id;
            product.Name = name;
            product.PakageDetail = pakagedetail;
            product.Price = price;
            product.Quantity = quantity;
            product.Category = category;
            product.Feature = features;
            product.Images = files;

            //ProductRepository productRepository = new ProductRepository();
            prod.Update(product);
            return RedirectToAction("viewproduct", "Product");
        }
        public ViewResult Details(int id)
        {
            //ProductRepository productRepository = new ProductRepository();
            Product p = prod.GetById(id);
            return View("Details", p);
        }
    }
}
