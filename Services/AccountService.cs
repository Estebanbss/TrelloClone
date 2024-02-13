
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.Data.DTOs;
using TrelloClone.Data.TrelloModels;

namespace TrelloClone.Services;

public class AccountService 
{

     private readonly TrelloCloneContext _context;
     public AccountService(TrelloCloneContext context)
     {
         _context = context;
     }

     public async Task<IEnumerable<AccountDtoOut>> GetAll()
     {
         return await _context.Accounts.Select(a => new AccountDtoOut
         {
                Id = a.Id,
                Username = a.Username,
                Email = a.Email,
                Photo = a.Photo,
                Atype = a.Atype
         }).ToListAsync();
     }

          public async Task<AccountDtoOut?> GetDtoById(int id)
     {
         return await _context.Accounts.Where(a => a.Id == id).Select(a => new AccountDtoOut
         {
                Id = a.Id,
                Username = a.Username,
                Email = a.Email,
                Photo = a.Photo,
                Atype = a.Atype
         }).SingleOrDefaultAsync();
     }

     public async Task<Account?> GetById(int id)
     {
         return await _context.Accounts.FindAsync(id);
     }

     public async Task<Account> Create(AccountDtoIn newaccountDTO)
     {
          var newAccount = new Account();

          newAccount.Username = newaccountDTO.Username;
          newAccount.Email = newaccountDTO.Email;
          newAccount.Photo = newaccountDTO.Photo;
          newAccount.Pwd = newaccountDTO.Pwd;
          newAccount.Atype = newaccountDTO.Atype;
     

         _context.Accounts.Add(newAccount);
         await _context.SaveChangesAsync();

         return newAccount;
     }

     public async Task Update(int id, AccountDtoIn account){

          var existingAccount = await GetById(id);

          if (existingAccount is not null)
          {
              existingAccount.Username = account.Username;
              existingAccount.Email = account.Email;
              existingAccount.Photo = account.Photo;
              existingAccount.Pwd = account.Pwd;
              existingAccount.Atype = account.Atype;
              
              await _context.SaveChangesAsync();
          }
     }

     public async Task Delete(int id)
     {
         var accountToDelete = await GetById(id);

         if (accountToDelete is not null)
         {
             _context.Accounts.Remove(accountToDelete);
             await _context.SaveChangesAsync();
         }
     }

}