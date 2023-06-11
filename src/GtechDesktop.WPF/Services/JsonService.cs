using System.IO;
using System.Text.Json;

namespace GtechDesktop.WPF.Services
{
    public class JsonService
    {
        public static void WriteFile(object obj, string filename)//metoda do zapisu danych do pliku json
        {
            var jsonString = JsonSerializer.Serialize(obj);
            File.WriteAllText(filename, jsonString);
        }
    }
}
