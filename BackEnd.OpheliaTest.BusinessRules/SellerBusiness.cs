using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BackEnd.OpheliaTest.Entities.Interface.BusinessRules;
using BackEnd.OpheliaTest.Entities.Interface.Repository;
using BackEnd.OpheliaTest.Entities.Models;
using BackEnd.OpheliaTest.Entities.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BackEnd.OpheliaTest.BusinessRules
{
    public class SellerBusiness : ISellerBusiness
    {
        private readonly IBaseRepository<Seller> Repository;

        public SellerBusiness(IBaseRepository<Seller> repository)
        {
            Repository = repository;
        }

        public async Task<ResponseBase<Seller>> Create(Seller data)
        {
            try
            {
                var seller = await Repository.GetAsync(predicate: x => x.Id == data.Id || x.SellerCode == data.SellerCode);
                if (seller == null)
                {
                    await Repository.AddAsync(data);
                    return new ResponseBase<Seller>(code: HttpStatusCode.OK, message: "Se ha creado un nuevo vendedor");
                }
                return new ResponseBase<Seller>(code: HttpStatusCode.BadRequest, message: "El vendedor o el codigo de vendedor  ya se encuentra registrado en el sistema");

            }
            catch (Exception e)
            {
                return new ResponseBase<Seller>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<Seller>> Delete(int Id)
        {
            try
            {
                var clientInvoice = await Repository.GetAsync(predicate: x => x.Id == Id,
                        include: i => i.Include(inc => inc.Invoices)
                    );

                if (clientInvoice != null && clientInvoice.Invoices.Count == 0)
                {
                    await Repository.DeleteAsync(predicate: x => x.Id == Id);
                    return new ResponseBase<Seller>(code: HttpStatusCode.OK, message: "Se ha eliminado el registro del vendedor");
                }
                return new ResponseBase<Seller>(code: HttpStatusCode.OK, message: "Nose encontro el recurso a eliminar");
            }
            catch (Exception e)
            {
                return new ResponseBase<Seller>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<List<Seller>>> GetAll()
        {

            try
            {
                var all = await Repository.GetAllAsync();
                return new ResponseBase<List<Seller>>(code: HttpStatusCode.OK, message: "Solicitud ok", data: all.ToList());
            }
            catch (Exception e)
            {
                return new ResponseBase<List<Seller>>(code: HttpStatusCode.InternalServerError, message: "Error de servidor");
            }
        }

        public async Task<ResponseBase<Seller>> GetFind(int Id)
        {
            try
            {
                var cliente = await Repository.GetAsync(predicate: x => x.Id == Id,
                    include: i=>i.Include(inc=>inc.Invoices));
                return new ResponseBase<Seller>(code: HttpStatusCode.OK, message: "Solicitud Ok", data: cliente);
            }
            catch (Exception e)
            {
                return new ResponseBase<Seller>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<Seller>> Update(Seller data)
        {
            try
            {
                var seller = await Repository.GetAsync(predicate: x => x.Id == data.Id);

                if (seller != null)
                {
                    data.UpdateAt = DateTime.Now;
                    data.CreateAt = seller.CreateAt;
                    await Repository.UpdateAsync(data);
                    return new ResponseBase<Seller>(code: HttpStatusCode.OK, message: "Se ha actualizado la informacion del cliente", data: data);
                }

                return new ResponseBase<Seller>(code: HttpStatusCode.OK, message: "La informacion del cliente no existe o esta erronea ", data: data);
            }
            catch (Exception e)
            {
                return new ResponseBase<Seller>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

       
    }
}
