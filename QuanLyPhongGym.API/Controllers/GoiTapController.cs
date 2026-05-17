using Microsoft.AspNetCore.Mvc;
using MediatR;
using QuanLyPhongGym.Application.Features.GoiTaps.Commands;
using System;
using System.Threading.Tasks;

namespace QuanLyPhongGym.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Đường dẫn cơ bản: api/GoiTap [cite: 182, 183]
    public class GoiTapController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GoiTapController(IMediator mediator) => _mediator = mediator;

        /// Use Case 3: Tạo gói tập mới (Tháng, Quý, Năm, PT) [cite: 457, 459]
        /// </summary>
        [HttpPost("Create")] // URL: POST api/GoiTap/Create
        public async Task<IActionResult> Create([FromBody] CreateGoiTapCommand command)
        {
            var result = await _mediator.Send(command);

            // Trả về 200 OK kèm theo ID của gói tập vừa tạo [cite: 236]
            return Ok(new
            {
                message = "Tạo gói tập thành công",
                packageId = result
            });
        }

        // Bạn nên thêm phương thức GET để lấy danh sách gói tập phục vụ việc chọn gói khi đăng ký thẻ [cite: 232, 310]
        [HttpGet("GetList")] // URL: GET api/GoiTap/GetList
        public async Task<IActionResult> GetList()
        {
            // Logic lấy danh sách gói tập từ Database [cite: 187]
            // var query = new GetGoiTapListQuery();
            // var result = await _mediator.Send(query);
            // return Ok(result);
            return Ok("Tính năng lấy danh sách gói tập");
        }
    }
}