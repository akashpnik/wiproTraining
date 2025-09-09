// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class ApplicationDbContext : DbContext
{
    private readonly DataEncryptionService _encryptionService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, DataEncryptionService encryptionService)
        : base(options)
    {
        _encryptionService = encryptionService;
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var encryptedConverter = new ValueConverter<string, string>(
            v => _encryptionService.Encrypt(v),
            v => _encryptionService.Decrypt(v));

        modelBuilder.Entity<User>()
            .Property(u => u.CreditCardNumberEncrypted)
            .HasConversion(encryptedConverter);
    }
}