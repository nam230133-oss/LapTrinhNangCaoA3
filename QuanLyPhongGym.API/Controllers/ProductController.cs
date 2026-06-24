using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongGym.Application.Features.Products.Commands.CreateProduct;
using QuanLyPhongGym.Application.Features.Products.Commands.DeleteProduct;
using QuanLyPhongGym.Application.Features.Products.Commands.SellProduct;
using QuanLyPhongGym.Application.Features.Products.Commands.UpdateProduct;
using QuanLyPhongGym.Application.Features.Products.Queries.GetProductStats;
using QuanLyPhongGym.Application.Features.Products.Queries.GetAllProducts;

using System;
using System.Threading.Tasks;

namespace QuanLyPhongGym.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetProductStats()
        {
            return Ok(await _mediator.Send(new GetProductStatsQuery()));
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPost("sell")]
        public async Task<IActionResult> SellProduct([FromBody] SellProductCommand command)
        {
            // Gọi handler và nhận kết quả chi tiết
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { message = "Sản phẩm đã hết hàng hoặc không tồn tại!" });

            // Thông báo tùy chỉnh theo yêu cầu của bạn
            return Ok(new
            {
                message = $"Đã bán {result.Quantity} sản phẩm \"{result.ProductName}\" thành công."
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            if (command == null) return BadRequest("Dữ liệu không hợp lệ.");

            var result = await _mediator.Send(command);

            return Ok(new
            {
                message = $"Đã thêm sản phẩm \"{result.Name}\" thành công.",
                productId = result.Id
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command with { Id = id });
            if (!result) return BadRequest("Sản phẩm không tồn tại!");
            return Ok(new { message = "Cập nhật thành công." });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (!result) return BadRequest("Sản phẩm không tồn tại!");
            return Ok(new { message = "Xóa thành công." });
        }
        [Authorize(Roles = "Admin")]
        [HttpGet] // Đường dẫn là api/product
        public async Task<IActionResult> GetAll()
        {
            // Đảm bảo không có hàm GetAll khác trong file này
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }
    }
}