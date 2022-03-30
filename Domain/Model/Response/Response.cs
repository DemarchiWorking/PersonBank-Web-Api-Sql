using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class Response
    {
        public string Title { get; set; }
        public bool Registered { get; set; }
        public int Status { get; set; }
        public bool VipPlan { get; set; }
    }
}
