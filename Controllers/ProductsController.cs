using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pantry.Models;

namespace Pantry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDataContext _context;

        public ProductsController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Products 
        // get a particular product from a particular package
        // query ?
        [HttpGet("{ProductID}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string ProductID)
        {
            try
            {
                
                int proID = int.Parse(ProductID);
                IQueryable<Product> query = _context.Products.AsQueryable();
                query = query.Where<Product>(x => x.ProductID == proID);
                return await query.ToListAsync();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        // GET: api/Products 
        //object list in pantry
        // query ?
        [HttpGet("{PantryID}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsInPantry(string PantryID)
        {
            try
            {
                int paID = int.Parse(PantryID);
                IQueryable<Product> query1 = _context.Products.AsQueryable();
                IQueryable<Package> query2 = _context.Packages.AsQueryable();
                query2 = query2.Where<Package>(x => x.PantryID == paID);
                var join = (from q1 in query1 join q2 in query2
                            on q1.PackageID equals q2.PackageID
                            where q2.PantryID == paID
                            select new Product
                            {
                                ProductID = q1.ProductID,
                                ProductDesc = q1.ProductDesc,
                                PackageID = q1.PackageID
                            });

                return await join.ToListAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        

    }
}