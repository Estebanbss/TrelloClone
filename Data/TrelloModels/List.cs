using System;
using System.Collections.Generic;

namespace TrelloClone.Data.TrelloModels;

public partial class List
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int BoardId { get; set; }

    public int? Pos { get; set; }

    public virtual Board Board { get; set; } = null!;

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
