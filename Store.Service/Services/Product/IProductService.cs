using Store.Repository.Specification.Productspecs;
using Store.Service.Helper;
using Store.Service.Services.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Product
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int? ProductId);
        Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification specs);
        Task<IReadOnlyList<BrandTypeDetailDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<BrandTypeDetailDto>> GetAllTypesAsync();
    }
}
