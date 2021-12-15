
namespace WpfApp1.Models
{
    public class Proposal
    {
        public int Id { get; set; }
        public int Square { get; set; }
        public int CountRooms { get; set; }
        public string Number { get; set; }
        //true - primary, false - secondary
        public bool PrimaryOrSecondaryHousing { get; set; }
        public string NumberOfHouse { get; set; }
        public string Street { get; set; }
        public string District { get; set; }   
        public string City { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public string Photos { get; set; }
        //add photos
    }
}
