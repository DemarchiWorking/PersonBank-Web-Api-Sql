using System;

namespace Domain.Model.Request
{
    public class FilterDateRequest
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        
    }  
}
