using Microsoft.AspNetCore.Mvc;
using backend.data;
using Microsoft.EntityFrameworkCore;
using backend.models;
using Microsoft.AspNetCore.Cors;

namespace backend.Controllers
{
  [EnableCors]
  [ApiController]
  public class HomeController : ControllerBase
  {
    [HttpGet("/")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
      => Ok(await context.Todos.ToListAsync());

    [HttpPost("/")]
    public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] Todo todo)
    {
      await context.AddAsync(todo);
      await context.SaveChangesAsync();
      return Ok(todo);
    }

    [HttpGet("/{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] Guid id)
    {
      var model = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
        return NotFound();

      return Ok(model);
    }

    [HttpPut("/{id:Guid}")]
    public async Task<IActionResult> Put([FromServices] AppDbContext context, [FromBody] Todo todo, [FromRoute] Guid id)
    {
      var model = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
        return NotFound();

      model.Title = todo.Title;
      model.Status = todo.Status;

      context.Update(model);
      await context.SaveChangesAsync();

      return Ok(model);
    }

    [HttpDelete("/{id:Guid}")]
    public async Task<IActionResult> Delete([FromServices] AppDbContext context, [FromRoute] Guid id)
    {
      var model = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
        return NotFound();

      context.Todos.Remove(model);
      await context.SaveChangesAsync();
      return Ok(model);
    }
  }
}