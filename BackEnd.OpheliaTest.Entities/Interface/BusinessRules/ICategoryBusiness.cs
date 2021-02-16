using System.Threading.Tasks;
using BackEnd.OpheliaTest.Entities.Models;
using BackEnd.OpheliaTest.Entities.Responses;


namespace BackEnd.OpheliaTest.Entities.Interface.BusinessRules{

    public interface ICategoryBusiness{

        Task<ResponseBase<System.Collections.Generic.List<Category>>> GetAll();
        Task<ResponseBase<Category>> GetFind(int Id);
        Task<ResponseBase<Category>> Create(Category data);
        Task<ResponseBase<Category>> Delete(int Id);
        Task<ResponseBase<Category>> Update(Category data);
    }
}