using Microsoft.AspNetCore.Mvc;
using TrelloClone.Services;
using TrelloClone.Data.TrelloModels;
using Microsoft.AspNetCore.Authorization;
using TrelloClone.Data.DTOs;

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
    
    [Authorize(Policy = "Authenticated")]
    [HttpGet("all")]
    public async Task<IEnumerable<CardDtoOut>> Get()
    {
            return await _service.GetAll();
    }
    
    [Authorize(Policy = "Authenticated")]
    [HttpGet("get/{id}")]
    public async Task<ActionResult<CardDtoOut>> GetById(int id)
     {
          var card = await _service.GetDtoById(id);
     
          if (card == null)
               return CardNotFound(id);
     
          return card;
     }

     [Authorize(Policy = "Authenticated")]
     [HttpGet("getbyList/{id}")]
     public async Task<ActionResult<IEnumerable<CardDtoOut>>> GetByListId(int id)
     {
         var cards = await _service.GetByListId(id);

         if (cards.Count() == 0)
             return CardsNotFound(id);

         return cards.ToList();
     }

     
    [Authorize(Policy = "Authenticated")]
    [HttpPost("create")]
    public async Task<IActionResult> Create(CardDtoIn card)
    {
        var newCard = await _service.Create(card);

        return CreatedAtAction(nameof(GetById), new { id = newCard.Id }, newCard);
    }
     
    [Authorize(Policy = "Authenticated")]
    [HttpPut("update/{id}")]
    public async  Task<IActionResult> Update(int id, CardDtoIn card){

        if(id != card.Id)
        {
            return BadRequest(new {message=$"the id = {id} does not match the card id {card.Id} in the request body"});
        }
        var existingCard = await _service.GetById(id);

        if (existingCard is not null)
        {
            await _service.Update(id, card);
            return NoContent();
        }
        else
        {
            return CardNotFound(id);
        }
     }


     [Authorize(Policy = "Authenticated")]
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
     private ActionResult CardsNotFound(int id)
     {
          return NotFound(new {message=$"No cards found for list with id {id}"});
     }
}