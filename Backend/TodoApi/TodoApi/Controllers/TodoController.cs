using Microsoft.AspNetCore.Mvc;
using TodoApi.Service.Queries;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : Controller
{
    private readonly ITodoQueryHandler _queryHandler;

    public TodoController(ITodoQueryHandler queryHandler)
    {
        _queryHandler = queryHandler;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetTodo()
    {
        var query = new GetAllTodosQuery();
        var result = await _queryHandler.Handle(query, new CancellationToken());
        return Ok(result);
    }
}