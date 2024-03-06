
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.Data.DTOs;
using TrelloClone.Data.TrelloModels;

namespace TrelloClone.Services;

 public class CardService 
 {
  
      private readonly TrelloCloneContext _context;
      public CardService(TrelloCloneContext context){
           _context = context;
      }

      public async Task<IEnumerable<CardDtoOut>> GetAll()
      {
          return await _context.Cards.Select(
           c => new CardDtoOut
           {
               Id = c.Id,
               Title = c.Title,
               Description = c.Description,
               Comment = c.Comment,
               Labels = c.Labels,
               Cover = c.Cover,
               ListId = c.ListId
           }).ToListAsync();
      }

          public async Task<Card?> GetById(int id)
          {
               return await _context.Cards.FindAsync(id);
          }

          public async Task<CardDtoOut?>GetDtoById(int id)
          {
               return await _context.Cards.Where(c => c.Id == id).Select(
                    c => new CardDtoOut
                    {
                         Id = c.Id,
                         Title = c.Title,
                         Description = c.Description,
                         Comment = c.Comment,
                         Labels = c.Labels,
                         Cover = c.Cover,
                         ListId = c.ListId
                    }).SingleOrDefaultAsync();
          }

          public async Task<IEnumerable<CardDtoOut>> GetByListId(int listId)
          {
               return await _context.Cards.Where(c => c.ListId == listId).Select(
                    c => new CardDtoOut
                    {
                         Id = c.Id,
                         Title = c.Title,
                         Description = c.Description,
                         Comment = c.Comment,
                         Labels = c.Labels,
                         Cover = c.Cover,
                         ListId = c.ListId
                    }).ToListAsync();
          }

          public async Task<Card> Create(CardDtoIn newCardDTO)
          {
               var newCard = new Card();

               newCard.Title = newCardDTO.Title;
               newCard.Description = newCardDTO.Description;
               newCard.Comment = newCardDTO.Comment;
               newCard.Labels = newCardDTO.Labels;
               newCard.Cover = newCardDTO.Cover;
               newCard.ListId = newCardDTO.ListId;

               _context.Cards.Add(newCard);
               await _context.SaveChangesAsync();

               return newCard;
          }

          public async Task Update(int id, CardDtoIn card)
          {
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