using Microsoft.EntityFrameworkCore;
using SecureMessages.Models.Db;

namespace SecureMessages.Models;

public class MessagesDbContext: DbContext
{
    public MessagesDbContext(DbContextOptions options) : base(options) { }
    public virtual DbSet<Message> Messages { get; set; } = null!;
}
