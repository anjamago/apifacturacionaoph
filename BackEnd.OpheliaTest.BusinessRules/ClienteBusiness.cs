using BackEnd.OpheliaTest.Entities.Interface.BusinessRules;
using BackEnd.OpheliaTest.Entities.Models;
using BackEnd.OpheliaTest.Entities.Interface.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.OpheliaTest.Entities.Responses;
using System;
using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.OpheliaTest.BusinessRules
{
    
    public class ClienteBusiness : IClienteBusiness
    {
        private readonly IBaseRepository<Client> Repository;

        public ClienteBusiness(IBaseRepository<Client> repository)
        {
            Repository = repository;
        }
        public async Task<ResponseBase<Client>> Create(Client data)
        {
            try
            {
               
                if(await GetClientFind(data) == null)
                {
                    await Repository.AddAsync(data);
                    return new ResponseBase<Client>(code: HttpStatusCode.OK, message: "Se ha creado un nuevo cliente");
                }
                return new ResponseBase<Client>(code: HttpStatusCode.BadRequest, message: "El Cliente se encuentra registrado en el sistema");

            }
            catch (Exception e)
            {
                return new ResponseBase<Client>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<Client>> Delete(int Id)
        {
            try
            {
                var clientInvoice = await Repository.GetAsync(predicate: x => x.Id == Id,
                        include: i => i.Include(inc => inc.Invoices)
                    );

                if (clientInvoice != null && clientInvoice.Invoices.Count == 0)
                {
                    await Repository.DeleteAsync(predicate: x => x.Id == Id);
                    return new ResponseBase<Client>(code: HttpStatusCode.OK, message: "Se ha eliminado el registro del cliente");
                }
                return new ResponseBase<Client>(code: HttpStatusCode.OK, message: "Nose encontro el recurso a eliminar");
            }
            catch (Exception e)
            {
                return new ResponseBase<Client>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<List<Client>>> GetAll()
        {

            try
            {
                var all = await Repository.GetAllAsync();
                return new ResponseBase<List<Client>>(code: HttpStatusCode.OK, message: "Solicitud ok", data: all.ToList());
            }
            catch(Exception e)
            {
                return new ResponseBase<List<Client>>(code: HttpStatusCode.InternalServerError, message: "Error de servidor",data: new List<Client>());
            }
        }

        public async Task<ResponseBase<Client>> GetFind(int Id)
        {
            try
            {
                var cliente = await Repository.GetAsync(predicate:x=>x.Id == Id,
                    include: i => i.Include(inc => inc.Invoices));
                return new ResponseBase<Client>(code: HttpStatusCode.OK, message: "Se ha creado un nuevo cliente", data: cliente);
            }
            catch (Exception e)
            {
                return new ResponseBase<Client>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<Client>> Update(Client data)
        {
            try
            {
                var client = await GetClientFind(data);
                if (client != null)
                {
                    data.UpdateAt = DateTime.Now;
                    data.CreateAt = client.CreateAt;
                    await Repository.UpdateAsync(data);
                    return new ResponseBase<Client>(code: HttpStatusCode.OK, message: "Se ha actualizado la informacion del cliente", data: data);
                }
                
                return new ResponseBase<Client>(code: HttpStatusCode.OK, message: "La informacion del cliente no existe o esta erronea ", data: data);
            }
            catch (Exception e)
            {
                return new ResponseBase<Client>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        private async Task<Client> GetClientFind (Client data)
        {
            return await Repository.GetAsync(predicate: x => x.IdentificationNumber == data.IdentificationNumber && x.Email == data.Email);
        }
    }
}
