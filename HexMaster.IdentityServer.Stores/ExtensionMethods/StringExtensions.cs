using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace HexMaster.IdentityServer.Stores.ExtensionMethods
{
    public static class StringExtensions
    {

        public static string HashPassword(this string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                password: value,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 512 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public static bool ValidatePassword(this string value, string salt, string hash)
            => HashPassword(value, salt) == hash;

    }
}
