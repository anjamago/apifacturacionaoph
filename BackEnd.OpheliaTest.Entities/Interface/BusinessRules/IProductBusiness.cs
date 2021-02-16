

using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.OpheliaTest.Entities.Models;
using BackEnd.OpheliaTest.Entities.Responses;

namespace BackEnd.OpheliaTest.Entities.Interface.BusinessRules
{
    public interface IProductBusiness
    {
        Task<ResponseBase<Product>> GetFind(int Id);
        Task<ResponseBase<List<Product>>> GetAll();
        Task<ResponseBase<Product>> Create(Product data);
        Task<ResponseBase<Product>> Update(Product data);
        Task<ResponseBase<Product>> Delete(int Id);
        Task<ResponseBase<List<Product>>> GetAllProductMinimun();
    }
}
