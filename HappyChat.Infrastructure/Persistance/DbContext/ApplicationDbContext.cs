using HappyChat.Core.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<UserChat> UserChats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Role>().ToTable("Role");
        modelBuilder.Entity<UserRole>().ToTable("UserRole");
        modelBuilder.Entity<RefreshToken>().ToTable("RefreshToken");
        modelBuilder.Entity<Chat>().ToTable("Chat");
        modelBuilder.Entity<Contact>().ToTable("Contact");
        modelBuilder.Entity<Message>().ToTable("Message");
        modelBuilder.Entity<UserChat>().ToTable("UserChat");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}