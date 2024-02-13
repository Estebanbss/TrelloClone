using System;
using System.Collections.Generic;

namespace TrelloClone.Data.TrelloModels;

public partial class Board
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<List> Lists { get; set; } = new List<List>();
}
