namespace TrelloClone.Data.DTOs
{
     public class CardDtoOut
     {
          public int Id { get; set; }

          public string Title { get; set; } = null!;

          public string? Description { get; set; }

          public string? Comment { get; set; }

          public string? Labels { get; set; }

          public string? Cover { get; set; }

          
           public int ListId { get; set; }
     }
}