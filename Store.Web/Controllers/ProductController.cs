using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ninject.Activation.Caching;
using Store.Repository.Specification.Productspecs;
using Store.Service.Services.Product;
using Store.Service.Services.Product.Dtos;

namespace Store.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetAllBrands")]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailDto>>> GetAllBrands()
            =>Ok(await _productService.GetAllBrandsAsync());

        [HttpGet("GetAllTypes")]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailDto>>> GetAllTypes()
          => Ok(await _productService.GetAllTypesAsync());

        [HttpGet] 
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAllProducts([FromQuery] ProductSpecification input)
         => Ok(await _productService.GetAllProductsAsync(input));

        [HttpGet("GetProductById")]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailDto>>> GetProductById(int? Id)
                 => Ok(await _productService.GetProductByIdAsync(Id));
    }
}
