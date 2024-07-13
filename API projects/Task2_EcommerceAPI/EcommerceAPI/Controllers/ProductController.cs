using AutoMapper;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
      
        private readonly IUnitOfWork<Products> unitOfWork;
        private readonly IMapper mapper;
        public ApiResponse response;
   
        public ProductController( IUnitOfWork<Products> unitOfWork, IMapper mapper) {
           
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            response = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult <ApiResponse>> GetAll()
        {
            var model = await unitOfWork.ProductRepository.GetAll();
            var check =model.Any(); // to check the model has products or null
            if (check)
            {
                response.StatusCode=System.Net.HttpStatusCode.OK;
                response.IsSuccess=true; //will be the value of check
                var mappedProducts = mapper.Map<IEnumerable<Products>, IEnumerable<ProductDTO>>(model);
                response.Result= mappedProducts;
            return response;
            }
            else{
                response.ErrorMessages = "No products, products not yet added";
                response.StatusCode = System.Net.HttpStatusCode.OK; // no products added. the response is ok
                response.IsSuccess= false;
                return response;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id) { 

            var model = await unitOfWork.ProductRepository.GetById(id);
            return Ok(model);
        
        }



        [HttpPost]
        public async Task<ActionResult> CreateProduct(Products product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }
            await unitOfWork.ProductRepository.Create(product);
            await unitOfWork.Save();

            var response = new ApiResponse
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                IsSuccess = true,
                Result = product
            };

             //return CreatedAtAction(nameof(GetById), new { id = product.Id }, response);
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Products product) {

            unitOfWork.ProductRepository.Update(product);
            await unitOfWork.Save(); ;
            return Ok(product);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            unitOfWork.ProductRepository.Delete(id);
            await unitOfWork.Save();
            return Ok();
        }

        [HttpGet("Product/{categoryId}")]
        public async Task<ActionResult<ApiResponse>> getProductByCategoryId(int categoryId)
        {
            var products=await unitOfWork.ProductRepository.GetAllProductsByCategoryId(categoryId);
            var mappedProducts=mapper.Map<IEnumerable< Products>,IEnumerable<ProductDTO>>(products);
            return Ok(mappedProducts);
        }

        [HttpPost("createNewProductByCategoryId")]
        public async Task<ActionResult<ApiResponse>> createNewProductByCategoryId([FromBody] ProductPostDTO productPostDTO)
        {

            var category = await unitOfWork.CategoryRepository.GetCategoryByCategoryId(productPostDTO.CategoryId);

            if (category == null)
            {
                return BadRequest("Invalid Category_Id.");
            }

            var product = mapper.Map<ProductPostDTO, Products>(productPostDTO);

            // Use the logic of CreateProduct method to create the product and return response
            return await CreateProduct(product);


        }
    }
}


