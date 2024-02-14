using Microsoft.AspNetCore.Mvc;
using TrelloClone.Services;
using TrelloClone.Data.TrelloModels;
using TrelloClone.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace TrelloClone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController: ControllerBase
{

     private readonly AccountService _service;
     public AccountController(AccountService account)
     {
         _service = account;
     }

     [HttpGet("all")]
     public async Task<IEnumerable<AccountDtoOut>> Get()
     {
         return await _service.GetAll();
     }

     
     [HttpGet("get/{id}")]
     public async Task<ActionResult<AccountDtoOut>> GetById(int id)
     {
         var account = await _service.GetDtoById(id);

         if (account == null)
             return AccountNotFound(id);

          return account;
     }
     

     [HttpPost("create")]
     public async Task<IActionResult> Create(AccountDtoIn account)
     {
          var newAccount = await _service.Create(account);

         return CreatedAtAction(nameof(GetById), new { id = newAccount.Id }, newAccount);
     }


     [Authorize(Policy = "Authenticated")]
     [HttpPut("update/{id}")]
     public async  Task<IActionResult> Update(int id, AccountDtoIn account)
     {
         if (id != account.Id)
             return BadRequest(new {message=$"the id = {id} does not match the account id {account.Id} in the request body"});

          var accountToUpdate = await _service.GetById(id);
     
           if (accountToUpdate is not null)
          {
                await _service.Update(id, account);
                return NoContent(); 
          }
          else
          {
               return AccountNotFound(id);
          }
     }

     [Authorize(Policy = "Authenticated")]
     [HttpDelete("delete/{id}")]

     public async Task<IActionResult> Delete(int id)
     {
         var existingAccount = await _service.GetById(id);

          if(existingAccount is not null)
          {
               await _service.Delete(id);
               return Ok();
          }
          else
          {
               return AccountNotFound(id);
          }
     }

     public NotFoundObjectResult AccountNotFound(int id)
     {
         return NotFound(new {message=$"Account not found = {id} does not exist"});
     }
}