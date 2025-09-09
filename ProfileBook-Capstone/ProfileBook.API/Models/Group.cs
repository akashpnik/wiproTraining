using System;
using System.Collections.Generic;

namespace ProfileBook.API.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
}
