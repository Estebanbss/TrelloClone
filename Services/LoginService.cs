
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

     public async Task<Admin?> GetAdmin(AdminDto admin)
     {
          return await 
                         _context.Admins.
                         SingleOrDefaultAsync( x => x.Email == admin.Email && x.Pwd == admin.Pwd);
     }


}