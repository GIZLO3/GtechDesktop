using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GtechDesktop.WPF.Services
{
    public class JsonService
    {
        public static void WriteFile(object obj, string filename)
        {
            var jsonString = JsonSerializer.Serialize(obj);
            File.WriteAllText(filename, jsonString);
        }
    }
}
