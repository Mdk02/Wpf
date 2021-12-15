using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Models
{
    public class DataMethodsProposal
    {
        public DataMethodsProposal()
        {
            if (!GetAllDataProposals().Any())
            {
                AddProposal(new Proposal { Id = 0, Price = 11000000, UserId = 0, City = "Gorod", CountRooms = 3, District = "dis", Number = "31", NumberOfHouse = "31/2", PrimaryOrSecondaryHousing = true, Square = 54, Street = "Kolotushkino", Description = "Особая группа элементов управления образована от класса HeaderedContentControl, который является подклассом ContentControl. Эта группа отличается тем, что позволяет задать заголовок содержимому. В эту группу элементов входят GroupBox и Expander.", Photos = @"C:\Users\1\Desktop\2\WpfApp1\Photos\0" });
                AddProposal(new Proposal { Id = 1, Price = 11000000, UserId = 3, City = "Gorod", CountRooms = 2, District = "dis", Number = "33", NumberOfHouse = "31/2", PrimaryOrSecondaryHousing = true, Square = 45, Street = "Kolotushkino", Description = "Элемент GroupBox организует наборы элементов управления в отдельные группы. При этом мы можем определить у группы заголовок:", Photos = @"C:\Users\1\Desktop\2\WpfApp1\Photos\1" });
            }
        }

        string path = @"C:\Users\1\Desktop\2\WpfApp1\Data\Proposals.txt";
        public void AddProposal(Proposal proposal)
        {
            if (proposal != null)
            {
                using (StreamWriter sr = new StreamWriter(path, true))
                {
                    sr.WriteLine(JsonSerializer.Serialize<Proposal>(proposal));
                }
            }
        }

        public void DeleteProposal(Proposal proposal) 
        {
            var new_data = GetAllDataProposals().Where(i => i.Id != proposal.Id);
            List<string> string_array = new List<string>();

            new_data.ToList().ForEach(propos => string_array.Add(JsonSerializer.Serialize<Proposal>(propos)));
            File.WriteAllLines(path, string_array);
        }

        public IEnumerable<Proposal> GetAllDataProposals()
        {
            var list = new List<Proposal>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(JsonSerializer.Deserialize<Proposal>(line));
                }
            }

            return list;
        }
    }
}
