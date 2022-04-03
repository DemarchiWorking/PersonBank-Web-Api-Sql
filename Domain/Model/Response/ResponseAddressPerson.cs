using Domain.Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class ResponseAddressPerson
    {
        public string Title { get; set; }

        public dynamic AddressPerson {get; set;}
        public int Status { get; set; }
        public bool IsReturned { get; set; }

    }
}
