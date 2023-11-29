using System.Text.Json;

namespace Henry.Helpers
{
    public class JsonFileWriter<T>
    {
        public static void WriteToJson(List<T> events, string jsonFileName)
        {
            using (FileStream outputStream = File.Create(jsonFileName))
            {
                var writer = new Utf8JsonWriter(outputStream, new JsonWriterOptions()
                {
                    SkipValidation = false,
                    Indented = true
                }
                );
                JsonSerializer.Serialize(writer, events.ToArray());
            }
        }
    }
}