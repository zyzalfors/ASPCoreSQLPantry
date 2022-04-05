using System;
using Pantry.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pantry.Controllers;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Pantry.Services
{
    public interface IPackageService
    {
        public Task<ActionResult<Package>> GetPackageInteg(string id, PackagesController pc);
        public Task<ActionResult<Package>> GetPackage(string id, PackagesController pc);
        public Task<ActionResult<Package>> PostPackage(Package package, PackagesController pc);
    }

    public class PackageService : IPackageService
    {
        private readonly AppDataContext _context;
        
        public PackageService(AppDataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Package>> GetPackageInteg(string id, PackagesController pc)
        {
            try
            {
                int Id = int.Parse(id);
                var package = await _context.Packages.FindAsync(Id);
                if (package == null)
                {
                    return pc.NotFound();
                }

                var a = new
                {
                    PackageID = Id,
                    PackageInteg = package.PackageInteg
                };

                return new ObjectResult(a); // return anonymous JSON object

            }
            catch (Exception e)
            {
                return pc.StatusCode(500, e.ToString());
            }

        }

        public async Task<ActionResult<Package>> GetPackage(string id, PackagesController pc)
        {
            try
            {
                int Id = int.Parse(id);
                var package = await _context.Packages.FindAsync(Id);
                if (package == null)
                {
                    return pc.NotFound();
                }
                return package;
            }
            catch (Exception e)
            {
                return pc.StatusCode(500, e.ToString());
            }


        }

        public async Task<ActionResult<Package>> PostPackage(Package package, PackagesController pc)
        {
            try
            {
                _context.Packages.Add(package);
                await _context.SaveChangesAsync();
                return pc.CreatedAtAction("GetPackage", new { id = package.PackageID }, package);
            }
            catch (Exception e)
            {
                return pc.StatusCode(500, e.ToString());
            }
        }

    }

    public interface IProductService
    {
        public Task<ActionResult<IEnumerable<Product>>> GetProducts(string ProductID, ProductsController pc);
        public Task<ActionResult<IEnumerable<Product>>> GetProductsInPantry(string PantryID, ProductsController pc);

    }

    public class ProductService : IProductService
    {
        private readonly AppDataContext _context;

        public ProductService(AppDataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string ProductID, ProductsController pc)
        {
            try
            {
                int proID = int.Parse(ProductID);
                IQueryable<Product> query = _context.Products.AsQueryable();
                query = query.Where<Product>(x => x.ProductID == proID);
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                return pc.StatusCode(500, e.ToString());
            }
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProductsInPantry(string PantryID, ProductsController pc)
        {
            try
            {
                int paID = int.Parse(PantryID);
                IQueryable<Product> query1 = _context.Products.AsQueryable();
                IQueryable<Package> query2 = _context.Packages.AsQueryable();
                query2 = query2.Where<Package>(x => x.PantryID == paID);
                var join = (from q1 in query1 join q2 in query2 on q1.PackageID equals q2.PackageID
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
                return pc.StatusCode(500, e.ToString());
            }
        }

    }
}