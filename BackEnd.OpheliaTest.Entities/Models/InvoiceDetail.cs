using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.OpheliaTest.Entities.Models
{
    public partial class InvoiceDetail
    {
        public int Id { get; set; }
        public Guid IdInvoice { get; set; }
        public int IdProduct { get; set; }
        public int Cuantity { get; set; }
        public double PrinceProduct { get; set; }
        public double PrinceInvoice { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual Invoice IdInvoiceNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
