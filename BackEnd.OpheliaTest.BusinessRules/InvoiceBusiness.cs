using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BackEnd.OpheliaTest.Entities.Interface.BusinessRules;
using BackEnd.OpheliaTest.Entities.Interface.Repository;
using BackEnd.OpheliaTest.Entities.Models;
using BackEnd.OpheliaTest.Entities.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BackEnd.OpheliaTest.BusinessRules
{
    public class InvoiceBusiness : IInvoiceBusiness
    {
        private readonly IBaseRepository<Invoice> InvoiceRepo;
        private readonly IBaseRepository<InvoiceDetail> InvoiceDetailtRepo;
        private readonly IBaseRepository<Product> ProductRepository;
        private readonly IInvoiceRepository invoiceRepositorie;

        public InvoiceBusiness(
            IBaseRepository<Invoice> repository,IBaseRepository<InvoiceDetail> repositoryDetailt, 
            IBaseRepository<Product>productRepo,
            IInvoiceRepository invoiceRepository)
        {
            InvoiceRepo = repository;
            InvoiceDetailtRepo = repositoryDetailt;
            ProductRepository = productRepo;
            invoiceRepositorie = invoiceRepository;
        }

       
        public async Task<ResponseBase<List<Invoice>>> GetAll()
        {
            try{
                var all = await InvoiceRepo.GetAllAsync();
                return new ResponseBase<List<Invoice>>(message:"Solicitud Ok",code:HttpStatusCode.OK,data:all.ToList());
            }catch{
                return new ResponseBase<List<Invoice>>(message:"Error de servidor",code:HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseBase<Invoice>> GetFind(Guid Id)
        {
           try{
                var invoice = await InvoiceRepo.GetAsync(predicate:x=>x.Id == Id,
                include:i=>i.Include(inc=>inc.InvoiceDetails));

                return new ResponseBase<Invoice>(message:"Solicitud Ok",code:HttpStatusCode.OK,data:invoice);

            }catch{
                return new ResponseBase<Invoice>(message:"Error de servidor",code:HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseBase<Invoice>> Create(Invoice data)
        {
            try{
                List<InvoiceDetail> addDetail = new List<InvoiceDetail>();
                List<Product> productUpdate = new List<Product>();

                var invoiceAdd =  new Invoice(){
                    Id= Guid.NewGuid(),
                    InvoiceDate = data.InvoiceDate,
                    SellerId = data.SellerId,
                    ClientId = data.ClientId
                };

                foreach(InvoiceDetail item in data.InvoiceDetails)
                {
                    var product = await ProductRepository.GetAsync(predicate: x => x.Id == item.IdProduct);
                    product.Cuantity = product.Cuantity - item.Cuantity;
                    product.UpdateAt = DateTime.Now;
                    item.IdInvoice = invoiceAdd.Id;

                    addDetail.Add(item);
                    productUpdate.Add(product);
                }

                await InvoiceRepo.AddAsync(invoiceAdd);
                await InvoiceDetailtRepo.AddAsync(addDetail);
                await ProductRepository.UpdateAsync(productUpdate);

                return new ResponseBase<Invoice>(message:"Solicitud Ok",code:HttpStatusCode.OK);
            }catch(Exception e){
                 return new ResponseBase<Invoice>(message:"Error de servidor",code:HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseBase<List<Client>>> FilterClient()
        {
            try{
                var list = await invoiceRepositorie.getFilterClient();
                return new ResponseBase<List<Client>>(message:"Solicitud Ok",code:HttpStatusCode.OK,data: list);

            }catch(Exception e){
                return new ResponseBase<List<Client>>(message:"Error de servidor",code:HttpStatusCode.InternalServerError);
            }
        }

        public Task<ResponseBase<dynamic>> PurchaseFrequency()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseBase<List<ProductoRequest>>> TotalSold()
        {
            try
            {
                var list =await  invoiceRepositorie.GetTotalSold();
                return new ResponseBase<List<ProductoRequest>>(message: "Solicitud Ok", code: HttpStatusCode.OK, data: list);

            }
            catch
            {
                return new ResponseBase<List<ProductoRequest>>(message: "Error de servidor", code: HttpStatusCode.InternalServerError);
            }
        }
    }
}
