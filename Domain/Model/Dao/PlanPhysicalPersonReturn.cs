using System;

namespace Domain.Model.Dao
{
    public class PlanPhysicalPersonReturn
    {
        public int IdPerson { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Cpf { get; set; }
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AdditionalInformation { get; set; }
        public double? MonthlyIncome { get; set; }
        public string Plan { get; set; }
        public int? IdPlan { get; set; }
        public DateTime? LastPlanChange { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
