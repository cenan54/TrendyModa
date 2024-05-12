using System;
using System.Collections.Generic;

namespace TrendyModa.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Name { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Follow> FollowFallowees { get; set; } = new List<Follow>();

    public virtual ICollection<Follow> FollowFallowers { get; set; } = new List<Follow>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
