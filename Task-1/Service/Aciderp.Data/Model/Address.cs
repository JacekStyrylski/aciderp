using Aciderp.Data.Model.Abstract;

namespace Aciderp.Data.Model
{
    public class Address : Entity
    {
		public string City { get; set; }
		public string StreetName { get; set; }
		public string BuildingNumber { get; set; }
		public string FlatNumber { get; set; }
		public string Postal { get; set; }
    }
}
