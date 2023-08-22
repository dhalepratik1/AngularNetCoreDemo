using API.DTO;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    
    public class ProductsController : BaseApiController 
    {
       private readonly IGenericRepository<Product> _productsRepo;
       private readonly IGenericRepository<ProductBrand> _producBrandRepo;
       private readonly IGenericRepository<ProductType> _productTypeRepo;
       private readonly IMapper _mapper;
       
        public ProductsController(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> producBrandRepo, 
        IGenericRepository<ProductType> productTypeRepo,
        IMapper mapper)
        {
           _productsRepo = productsRepo;
           _producBrandRepo = producBrandRepo;
           _productTypeRepo = productTypeRepo;
           _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts(
            [FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);

            var product = await _productsRepo.ListAsync(spec);

            var data = _mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(product);
            
            return Ok(new Pagination<ProductToReturnDTO>(productParams.PageIndex,
            productParams.PageSize,totalItems,data)); 
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponce),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
           
            var product = await _productsRepo.GetEntityWithSpec(spec);
           
            if (product == null) return NotFound(new ApiResponce(404));

            return _mapper.Map<Product, ProductToReturnDTO>(product);
            //return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var brands = await _producBrandRepo.ListAllAsync();
             return Ok(brands); 
        }

         [HttpGet("brands/{id}")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrandsBy(int id)
        {
             var brands = await _producBrandRepo.GetByIdAsync(id);
              return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var types = await _productTypeRepo.ListAllAsync();
             return Ok(types); 
        }

         [HttpGet("types/{id}")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypeById(int id)
        {
             var types = await _productTypeRepo.GetByIdAsync(id);
              return Ok(types);
        }
    }
}