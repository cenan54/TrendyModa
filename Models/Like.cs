using System;
using System.Collections.Generic;

namespace TrendyModa.Models;

public partial class Like
{
    public int LikeId { get; set; }

    public int UserId { get; set; }

    public int PhotoId { get; set; }

    public int? CreatedAt { get; set; }

    public virtual Photo Photo { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
