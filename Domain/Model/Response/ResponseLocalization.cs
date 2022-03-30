using Domain.Model.Dao;
using System.Collections.Generic;

namespace Domain.Model.Response
{
    public class ResponseLocalization
    {
        public string Title { get; set; }
        public List<State> States { get; set; }
        public int Status { get; set; }
        public bool IsReturned { get; set; }
    }
}
