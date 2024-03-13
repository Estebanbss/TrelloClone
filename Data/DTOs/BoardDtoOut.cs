

namespace TrelloClone.Data.DTOs
{
    public class BoardDtoOut
    {
          public int Id { get; set; }
          public string Name { get; set; } = null!;

          public int AccountId { get; set; }
          
          public string? Ph { get; set; }
        
    }
}