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
    public class PackagesController : ControllerBase
    {
        private readonly IPackageService _ps;

        public PackagesController(IPackageService ps)
        {
            _ps = ps;
        }

        // GET: api/Packages/n
        //get package specified by an id
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(string id)
        {
            return await _ps.GetPackage(id, this);
        }

        // POST: api/Package
        //add a new package
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            return await _ps.PostPackage(package, this);

        }
    }

}