using System;
using System.Collections.Generic;

namespace TrendyModa.Models;

public partial class Photo
{
    public int PhotoId { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual User User { get; set; } = null!;

    public string FotoBase64 => Convert.ToBase64String(Image);
}
