
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.Data.TrelloModels;

namespace TrelloClone.Services;

 public class CardService 
 {
  
      private readonly TrelloCloneContext _context;
      public CardService(TrelloCloneContext context){
           _context = context;
      }

      public async Task<IEnumerable<Card>> GetAll()
      {
          return await _context.Cards.ToListAsync();
      }

      public async Task<Card?> GetById(int id)
      {
          return await _context.Cards.FindAsync(id);
      }

      public async Task<Card> Create(Card card)
      {
          _context.Cards.Add(card);
          await _context.SaveChangesAsync();

          return card;
      }

      public async Task Update(int id, Card card){

           var existingCard = await GetById(id);

           if (existingCard is not null)
           {
               existingCard.Title = card.Title;
               existingCard.Description = card.Description;
               existingCard.Comment = card.Comment;
               existingCard.Labels = card.Labels;
               existingCard.Cover = card.Cover;
               existingCard.ListId = card.ListId;
      
               
               await _context.SaveChangesAsync();
           }
      }

      public async Task Delete(int id)
      {
          var cardToDelete = await GetById(id);

          if (cardToDelete is not null)
          {
              _context.Cards.Remove(cardToDelete);
              await _context.SaveChangesAsync();
          }
      }
 }