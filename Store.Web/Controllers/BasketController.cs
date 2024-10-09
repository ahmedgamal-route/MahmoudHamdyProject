using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketServices.Dto;
using Store.Service.Services.BasketServices;

namespace Store.Web.Controllers
{
 
    public class BasketController : BaseController
    {
        private readonly IBasketServices _basketServices;

        public BasketController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketAsync(string? id)
            => Ok(await _basketServices.GetBasketAsync(id));

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto input)
           => Ok(await _basketServices.UpdateBasketAsync(input));

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string? id)
           => Ok(await _basketServices.DeleteBasketAsync(id));
    }
}
