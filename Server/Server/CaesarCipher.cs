using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server1
{
    public static class CaesarCipher
    {
        public static string Encrypt(string text, int shift)
        {
            return new string(text.Select(c => (char)(c + shift)).ToArray());
        }

        public static string Decrypt(string text, int shift)
        {
            return new string(text.Select(c => (char)(c - shift)).ToArray());
        }
    }
}
