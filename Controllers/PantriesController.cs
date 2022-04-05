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
    public class PantriesController : ControllerBase
    {
        private readonly AppDataContext _context;

        public PantriesController(AppDataContext context)
        {
            _context = context;
        }

        
                
    }
}