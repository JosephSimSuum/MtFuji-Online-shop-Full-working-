using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtFuji.Models
{
    public class User
    {
        public Customer customer{ get; set; }
        public User(Customer customer)
        {
            this.customer = customer;
        }
    }
}