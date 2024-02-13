using System;
using System.Collections.Generic;

namespace TrelloClone.Data.TrelloModels;

public partial class Card
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Comment { get; set; }

    public string? Labels { get; set; }

    public string? Cover { get; set; }

    public DateTime Date { get; set; }

    public int ListId { get; set; }

    public virtual List List { get; set; } = null!;
}
