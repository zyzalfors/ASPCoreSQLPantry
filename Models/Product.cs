using System;
using System.Collections.Generic;



namespace Pantry.Models
{
    public partial class Product
    {
        public int ProductID { get; set; }
        public string ProductDesc { get; set; }
        public int PackageID { get; set; }
    }
}
