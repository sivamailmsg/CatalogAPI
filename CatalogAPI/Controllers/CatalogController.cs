using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private static IList<Product> Products = new List<Product>()
        {
            new Product{Id=1,Name="Apple",Price=152,Quantity=25},
            new Product{Id=2,Name="Orange",Price=52,Quantity=15},
            new Product{Id=3,Name="Mango",Price=63,Quantity=25},
        };

        [HttpGet("", Name = "GetAll")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IEnumerable<Product> GetItems()
        {
            return Products;
        }

        [HttpGet("", Name = "GetById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<Product> GetItemById([FromRoute]int id)
        {
            var item = Products.Single(s => s.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return item;
            }
        }

        [HttpGet("", Name = "AddItem")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public ActionResult<Product> AddItem([FromBody]Product product)
        {
            product.Id = Products.Max(s => s.Id) + 1;
            Products.Add(product);
            return Created("", product);
        }
    }
}