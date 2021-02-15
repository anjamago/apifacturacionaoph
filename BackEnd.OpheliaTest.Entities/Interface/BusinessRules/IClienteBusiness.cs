using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.OpheliaTest.Entities.Models;
using BackEnd.OpheliaTest.Entities.Responses;

namespace BackEnd.OpheliaTest.Entities.Interface.BusinessRules
{
    public interface IClienteBusiness
    {
        Task<ResponseBase<Client>> GetFind(int Id);
        Task<ResponseBase<List<Client>>> GetAll();
        Task<ResponseBase<Client>> Create(Client data);
        Task<ResponseBase<Client>> Update(Client data);
        Task<ResponseBase<Client>> Delete(int Id);
    }
}
