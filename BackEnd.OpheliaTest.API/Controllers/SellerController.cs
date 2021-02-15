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
    public class SellerController : ControllerBase
    {
        private readonly ISellerBusiness _business;
        public SellerController(ISellerBusiness business)
        {
            _business = business;
        }


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _business.GetAll();
            return StatusCode(200, result);
        }

        [HttpGet("find")]
        public async Task<ActionResult> GetFind(int id)
        {
            var result = await _business.GetFind(id);
            return StatusCode(200, result);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] Seller data)
        {
            var result = await _business.Create(data);
            return StatusCode(200, result);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] Seller data)
        {
            var result = await _business.Update(data);
            return StatusCode(200, result);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _business.Delete(id);
            return StatusCode(200, result);
        }
    }
}
