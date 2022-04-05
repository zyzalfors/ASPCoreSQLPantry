using System;
using System.Collections.Generic;

namespace Pantry.Models
{
    public class Package
    {
        public int PackageID { get; set; }

        public string PackageDesc { get; set; }

        public int PackageInteg { get; set; }

        public int PantryID { get; set; }
    }
}
