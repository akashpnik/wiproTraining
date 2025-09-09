using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProfileBook.API.Models;

namespace ProfileBook.API.Models;

public partial class User
{
    public int UserId { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    [Column("Password")]  // Maps PasswordHash property to Password column
    public string PasswordHash{ get; set; } = null!;
    public string? Role { get; set; }

    public string? ProfileImage { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    //public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    //public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();

    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    //public virtual ICollection<Report> ReportedUsers { get; set; } = new List<Report>();

    //public virtual ICollection<Report> ReportingUsers { get; set; } = new List<Report>();

    public User()
    {
        // Initialize collections to avoid null reference errors
        Posts = new HashSet<Post>();
        SentMessages = new HashSet<Message>();
        ReceivedMessages = new HashSet<Message>();
        ReportsReceived = new HashSet<Report>();
        ReportsGiven = new HashSet<Report>();
        GroupMembers = new HashSet<GroupMember>();
        Groups = new HashSet<Group>();
        PostComments = new HashSet<PostComment>();
        PostLikes = new HashSet<PostLike>();
    }

    public virtual ICollection<Message> SentMessages { get; set; }        // ← Add this
    public virtual ICollection<Message> ReceivedMessages { get; set; }    // ← Add this  
    public virtual ICollection<Report> ReportsReceived { get; set; }      // ← Add this
    public virtual ICollection<Report> ReportsGiven { get; set; }         // ← Add this for reports user made
}

