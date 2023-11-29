using System.Text.Json;

namespace Henry.Helpers
{
    public class JsonFileReader<T>
    {
        public static List<T> ReadJson(string jsonFileName)
        {
            using (var JsonFileReader = File.OpenText(jsonFileName))
            {
                string inData = JsonFileReader.ReadToEnd();
                return JsonSerializer.Deserialize<List<T>>(inData);
            }
        }
    }
}
