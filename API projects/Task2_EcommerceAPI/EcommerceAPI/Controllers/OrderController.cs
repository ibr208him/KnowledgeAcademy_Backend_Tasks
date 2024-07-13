using AutoMapper;
using Azure;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork<Orders> unitOfWork;
        private readonly IMapper mapper;
        public ApiResponse response;

        public OrderController(IUnitOfWork<Orders> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            response = new ApiResponse();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ApiResponse>> GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = await unitOfWork.OrderRepository.GetOrdersByUserId(userId);

                if (orders == null || !orders.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        IsSuccess = false,
                        ErrorMessages = "No orders found for the specified user."
                    });
                }

                var orderDtos = mapper.Map<IEnumerable<OrderDTO>>(orders);

                return Ok(new ApiResponse
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = orderDtos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = ex.Message
                });
            }
        }
    }
}
