using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtFuji.Models
{
    public class ProductVM
    {
        public int product_Id { get; set; }
        public string product_name { get; set; }
        public string product_description { get; set; }
        public string is_new { get; set; }
        public string is_discount { get; set; }
        public Nullable<decimal> new_price { get; set; }
        public Nullable<decimal> old_price { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }
        public string img4 { get; set; }
        public Nullable<int> quantity { get; set; }
        public string offer_title { get; set; }
        public Nullable<int> star { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> SubCategoryID { get; set; }
        public string model_No { get; set; }
        public Nullable<bool> isDeleted { get; set; }

        public virtual Category Category1 { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}