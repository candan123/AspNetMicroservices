using Discount.API.Entities;
using Discount.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{productName}",Name ="GetDiscount")]
        [ProducesResponseType(typeof(Coupan),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupan>> GetDiscount(string productName)
        {
            var coupan = await _repository.GetDiscount(productName);
            return Ok(coupan);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(Coupan), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupan>> CreateDiscount([FromBody] Coupan coupan)
        {
            var isDataUpdated = await _repository.CreateDiscount(coupan);
            return CreatedAtRoute("GetDiscount",new { productName= coupan.Productname, coupan});
        }

        [HttpPut()]
        [ProducesResponseType(typeof(Coupan),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupan>> UpdateDiscount([FromBody] Coupan coupan)
        {
            return Ok( await _repository.UpdateDiscount(coupan));
            
        }

        [HttpDelete("{productName}",Name ="DeleteDiscount")]
        [ProducesResponseType(typeof(void),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            return Ok(await _repository.DeleteDiscount(productName));
            
        }
    }
}
