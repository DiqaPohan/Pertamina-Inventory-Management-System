using Microsoft.AspNetCore.Mvc;
using Application.Services.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Pertamina.SolutionTemplate.WebApi.Areas.V1.Controllers;

public class ItemsController : ApiControllerBase
{
    private readonly ISolutionTemplateDbContext _context;

    public ItemsController(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems()
    {
        // Mengambil data dari tabel Items yang baru kita buat
        return await _context.Items.ToListAsync();
    }
}