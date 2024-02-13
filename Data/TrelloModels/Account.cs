using System;
using System.Collections.Generic;

namespace TrelloClone.Data.TrelloModels;

public partial class Account
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Photo { get; set; }

    public string? Email { get; set; }

    public int? BoardId { get; set; }

    public virtual Board? Board { get; set; }

    public virtual ICollection<Board> Boards { get; set; } = new List<Board>();
}
