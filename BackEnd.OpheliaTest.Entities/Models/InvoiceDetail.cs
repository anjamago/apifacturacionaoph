using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.OpheliaTest.Entities.Models
{
    public partial class InvoiceDetail
    {
        public int Id { get; set; }
        public int Invoice { get; set; }
        public int IdProduct { get; set; }
        public int Cuantity { get; set; }
        public double PrinceProduct { get; set; }
        public double PrinceInvoice { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual Product IdProduct1 { get; set; }
        public virtual Invoice IdProductNavigation { get; set; }
    }
}
