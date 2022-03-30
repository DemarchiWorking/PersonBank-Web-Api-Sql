using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Dao
{
    public class Address
    {
        public string Street_Address { get; set; }
        public string Suburb { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
