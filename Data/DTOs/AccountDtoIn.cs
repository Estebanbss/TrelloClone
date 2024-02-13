namespace TrelloClone.Data.DTOs
{
    public class AccountDtoIn
    {
       public int Id { get; set; }
       public string Username { get; set; } = null!;
       public string? Photo { get; set; }
       public string? Email { get; set; }
    }
}