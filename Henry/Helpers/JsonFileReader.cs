﻿using System.Text.Json;

namespace Henry.Helpers
{
    public class JsonFileReader<T>
    {
        public static List<T> ReadJson(string jsonFileName)
        {
            using (var jsonFileReader = File.OpenText(jsonFileName))
            {
                string inddata = jsonFileReader.ReadToEnd();
                return JsonSerializer.Deserialize<List<T>>(inddata);
            }
        }
    }
}
