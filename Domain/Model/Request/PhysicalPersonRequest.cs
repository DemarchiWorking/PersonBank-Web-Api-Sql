using System;

namespace Domain.Model.Request
{
    public class PhysicalPersonRequest
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cpf { get; set; }
        public AddressPerson Address { get; set; }
        public double MonthlyIncome { get; set; }
    }
    public class AddressPerson
    {
        public string Street_Address { get; set; }
        public string Suburb { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
