using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.OpheliaTest.Entities.Models;
using BackEnd.OpheliaTest.Entities.Interface.BusinessRules;

namespace BackEnd.OpheliaTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceBusiness _business;
        public InvoiceController(IInvoiceBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _business.GetAll();
            return StatusCode(200, result);
        }

        [HttpGet("find")]
        public async Task<ActionResult> GetFind(Guid id)
        {
            var result = await _business.GetFind(id);
            return StatusCode(200, result);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] Invoice data)
        {
            var result = await _business.Create(data);
            return StatusCode(200, result);
        }

    
    }
}
