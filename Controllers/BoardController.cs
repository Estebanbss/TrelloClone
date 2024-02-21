using Microsoft.AspNetCore.Mvc;
using TrelloClone.Services;
using TrelloClone.Data.TrelloModels;
using TrelloClone.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace TrelloClone.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BoardController: ControllerBase
{
 
    private readonly BoardService _service;
    public BoardController(BoardService board)
    {
        _service = board;
    }
     
    [Authorize(Policy = "Authenticated")]
    [HttpGet("all")]
    public async Task<IEnumerable<Board>> Get()
    {
        return await _service.GetAll();
    }
     
     [Authorize(Policy = "Authenticated")]
     [HttpGet("get/{id}")]
     public async Task<ActionResult<Board>> GetById(int id)
     {
         var board = await _service.GetById(id);

         if (board == null)
             return BoardNotFound(id);

          return board;
     }

     [Authorize(Policy = "Authenticated")]
     [HttpPost("create")]
     public async Task<IActionResult> Create(BoardDtoIn board)
     {
          var newBoard = await _service.Create(board);

         return CreatedAtAction(nameof(GetById), new { id = newBoard.Id }, newBoard);
     }

     [Authorize(Policy = "Authenticated")]
     [HttpPut("update/{id}")]

     public async  Task<IActionResult> Update(int id, BoardDtoIn board)
     {
         if (id != board.Id)
             return BadRequest(new {message=$"the id = {id} does not match the board id {board.Id} in the request body"});

          var boardToUpdate = await _service.GetById(id);
     
           if (boardToUpdate is not null)
          {
                await _service.Update(id, board);
                return NoContent(); 
          }
          else
          {
               return BoardNotFound(id);
          }
     }

     [Authorize(Policy = "Authenticated")]
     [HttpDelete("delete/{id}")]

     public async Task<IActionResult> Delete(int id)
     {
         var boardToDelete = await _service.GetById(id);

         if (boardToDelete is not null)
         {
             await _service.Delete(id);
             return NoContent();
         }
         else
         {
             return BoardNotFound(id);
         }
     }

     private ActionResult BoardNotFound(int id)
     {
         return NotFound(new {message=$"Board with id = {id} not found"});
     }


}