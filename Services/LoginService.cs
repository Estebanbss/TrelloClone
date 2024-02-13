
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.Data.DTOs;
using TrelloClone.Data.TrelloModels;

namespace TrelloClone.Services;

public class LoginService
{
     private readonly TrelloCloneContext _context;
    public LoginService(TrelloCloneContext context){
        _context = context;
    }

     public async Task<Account?> GetUser(LoginDto user)
     {
          return await 
                         _context.Accounts.
                         SingleOrDefaultAsync( x => x.Email == user.Email && x.Pwd == user.Pwd);
     }


}