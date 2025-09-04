using Chat_CSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_CSharp.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;User Id=sa;Password=YourStrong!Passw0rd;Database=ChatApp;TrustServerCertificate=True");
    }
}