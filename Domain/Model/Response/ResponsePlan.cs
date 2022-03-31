using Domain.Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class ResponsePlan
    {
        public string Title { get; set; }

        public List<Plan> Plans {get; set;}
        public int Status { get; set; }
        public bool IsReturned { get; set; }

    }
}
