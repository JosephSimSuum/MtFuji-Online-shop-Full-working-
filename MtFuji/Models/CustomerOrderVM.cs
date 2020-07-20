using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MountFuji.Models
{
    public class CustomerOrderVM
    {

        [Required]
        public int OrderID { get; set; }
        [Required(ErrorMessage = "Customer Name is required")]
        public string CustomerName { get; set; }
         [Required(ErrorMessage = "Customer Phone is required")]
         [StringLength(11,ErrorMessage = "Customer Phone is required")]
        public string CustomerPhone { get; set; }
        [Required(ErrorMessage = "Customer Address is required")]
        public string CustomerAddress { get; set; }
        public string PaymentType { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public byte[] Status { get; set; }
        public string Phone2 { get; set; }
    }
}