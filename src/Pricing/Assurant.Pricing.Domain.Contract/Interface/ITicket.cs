using System;
using System.Collections.Generic;
using System.Text;

namespace Assurant.Pricing.Domain.Contract.Interface
{
    public  interface ITicket
    {
        public int Age { get; set; }
        public string Type { get; set; }
        public DateTime? Date { get; set; }
        public bool IsStudent { get; set; }
    }
}
