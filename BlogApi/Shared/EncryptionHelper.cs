using System;
using System.Security.Cryptography;
using System.Text;

public static class EncryptionHelper
{
    public static string EncryptId(int id)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(id.ToString()));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }
}
