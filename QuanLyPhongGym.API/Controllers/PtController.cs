using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class PtController : ControllerBase
{
    private readonly IApplicationDbContext _context;
    public PtController(IApplicationDbContext context) => _context = context;
}