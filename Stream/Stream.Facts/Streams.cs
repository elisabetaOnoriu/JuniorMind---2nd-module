using System.IO.Compression;
using System.Security.Cryptography;

namespace Streams
{
    public class Streams
    {
        public Aes aes = Aes.Create();
        
        public void Write(Stream stream, string text, bool crypt = false, bool gzip = false)
        { 
            if (gzip)
            {
                stream = new GZipStream(stream, CompressionMode.Compress);
            }

            if (crypt)
            {
                stream = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            }

            StreamWriter streamWriter = new(stream);
            streamWriter.Write(text);
            streamWriter.Flush();

            if (stream is CryptoStream cryptoStream)
            {
                cryptoStream.FlushFinalBlock();
            }
        }

        public string Read(Stream stream, bool crypt = false, bool gzip = false)
        { 
            if (gzip)
            {
                stream = new GZipStream(stream, CompressionMode.Decompress);
            }

            if (crypt)
            {
                stream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            }

            StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }

        [Fact]
        public void WritesTextIntoStream()
        {
            MemoryStream stream = new();
            Write(stream, "1234");

            stream.Seek(0, SeekOrigin.Begin);
            Assert.Equal("1234", Read(stream));
        }

        [Fact]
        public void WritesTextIntoStream_CryptIsTrue()
        {
            MemoryStream stream = new();
            Write(stream, "1234", true);
            
            stream.Seek(0, SeekOrigin.Begin);
            Assert.Equal("1234", Read(stream, true));
        }

        [Fact]
        public void WritesTextIntoStream_GzipIsTrue()
        {
            MemoryStream stream = new();
            Write(stream, "1234", false, true);

            stream.Seek(0, SeekOrigin.Begin);
            Assert.Equal("1234", Read(stream, false, true));
        }

        [Fact]
        public void WritesTextIntoStream_CryptAndGzipAreTrue()
        {
            MemoryStream stream = new();
            Write(stream, "1234", true, true);

            stream.Seek(0, SeekOrigin.Begin);
            Assert.Equal("1234", Read(stream, true, true));
        }
    }
}