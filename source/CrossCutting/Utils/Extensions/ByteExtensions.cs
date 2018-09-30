using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace Solution.CrossCutting.Utils
{
    public static class ByteExtensions
    {
        public static byte[] Compress(this byte[] bytes)
        {
            using (var ms = new MemoryStream())
            {
                using (var gzip = new GZipStream(ms, CompressionMode.Compress))
                {
                    gzip.Write(bytes, 0, bytes.Length);
                }

                return ms.ToArray();
            }
        }

        public static byte[] Decompress(this byte[] bytes)
        {
            using (var ms = new MemoryStream())
            {
                using (var input = new MemoryStream(bytes))
                using (var gzip = new GZipStream(input, CompressionMode.Decompress))
                {
                    gzip.CopyTo(ms);
                }

                return ms.ToArray();
            }
        }

        public static T ToObject<T>(this byte[] bytes)
        {
            using (var ms = new MemoryStream())
            {
                var decompressed = bytes.Decompress();
                ms.Write(decompressed, 0, decompressed.Length);
                ms.Seek(0, SeekOrigin.Begin);

                return (T)new BinaryFormatter().Deserialize(ms);
            }
        }
    }
}
