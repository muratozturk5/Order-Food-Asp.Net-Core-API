using Microsoft.AspNetCore.Mvc;
using OrderFoodAPI.Models;
using System.Collections.Generic;
using System.Linq;



namespace OrderFoodAPI.Controllers
{
    public class OrderFoodApiController : Controller
    {
        order_foodContext db;



        public OrderFoodApiController(order_foodContext _db)
        {
            db = _db;
        }


        [HttpGet]
        [Route("getCategories")]
        public List<Category> getCategories()
        {
            var categories = db.Categories.OrderBy(x => x.Id).ToList();

            return categories;
        }

        [HttpGet]
        [Route("getProducts")]
        public List<Product> getProducts()
        {
            var products = db.Products.OrderBy(x => x.Id).ToList();

            return products;
        }

        [HttpGet]
        [Route("getCategoryProducts")]
        public List<Product> getCategoryProducts(int categoryId)
        {
            var products = db.Products.Where(x => x.CategoryId == categoryId).ToList();

            return products;
        }



    }
}
