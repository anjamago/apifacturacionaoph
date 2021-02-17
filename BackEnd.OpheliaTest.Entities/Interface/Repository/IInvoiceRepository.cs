using BackEnd.OpheliaTest.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.OpheliaTest.Entities.Interface.Repository
{
    public interface IInvoiceRepository
    {
        Task<List<Client>> getFilterClient();
        Task<List<ProductoRequest>> GetTotalSold();
    }
}
