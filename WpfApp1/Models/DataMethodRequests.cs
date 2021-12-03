using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class DataMethodRequests
    {
        string path = @"C:\Users\1\source\repos\WpfApp1\WpfApp1\Data\Requests.txt";
        public void AddRequest(Request requests)
        {
            if (requests != null)
            {
                using (StreamWriter sr = new StreamWriter(path, true))
                {
                    sr.WriteLine(JsonSerializer.Serialize(requests));
                }
            }
        }

        public IEnumerable<Request> GetAllDataRequests()
        {
            var list = new List<Request>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(JsonSerializer.Deserialize<Request>(line));
                }
            }

            return list;
        }
    }
}
