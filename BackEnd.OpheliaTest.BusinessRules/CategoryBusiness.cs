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
    
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly IBaseRepository<Category> Repository;

        public CategoryBusiness(IBaseRepository<Category> repository)
        {
            Repository = repository;
        }
        public async Task<ResponseBase<Category>> Create(Category data)
        {
            try
            {
                var category =  await Repository.GetAsync(predicate: x=>x.Id == data.Id);
               
                if(category == null)
                {
                    await Repository.AddAsync(data);
                    return new ResponseBase<Category>(code: HttpStatusCode.OK, message: "Se ha creado un nuevo cliente");
                }
                return new ResponseBase<Category>(code: HttpStatusCode.BadRequest, message: "El Cliente se encuentra registrado en el sistema");

            }
            catch (Exception e)
            {
                return new ResponseBase<Category>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<Category>> Delete(int Id)
        {
            try
            {
                var categoryProduct = await Repository.GetAsync(predicate: x => x.Id == Id,
                        include: i => i.Include(inc => inc.Products)
                    );

                if (categoryProduct != null && categoryProduct.Products.Count == 0)
                {
                    await Repository.DeleteAsync(predicate: x => x.Id == Id);
                    return new ResponseBase<Category>(code: HttpStatusCode.OK, message: "Se ha eliminado el registro dela categoria");
                }
                return new ResponseBase<Category>(code: HttpStatusCode.OK, message: "Nose encontro el recurso a eliminar");
            }
            catch (Exception e)
            {
                return new ResponseBase<Category>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<List<Category>>> GetAll()
        {

            try
            {
                var all = await Repository.GetAllAsync();
                return new ResponseBase<List<Category>>(code: HttpStatusCode.OK, message: "Solicitud ok", data: all.ToList());
            }
            catch(Exception e)
            {
                return new ResponseBase<List<Category>>(code: HttpStatusCode.InternalServerError, message: "Error de servidor");
            }
        }

        public async Task<ResponseBase<Category>> GetFind(int Id)
        {
            try
            {
                var categories = await Repository.GetAsync(predicate:x=>x.Id == Id,
                    include: i => i.Include(inc => inc.Products));
                return new ResponseBase<Category>(code: HttpStatusCode.OK, message: "Solicitud Ok", data: categories);
            }
            catch (Exception e)
            {
                return new ResponseBase<Category>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        public async Task<ResponseBase<Category>> Update(Category data)
        {
            try
            {
                var category = await Repository.GetAsync(predicate: x=>x.Id == data.Id);
                if (category != null)
                {
                    data.UpdateAt = DateTime.Now;
                    data.CreateAt = category.CreateAt;
                    await Repository.UpdateAsync(data);
                    return new ResponseBase<Category>(code: HttpStatusCode.OK, message: "Se ha actualizado la informacion dela categoria", data: data);
                }
                
                return new ResponseBase<Category>(code: HttpStatusCode.OK, message: "La informacion dela categoria no existe o esta erronea ", data: data);
            }
            catch (Exception e)
            {
                return new ResponseBase<Category>(code: HttpStatusCode.InternalServerError, message: "error de servidor");
            }
        }

        
    }
}
