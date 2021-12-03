using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class DataMethodResponse
    {
        string path = @"C:\Users\1\source\repos\WpfApp1\WpfApp1\Data\Responses.txt";
        public void AddResponse(Response response)
        {
            if (response != null)
            {
                using (StreamWriter sr = new StreamWriter(path, true))
                {
                    sr.WriteLine(JsonSerializer.Serialize(response));
                }
            }
        }

        public IEnumerable<Response> GetAllDataResponses()
        {
            var list = new List<Response>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(JsonSerializer.Deserialize<Response>(line));
                }
            }

            return list;
        }
    }
}
