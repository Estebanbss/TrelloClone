using Microsoft.AspNetCore.Mvc;
using TrelloClone.Services;
using TrelloClone.Data.TrelloModels;
using Microsoft.AspNetCore.Authorization;

namespace TrelloClone.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class ListController: ControllerBase
{
    private readonly ListService _service;
    public ListController(ListService list)
    {
        _service = list;
    }

    [HttpGet("all")]
    public async Task<IEnumerable<List>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List>> GetById(int id)
    {
        var list = await _service.GetById(id);

        if (list == null)
            return ListNotFound(id);

        return list;
    }

     
    [Authorize(Policy = "Authenticated")]
    [HttpPost("create")]
    public async Task<IActionResult> Create(List list)
    {
        var newList = await _service.Create(list);

        return CreatedAtAction(nameof(GetById), new { id = newList.Id }, newList);
    }

    
    [Authorize(Policy = "Authenticated")]
    [HttpPut("update/{id}")]
    public async  Task<IActionResult> Update(int id, List list)
    {
        if (id != list.Id)
            return BadRequest(new {message=$"the id = {id} does not match the list id {list.Id} in the request body"});

        var listToUpdate = await _service.GetById(id);

        if (listToUpdate is not null)
        {
            await _service.Update(id, list);
            return NoContent(); 
        }
        else
        {
            return ListNotFound(id);
        }
    }
 
    [Authorize(Policy = "Authenticated")]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var listToDelete = await _service.GetById(id);

        if (listToDelete is not null)
        {
            await _service.Delete(id);
            return NoContent();
        }
        else
        {
            return ListNotFound(id);
        }
    }

    private ActionResult ListNotFound(int id)
    {
        return NotFound(new {message=$"List with id = {id} not found"});
    }
}
