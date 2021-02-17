using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.OpheliaTest.Entities.Models;
using BackEnd.OpheliaTest.Entities.Responses;

namespace BackEnd.OpheliaTest.Entities.Interface.BusinessRules
{
    public interface IInvoiceBusiness
    {
        Task<ResponseBase<Invoice>> GetFind(Guid Id);
        Task<ResponseBase<List<Invoice>>> GetAll();
        Task<ResponseBase<Invoice>> Create(Invoice data);
        Task<ResponseBase<List<Client>>> FilterClient();
        Task<ResponseBase<List<ProductoRequest>>> TotalSold();
        Task<ResponseBase<dynamic>> PurchaseFrequency();
    }
}
