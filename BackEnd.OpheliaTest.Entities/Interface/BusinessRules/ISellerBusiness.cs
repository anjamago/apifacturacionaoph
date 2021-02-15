using BackEnd.OpheliaTest.Entities.Models;
using BackEnd.OpheliaTest.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.OpheliaTest.Entities.Interface.BusinessRules
{
    public interface ISellerBusiness
    {
        Task<ResponseBase<Seller>> GetFind(int Id);
        Task<ResponseBase<List<Seller>>> GetAll();
        Task<ResponseBase<Seller>> Create(Seller data);
        Task<ResponseBase<Seller>> Update(Seller data);
        Task<ResponseBase<Seller>> Delete(int Id);
    }
}
