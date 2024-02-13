using Microsoft.AspNetCore.Mvc;
using TrelloClone.Services;
using TrelloClone.Data.TrelloModels;
using Microsoft.AspNetCore.Authorization;

namespace TrelloClone.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class CardController: ControllerBase
{
    private readonly CardService _service;
    public CardController(CardService card)
    {
        _service = card;
    }

    [HttpGet("all")]
    public async Task<IEnumerable<Card>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<Card>> GetById(int id)
    {
        var card = await _service.GetById(id);

        if (card == null)
            return CardNotFound(id);

        return card;
    }

     
     [Authorize(Policy = "SuperAdmin")]
    [HttpPost("create")]
    public async Task<IActionResult> Create(Card card)
    {
        var newCard = await _service.Create(card);

        return CreatedAtAction(nameof(GetById), new { id = newCard.Id }, newCard);
    }
     
     [Authorize(Policy = "SuperAdmin")]
    [HttpPut("update/{id}")]
    public async  Task<IActionResult> Update(int id, Card card)
    {
        if (id != card.Id)
            return BadRequest(new {message=$"the id = {id} does not match the card id {card.Id} in the request body"});

        var cardToUpdate = await _service.GetById(id);

        if (cardToUpdate is not null)
        {
            await _service.Update(id, card);
            return NoContent(); 
        }
        else
        {
            return CardNotFound(id);
        }
    }


     [Authorize(Policy = "SuperAdmin")]
     [HttpDelete("delete/{id}")]
     public async Task<IActionResult> Delete(int id)
     {
         var card = await _service.GetById(id);
         if (card is not null)
         {
             await _service.Delete(id);
             return NoContent();
         }
         else
         {
             return CardNotFound(id);
         }
     }

     private ActionResult CardNotFound(int id)
     {
          return NotFound(new {message=$"Card with id {id} not found"});
     }
}