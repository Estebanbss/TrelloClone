namespace TrelloClone.Data.DTOs
{
     public class ListDtoIn
     {
          public int Id { get; set; }

          public string Name { get; set; } = null!;

          public int BoardId { get; set; }

          public int? Pos { get; set; }

     }
}