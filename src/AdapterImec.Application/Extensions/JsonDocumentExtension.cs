using System.IO;
using System.Text;
using System.Text.Json;

namespace AdapterImec.Application.Extensions
{
    public static class JsonDocumentExtension
    {
        public static string ToJsonString(this JsonDocument doc)
        {
            using var stream = new MemoryStream();
            var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = false });
            doc.WriteTo(writer);
            writer.Flush();
            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
