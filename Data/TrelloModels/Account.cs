using System;
using System.Collections.Generic;

namespace TrelloClone.Data.TrelloModels;

public partial class Account
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Photo { get; set; }

    public string Email { get; set; } = null!;

    public int? BoardId { get; set; }

    public string Pwd { get; set; } = null!;

    public string Atype { get; set; } = null!;

    public DateTime? RegDate { get; set; }

    public virtual Board? Board { get; set; }

    public virtual ICollection<Board> Boards { get; set; } = new List<Board>();
}
