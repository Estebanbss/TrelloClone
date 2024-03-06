using Microsoft.AspNetCore.Mvc;
using TrelloClone.Services;
using TrelloClone.Data.TrelloModels;
using Microsoft.AspNetCore.Authorization;
using TrelloClone.Data.DTOs;

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
     
     [Authorize(Policy = "Authenticated")]
    [HttpGet("all")]
    public async Task<IEnumerable<ListDtoOut>> Get()
    {
        return await _service.GetAll();
    }
     
     [Authorize(Policy = "Authenticated")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ListDtoOut>> GetById(int id)
    {
        var list = await _service.GetDtoById(id);

        if (list == null)
            return ListNotFound(id);

        return list;
    }

     [Authorize(Policy = "Authenticated")]
     [HttpGet("getbyboardid/{boardId}")]
     public async Task<ActionResult<IEnumerable<ListDtoOut>>> GetByBoardId(int boardId)
     {
          var list = await _service.GetByBoardId(boardId);

          if (list == null)
              return ListByBoardNotFound(boardId);
          
          return list.ToList();

          
     }


    [Authorize(Policy = "Authenticated")]
    [HttpPost("create")]
    public async Task<IActionResult> Create(ListDtoIn list)
    {
        var newList = await _service.Create(list);

        return CreatedAtAction(nameof(GetById), new { id = newList.Id }, newList);
    }

    
    [Authorize(Policy = "Authenticated")]
    [HttpPut("update/{id}")]
    public async  Task<IActionResult> Update(int Id, ListDtoIn list)
    {
          if(Id != list.Id)
          {
              return BadRequest(new {message=$"the id = {Id} does not match the list id {list.Id} in the request body"});
          }
        var existingList = await _service.GetById(Id);

        if (existingList is not null)
        {
            await _service.Update(Id, list);
            return NoContent();
        }
        else
        {
            return ListNotFound(Id);
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

    private NotFoundObjectResult ListNotFound(int id)
    {
        return NotFound(new {message=$"List with id = {id} not found"});
    }
    private NotFoundObjectResult ListByBoardNotFound(int id)
    {
        return NotFound(new {message=$"List with board id = {id} not found"});
    }
}
