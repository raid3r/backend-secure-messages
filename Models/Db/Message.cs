using Microsoft.EntityFrameworkCore;

namespace SecureMessages.Models.Db;

[Index(nameof(Code), IsUnique = true)]
public class Message
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string EncryptedContent { get; set; }
}
