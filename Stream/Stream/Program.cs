using System.IO.Compression;
using System.Security.Cryptography;

namespace Streams
{
    public class Program
    {

        static void Main(string[] args)
        {
        }

        public static void Write(Stream stream, string text, bool crypt = false, bool gzip = false)
        {
            if (crypt)
            {
                Aes aes = Aes.Create();
                stream = new CryptoStream(stream, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write);
            }

            if (gzip)
            {
                stream = new GZipStream(stream, CompressionMode.Compress);
            }

            StreamWriter streamWriter = new(stream);
            streamWriter.Write(text);
            streamWriter.Flush();
        }

        public static string Read(Stream stream, bool crypt = false, bool gzip = false)
        {
            if (crypt)
            {
                stream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            }

            if (gzip)
            {
                stream = new GZipStream(stream, CompressionMode.Decompress);
            }

            StreamReader reader = new (stream);
            return reader.ReadToEnd();
        }
    }
}
