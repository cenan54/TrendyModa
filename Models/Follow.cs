using System;
using System.Collections.Generic;

namespace TrendyModa.Models;

public partial class Follow
{
    public int FollowId { get; set; }

    public int FallowerId { get; set; }

    public int FalloweeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Fallowee { get; set; } = null!;

    public virtual User Fallower { get; set; } = null!;
}
