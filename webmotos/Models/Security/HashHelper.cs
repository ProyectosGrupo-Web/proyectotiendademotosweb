using System;
using System.Security.Cryptography;
using System.Text;

namespace webmotos.Models.Security;

public static class HashHelper
{
    public static string ComputeMD5(string word)
    {
        using (var md5 = MD5.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(word);
            var hash = md5.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLower();
        }
    }


    public static string Token()
    {
        long i = 1;
        foreach (byte b in Guid.NewGuid().ToByteArray())
            i *= ((int)b + 1);

        return ComputeMD5($"{i - DateTime.Now.Ticks:x}");
    }

    public static string Base64Encode(string word)
    {
        var bytes = Encoding.UTF8.GetBytes(word);
        return Convert.ToBase64String(bytes);
    }

    public static string Base64Decode(string word)
    {
        var bytes = Convert.FromBase64String(word);
        return Encoding.UTF8.GetString(bytes);
    }

    public static string ComputeSHA1(string str)
    {
        using (var sha1 = SHA1.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var hash = sha1.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLower(); // Devuelve en minúsculas
        }
    }

    public static string ComputeSHA256(string str)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLower();
        }
    }

    public static string ComputeSHA384(string str)
    {
        using (var sha384 = SHA384.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var hash = sha384.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLower();
        }
    }

    public static string ComputeSHA512(string str)
    {
        using (var sha512 = SHA512.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var hash = sha512.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLower();
        }
    }
}
