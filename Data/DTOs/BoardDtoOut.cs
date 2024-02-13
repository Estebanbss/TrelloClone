

using TrelloClone.Data.TrelloModels;

namespace TrelloClone.Data.DTOs
{
    public class BoardDtoOut
    {
          public int Id { get; set; }
          public string Name { get; set; } = null!;
          
          public virtual ICollection<List> Lists { get; set; } = new List<List>();

          public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
        
    }
}