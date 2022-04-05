using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pantry.Models;
using Pantry.Services;

namespace Pantry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _ps;

        public ProductsController(IProductService ps)
        {
            _ps = ps;
        }
                  
        // GET: api/Products/n
        // get a particular product from a particular package
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string ProductID)
        {
            return await _ps.GetProducts(ProductID, this);
        }

        // GET: api/Products 
        //object list in pantry
        // query ?
        [HttpGet("{PantryID}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsInPantry(string PantryID)
        {
            return await _ps.GetProductsInPantry(PantryID, this);
        }    

    }
}