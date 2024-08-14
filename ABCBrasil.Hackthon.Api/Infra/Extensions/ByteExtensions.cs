using System.IO;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    public static class ByteExtensions
    {
        public static byte[] ReadAsBytes(this Stream input)
        {
            var buffer = new byte[16 * 1024];

            using var ms = new MemoryStream();

            int read;

            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                ms.Write(buffer, 0, read);

            return ms.ToArray();
        }
    }
}