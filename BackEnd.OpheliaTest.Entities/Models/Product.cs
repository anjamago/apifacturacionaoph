using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.OpheliaTest.Entities.Models
{
    public partial class Product
    {
        public Product()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public int Id { get; set; }
        public string Product1 { get; set; }
        public int Cuantity { get; set; }
        public double Prince { get; set; }
        public int IdCategory { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
