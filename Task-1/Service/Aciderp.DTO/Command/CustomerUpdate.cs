using Aciderp.CQRS.Command;

namespace Aciderp.DTO.Command
{
    public class CustomerUpdate : ICommand
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Phone { get; set; }
		public string City { get; set; }
		public string StreetName { get; set; }
		public string BuildingNumber { get; set; }
		public string FlatNumber { get; set; }
		public string Postal { get; set; }
    }
}
