using System.Security.Cryptography;
using System.Text;

namespace Graduation.Classes
{
    public static class Encryption
    {
        private static SHA256 _sha256;
        private static byte[] _hash;
        private static StringBuilder _stringBuilder;

        public static string Encrypt(string dataToEncrypt)
        {
            using (_sha256 = SHA256.Create())
            {
                _hash = _sha256.ComputeHash(Encoding.UTF8.GetBytes(dataToEncrypt));
                _stringBuilder = new StringBuilder();
                for (int i = 0; i < _hash.Length; i++)
                {
                    _stringBuilder.Append(_hash[i].ToString("x2"));
                }
                return _stringBuilder.ToString();
            }
        }
    }
}
