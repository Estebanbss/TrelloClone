
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.Data.DTOs;
using TrelloClone.Data.TrelloModels;

namespace TrelloClone.Services;
public class BoardService {
 
     private readonly TrelloCloneContext _context;
     public BoardService(TrelloCloneContext context){
          _context = context;
     }

          public async Task<IEnumerable<BoardDtoOut>> GetAll()
     {
         return await _context.Boards.Select(
          b => new BoardDtoOut
          {
              Id = b.Id,
              Name = b.Name,
              AccountId = b.AccountId
          }).ToListAsync();
     }

     public async Task<BoardDtoOut?> GetDtoById(int id)
     {
         return await _context.Boards.Where(b => b.Id == id).Select(
          b => new BoardDtoOut
          {
              Id = b.Id,
              Name = b.Name,
              AccountId = b.AccountId
          }).SingleOrDefaultAsync();
     }

     public async Task<IEnumerable<BoardDtoOut>> GetByAccountId(int accountId)
     {
           return await _context.Boards.Where(b => b.AccountId == accountId).Select(
            b => new BoardDtoOut
            {
                 Id = b.Id,
                 Name = b.Name,
                 AccountId = b.AccountId
            }).ToListAsync();
      }

     public async Task<Board?> GetById(int id)
     {
         return await _context.Boards.FindAsync(id);
     }
     
     public async Task<Board> Create(BoardDtoIn newBoardDTO)
     {
           var newBoard = new Board();

          newBoard.Name = newBoardDTO.Name;
          newBoard.AccountId = newBoardDTO.AccountId;
          
         _context.Boards.Add(newBoard);
         await _context.SaveChangesAsync();

         return newBoard;
     }

     public async Task Update(int id, BoardDtoIn board){

          var existingBoard = await GetById(id);

          if (existingBoard is not null)
          {
              existingBoard.Name = board.Name;
     
              
              await _context.SaveChangesAsync();
          }
     }

     public async Task Delete(int id)
     {
         var boardToDelete = await GetById(id);

         if (boardToDelete is not null)
         {
             _context.Boards.Remove(boardToDelete);
             await _context.SaveChangesAsync();
         }
     }
}