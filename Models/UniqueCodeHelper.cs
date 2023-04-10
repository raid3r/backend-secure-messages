using System.Text;

namespace SecureMessages.Models;

public static class UniqueCodeHelper
{
    public static string GenerateRandomString(int length)
    {
        var sb = new StringBuilder();
        var numGuidsToConcat = (length - 1) / 32 + 1;
        for (var i = 1; i <= numGuidsToConcat; i++)
        {
            sb.Append(Guid.NewGuid().ToString("N"));
        }
        return sb.ToString(0, length);
    }
}
