using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.OpheliaTest.Entities.Interface.BusinessRules;

namespace BackEnd.OpheliaTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _business;
        public ProductController(IProductBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var res = await _business.GetAll();
            return StatusCode(res.Code, res);
        }

    }
}
