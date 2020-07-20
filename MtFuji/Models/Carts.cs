using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtFuji.Models
{
    public class Carts
    {
           //public Mobile mobile { get; set; }
        public Product product { get; set; }
        public int p { get; set; }

        public Carts(Product product, int p)
        {
            // TODO: Complete member initialization
            this.product = product;
            this.p = p;
        }

    }
}