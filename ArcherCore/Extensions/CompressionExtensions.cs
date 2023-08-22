using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Extensions
{
    public static class CompressionExtensions
    {
        public static async Task<CompressionResult> ToGzipAsync(this string value, CompressionLevel level = CompressionLevel.Fastest)
        {
            var bytes = Encoding.Unicode.GetBytes(value);
            await using var input = new MemoryStream(bytes);
            await using var output = new MemoryStream();
            await using var stream = new GZipStream(output, level);

            await input.CopyToAsync(stream);

            var result = output.ToArray();

            return new CompressionResult(
                new CompressionValue(value, bytes.Length),
                new CompressionValue(Convert.ToBase64String(result), result.Length),
                level,
                "Gzip");
        }

        public static async Task<CompressionResult> ToBrotliAsync(this string value, CompressionLevel level = CompressionLevel.Fastest)
        {
            var bytes = Encoding.Unicode.GetBytes(value);
            await using var input = new MemoryStream(bytes);
            await using var output = new MemoryStream();
            await using var stream = new BrotliStream(output, level);

            await input.CopyToAsync(stream);
            await stream.FlushAsync();

            var result = output.ToArray();

            return new CompressionResult(
                new CompressionValue(value, bytes.Length),
                new CompressionValue(Convert.ToBase64String(result), result.Length),
                level,
                "Brotli"
            );
        }

        public static async Task<string> FromGzipAsync(this string value)
        {
            var bytes = Convert.FromBase64String(value);
            await using var input = new MemoryStream(bytes);
            await using var output = new MemoryStream();
            await using var stream = new GZipStream(input, CompressionMode.Decompress);

            await stream.CopyToAsync(output);
            await stream.FlushAsync();

            return Encoding.Unicode.GetString(output.ToArray());
        }

        public static async Task<string> FromBrotliAsync(this string value)
        {
            var bytes = Convert.FromBase64String(value);
            await using var input = new MemoryStream(bytes);
            await using var output = new MemoryStream();
            await using var stream = new BrotliStream(input, CompressionMode.Decompress);

            await stream.CopyToAsync(output);

            return Encoding.Unicode.GetString(output.ToArray());
        }

        public static string ToCompress(this string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string ToDecompress(this string s)
        {
            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }
        public static byte[] ToCompress(this byte[] bytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(bytes, 0, bytes.Length);
                }
                return memoryStream.ToArray();
            }
        }
        public static byte[] ToDecompress(this byte[] s)
        {
            var bytes = s;
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return mso.ToArray();
            }
        }
    }
    public record CompressionResult(
    CompressionValue Original,
    CompressionValue Result,
    CompressionLevel Level,
    string Kind
)
    {
        public int Difference =>
            Original.Size - Result.Size;

        public decimal Percent =>
          Math.Abs(Difference / (decimal)Original.Size);
    }

    public record CompressionValue(
        string Value,
        int Size
    );
}
