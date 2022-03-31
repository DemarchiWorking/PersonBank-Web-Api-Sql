using Domain.Model.Dao;
using System.Collections.Generic;

namespace Domain.Model.Response
{
    public class ResponseLocalizationFilter
    {
        public string Title { get; set; }
        public List<AddressByState> AddressByState { get; set; }
        public int Status { get; set; }
        public bool IsReturned { get; set; }
    }
}
