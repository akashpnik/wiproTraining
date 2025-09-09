using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProfileBook.API.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int ReportedUserId { get; set; }
    public int ReportingUserId { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime? TimeStamp { get; set; }

    public string? Status { get; set; } = "Pending";
        [ForeignKey("ReportedUserId")]
        [InverseProperty("ReportsReceived")]
        public virtual User ReportedUser { get; set; } = null!;

        [ForeignKey("ReportingUserId")]
        [InverseProperty("ReportsGiven")]
        public virtual User ReportingUser { get; set; } = null!;
}
