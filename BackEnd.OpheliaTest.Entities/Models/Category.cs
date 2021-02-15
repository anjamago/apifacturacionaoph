using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.OpheliaTest.Entities.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Category1 { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
