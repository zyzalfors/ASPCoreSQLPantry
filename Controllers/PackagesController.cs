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
    public class PackagesController : ControllerBase
    {
        private readonly AppDataContext _context;

        public PackagesController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Packages 
        //get integrity status of a package specified by ? url query
        [HttpGet]
        public async Task<ActionResult<Package>> GetPackageInteg(string id)
        {
            try
            {
                int Id = int.Parse(id);
                var package = await _context.Packages.FindAsync(Id);
                if (package == null)
                {
                    return NotFound();
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
                return StatusCode(500, e.ToString());
            }
        }

        // GET: api/Packages/n
        //get package specified by an id
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(String id)
        {
            try
            {
                int Id = int.Parse(id);
                var package = await _context.Packages.FindAsync(Id);
                if (package == null)
                {
                    return NotFound();
                }
                return package;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        // POST: api/Package
        //add a new package
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            try
            {
              _context.Packages.Add(package);
              await _context.SaveChangesAsync();
              return CreatedAtAction("GetPackage", new { id = package.PackageID }, package);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.ToString());
            }

        }
    }

}