using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileBook.API.Models;

public partial class Message
{
    public int MessageId { get; set; }
    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string MessageContent { get; set; } = null!;

    public DateTime? TimeStamp { get; set; }

    public bool? IsRead { get; set; }

    // Navigation properties with explicit mapping
    [ForeignKey("SenderId")]
    [InverseProperty("SentMessages")]
    public virtual User Sender { get; set; } = null!;

    [ForeignKey("ReceiverId")]
    [InverseProperty("ReceivedMessages")]
    public virtual User Receiver { get; set; } = null!;
}