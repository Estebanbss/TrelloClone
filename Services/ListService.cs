
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.Data.DTOs;
using TrelloClone.Data.TrelloModels;

namespace TrelloClone.Services;

public class ListService 
{
    private readonly TrelloCloneContext _context;
    public ListService(TrelloCloneContext context){
        _context = context;
    }

     public async Task<IEnumerable<ListDtoOut>> GetAll()
     {
           return await _context.Lists.Select(
            l => new ListDtoOut
            {
                 Id = l.Id,
                 Name = l.Name,
                 BoardId = l.BoardId
            }).ToListAsync();
     }

     public async Task<List?> GetById(int id)
     {
          return await _context.Lists.FindAsync(id);
     }

     public async Task<ListDtoOut?> GetDtoById(int id)
     {
          return await _context.Lists.Where(l => l.Id == id).Select(
               l => new ListDtoOut
               {
                    Id = l.Id,
                    Name = l.Name,
                    BoardId = l.BoardId
               }).SingleOrDefaultAsync();
     }

     public async Task<IEnumerable<ListDtoOut>> GetByBoardId(int boardId)
     {
          return await _context.Lists.Where(l => l.BoardId == boardId).Select(
               l => new ListDtoOut
               {
                    Id = l.Id,
                    Name = l.Name,
                    BoardId = l.BoardId
               }).ToListAsync();
     }

     public async Task<List> Create(ListDtoIn newListDTO)
     {
          var newList = new List();

          newList.Name = newListDTO.Name;
          newList.BoardId = newListDTO.BoardId;

          _context.Lists.Add(newList);
          await _context.SaveChangesAsync();

          return newList;
     }

     public async Task Update(int id, ListDtoIn list)
     {
          var existingList = await GetById(id);

          if (existingList is not null)
          {
               existingList.Name = list.Name;
               existingList.BoardId = list.BoardId;
               await _context.SaveChangesAsync();
          }
     }


    public async Task Delete(int id)
    {
        var listToDelete = await GetById(id);

        if (listToDelete is not null)
        {
            _context.Lists.Remove(listToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
