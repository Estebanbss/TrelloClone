
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.Data.TrelloModels;

namespace TrelloClone.Services;

public class ListService 
{
    private readonly TrelloCloneContext _context;
    public ListService(TrelloCloneContext context){
        _context = context;
    }

    public async Task<IEnumerable<List>> GetAll()
    {
        return await _context.Lists.ToListAsync();
    }

    public async Task<List?> GetById(int id)
    {
        return await _context.Lists.FindAsync(id);
    }

    public async Task<List> Create(List list)
    {
        _context.Lists.Add(list);
        await _context.SaveChangesAsync();

        return list;
    }

    public async Task Update(int id, List list){

        var existingList = await GetById(id);

        if (existingList is not null)
        {
            existingList.Name = list.Name;
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
