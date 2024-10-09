using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.Services.Product.Dtos;
using productBrands = Store.Data.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Repository.Specification.Productspecs;
using Store.Service.Helper;

namespace Store.Service.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailDto>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsNoTrackingAsync();
            //IReadOnlyList<BrandTypeDetailDto> mappedBrands = Brands.Select(x => new BrandTypeDetailDto
            //{ 
            //    Id = x.Id,
            //    Name = x.Name, 
            //    CreatedAt = x.CreatedAt
            //}).ToList();
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailDto>>(Brands);
            return mappedBrands;

        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new ProductWithSpecification(input);
            var Products = await _unitOfWork.Repository<productBrands, int>().GetAllWithSpecificationAsync(specs);

            var countSpecs = new ProductWithCountSpecification(input);
            var count = await _unitOfWork.Repository<productBrands, int>().GetCountSpecificationAsync(countSpecs);
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(Products);
            return new PaginatedResultDto<ProductDetailsDto>(input.PageSize, input.PageIndex, count, mappedProducts);

        }

        public async Task<IReadOnlyList<BrandTypeDetailDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsNoTrackingAsync();
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailDto>>(Types);



            return mappedBrands;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? ProductId)
        {
            if (ProductId is null)
                throw new Exception("Id is null");

            var specs = new ProductWithSpecification(ProductId);
            var product = await _unitOfWork.Repository<productBrands, int>().GetWithSpecificationByIdAsync(specs);

            if (product is null)
                throw new Exception("Product is null");

            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);
            return mappedProduct;
        }
    } 
}
