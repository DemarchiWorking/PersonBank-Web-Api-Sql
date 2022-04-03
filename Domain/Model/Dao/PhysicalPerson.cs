using System;

namespace Domain.Model.Dao
{
    public class PhysicalPerson
    {
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Cpf { get; set; }
        public Address Address { get; set; }
        public double MonthlyIncome { get; set; }

    }
}
