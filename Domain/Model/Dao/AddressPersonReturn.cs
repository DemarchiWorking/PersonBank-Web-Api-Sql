using System;

namespace Domain.Model.Dao
{
    public class AddressPersonReturn
    {
        public int Id_Person { get; set; }
        public string Street_Address { get; set; }
        public string Suburb { get; set; }
        public string Zip_Code { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Additional_Information { get; set; }

    }
}
