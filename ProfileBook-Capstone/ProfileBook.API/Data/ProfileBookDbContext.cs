using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProfileBook.API.Models;

namespace ProfileBook.API.Data;

public partial class ProfileBookDbContext : DbContext
{
    public ProfileBookDbContext()
    {
    }

    public ProfileBookDbContext(DbContextOptions<ProfileBookDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupMember> GroupMembers { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostComment> PostComments { get; set; }

    public virtual DbSet<PostLike> PostLikes { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ProfileBookDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

       // Configure the Password column mapping
        modelBuilder.Entity<User>()
        .Property(u => u.PasswordHash)
        .HasColumnName("Password");
        // Message relationships
        modelBuilder.Entity<Message>()
        .HasOne(m => m.Sender)
        .WithMany(u => u.SentMessages)
        .HasForeignKey(m => m.SenderId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Message>()
        .HasOne(m => m.Receiver)
        .WithMany(u => u.ReceivedMessages)
        .HasForeignKey(m => m.ReceiverId)
        .OnDelete(DeleteBehavior.Restrict);

       // Report relationships
    modelBuilder.Entity<Report>()
        .HasOne(r => r.ReportedUser)
        .WithMany(u => u.ReportsReceived)
        .HasForeignKey(r => r.ReportedUserId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Report>()
        .HasOne(r => r.ReportingUser)
        .WithMany(u => u.ReportsGiven)
        .HasForeignKey(r => r.ReportingUserId)
        .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF36ADD844A87");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.GroupName).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Groups__CreatedB__5165187F");
        });

        modelBuilder.Entity<GroupMember>(entity =>
        {
            entity.HasKey(e => e.GroupMemberId).HasName("PK__GroupMem__344812920D430DE0");

            entity.HasIndex(e => new { e.GroupId, e.UserId }, "UQ__GroupMem__C5E27FAFBFCF43C9").IsUnique();

            entity.Property(e => e.JoinedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupMemb__Group__571DF1D5");

            entity.HasOne(d => d.User).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupMemb__UserI__5812160E");
        });

       /* modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C0C9C734F5586");

            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.TimeStamp).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Receiv__46E78A0C");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Sender__45F365D3");
        }); */

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__AA126018E85BCEED");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PostImage).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Posts__UserId__412EB0B6");
        });

        modelBuilder.Entity<PostComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__PostComm__C3B4DFCAD5EF877F");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Post).WithMany(p => p.PostComments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostComme__PostI__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.PostComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostComme__UserI__628FA481");
        });

        modelBuilder.Entity<PostLike>(entity =>
        {
            entity.HasKey(e => e.LikeId).HasName("PK__PostLike__A2922C14C3F04D95");

            entity.HasIndex(e => new { e.PostId, e.UserId }, "UQ__PostLike__7B6AECDD5034081E").IsUnique();

            entity.Property(e => e.LikedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Post).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostLikes__PostI__5CD6CB2B");

            entity.HasOne(d => d.User).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostLikes__UserI__5DCAEF64");
        });

        /*modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD48054151482D");

            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TimeStamp).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ReportedUser).WithMany(p => p.ReportedUsers)
                .HasForeignKey(d => d.ReportedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reports__Reporte__4CA06362");

            entity.HasOne(d => d.ReportingUser).WithMany(p => p.ReportingUsers)
                .HasForeignKey(d => d.ReportingUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reports__Reporti__4D94879B");
        });*/

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CDAE24840");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4E21ADF48").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105346710535E").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.ProfileImage).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("User");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    
}
