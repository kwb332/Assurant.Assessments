using Assurant.Pricing.Domain.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assurant.Pricing.Domain.Contract.DomainModel
{
    public class Ticket : ITicket
    {
        public int Age { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public bool IsStudent { get; set; }
    }
}
