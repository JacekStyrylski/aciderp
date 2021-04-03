using System;
using System.Collections.Generic;
using Aciderp.Data.Model.Abstract;

namespace Aciderp.Data.Model
{
    public class Trip : Entity
    {
        public string Source { get; set; }
		public string Destination { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public bool IsCanceled { get; set; }
		public virtual ICollection<Customer> Customers { get; set; }
    }
}
