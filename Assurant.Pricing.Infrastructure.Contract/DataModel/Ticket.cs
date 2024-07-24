using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core.DataModel
{
    public class Ticket
    {
        public int ID {  get; set; }

        public double Price { get; set; }

        public string TicketType { get; set; }
    }
}
