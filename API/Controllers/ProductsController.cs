using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase 
    {
      
        private readonly IProductRepository _repo;
       
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;       
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();
             return Ok(products); 
        }

         [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> GetProduct(int id)
        {
             var products = await _repo.GetProductByIdAsync(id);
              return Ok(products);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var brands = await _repo.GetProductBrandsAsync();
             return Ok(brands); 
        }

         [HttpGet("brands/{id}")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrandsBy(int id)
        {
             var brands = await _repo.GetProductBrandsByIdAsync(id);
              return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var types = await _repo.GetProductTypesAsync();
             return Ok(types); 
        }

         [HttpGet("types/{id}")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypeById(int id)
        {
             var types = await _repo.GetProductTypeByIdAsync(id);
              return Ok(types);
        }
    }
}