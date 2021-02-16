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
    public class ProductBusiness : IProductBusiness
    {
        private readonly IBaseRepository<Product> Repository;

        public ProductBusiness(IBaseRepository<Product> repository)
        {
            Repository = repository;
        }

        public async Task<ResponseBase<Product>> Create(Product data)
        {
            try
            {
                var Product = await Repository.GetAsync(predicate: x => x.Id == data.Id );
                if (Product == null)
                {
                    await Repository.AddAsync(data);
                    return new ResponseBase<Product>(code: HttpStatusCode.OK, message: "Se ha creado un nuevo producto");
                }
                return new ResponseBase<Product>(code: HttpStatusCode.BadRequest, message: "El producto  ya se encuentra registrado en el sistema");

            }
            catch (Exception e)
            {
                return new ResponseBase<Product>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async  Task<ResponseBase<List<Product>>> GetAllProductMinimun(){
            try{
                var all = await Repository.GetAllAsync(predicate: x=> x.Cuantity <= 5);
                return new ResponseBase<List<Product>>(code: HttpStatusCode.OK, message: "Solicitud Ok",data: all.ToList());
            }catch{
                  return new ResponseBase<List<Product>>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<Product>> Delete(int Id)
        {
            try
            {
                var productInvoice = await Repository.GetAsync(predicate: x => x.Id == Id,
                        include: i => i.Include(inc => inc.InvoiceDetails)
                    );

                if (productInvoice != null && productInvoice.InvoiceDetails.Count == 0)
                {
                    await Repository.DeleteAsync(predicate: x => x.Id == Id);
                    return new ResponseBase<Product>(code: HttpStatusCode.OK, message: "Se ha eliminado el registro del vendedor");
                }
                return new ResponseBase<Product>(code: HttpStatusCode.OK, message: "Nose encontro el recurso a eliminar");
            }
            catch (Exception e)
            {
                return new ResponseBase<Product>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<List<Product>>> GetAll()
        {

            try
            {
                var all = await Repository.GetAllAsync();
                return new ResponseBase<List<Product>>(code: HttpStatusCode.OK, message: "Solicitud ok", data: all.ToList());
            }
            catch (Exception e)
            {
                return new ResponseBase<List<Product>>(code: HttpStatusCode.InternalServerError, message: "Error de servidor");
            }
        }

        public async Task<ResponseBase<Product>> GetFind(int Id)
        {
            try
            {
                var product = await Repository.GetAsync(predicate: x => x.Id == Id,
                    include: i=>i.Include(inc=>inc.InvoiceDetails));
                return new ResponseBase<Product>(code: HttpStatusCode.OK, message: "Solicitud Ok", data: product);
            }
            catch (Exception e)
            {
                return new ResponseBase<Product>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<Product>> Update(Product data)
        {
            try
            {
                var product = await Repository.GetAsync(predicate: x => x.Id == data.Id);

                if (product != null)
                {
                    data.UpdateAt = DateTime.Now;
                    data.CreateAt = product.CreateAt;
                    await Repository.UpdateAsync(data);
                    return new ResponseBase<Product>(code: HttpStatusCode.OK, message: "Se ha actualizado la informacion del cliente", data: data);
                }

                return new ResponseBase<Product>(code: HttpStatusCode.OK, message: "La informacion del cliente no existe o esta erronea ", data: data);
            }
            catch (Exception e)
            {
                return new ResponseBase<Product>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

       
    }
}
