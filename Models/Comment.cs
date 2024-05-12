using System;
using System.Collections.Generic;

namespace TrendyModa.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string? CommentText { get; set; }

    public int UserId { get; set; }

    public int PhotoId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Photo Photo { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
