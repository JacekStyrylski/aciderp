using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Aciderp.Data.Model.Abstract;

namespace Aciderp.Data.Model
{
    public class Customer : Entity
    {
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Phone { get; set; }
		public int AddressId { get; set; }
		public virtual Address Address { get; set; }
		public ICollection<Trip> Trips { get; set; }
    }
}
