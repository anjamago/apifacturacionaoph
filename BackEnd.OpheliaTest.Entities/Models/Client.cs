using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.OpheliaTest.Entities.Models
{
    public partial class Client
    {
        public Client()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime Birthday{ get; set; }
        public long IdentificationNumber { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
