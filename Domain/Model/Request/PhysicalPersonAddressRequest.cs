namespace Domain.Model.Request
{
    public class PhysicalPersonAddressRequest
    {
       public int Id_Person { get; set; }
       
       public AddressPerson Address { get; set; }
    }
}
