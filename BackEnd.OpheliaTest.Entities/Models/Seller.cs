using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.OpheliaTest.Entities.Models
{
    public partial class Seller
    {
        public Seller()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string SellerCode { get; set; }
        public string SellerName { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
