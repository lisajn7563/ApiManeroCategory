using ApiManeroCategory.Contexts;
using ApiManeroCategory.Entites;
using ApiManeroCategory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiManeroCategory.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpPost]
    public async Task<IActionResult> Create(CategoryRegistration model)
    {
        if (ModelState.IsValid)
        {
            var entity = new CategoryEntity
            {
                id = model.id,
                categoryTitle = model.categoryTitle,
            };
            _context.Category.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(entity);
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categoryList = await _context.Category.ToListAsync();

        return Ok(categoryList);

    }
}

   

