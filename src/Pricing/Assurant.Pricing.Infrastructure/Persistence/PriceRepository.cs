using Assurant.Pricing.Infrastructure.Contract.DataModel;
using Assurant.Pricing.Infrastructure.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assurant.Pricing.Infrastructure.Persistence
{
    public class PriceRepository : IPriceRepository
    {
        public readonly PricingContext _db;
  

        public PriceRepository(PricingContext db)
        {
            _db = db;
  
        }
        public decimal GetPrice(string ticketType)
        {
            try
            {
                Ticket ticket = _db.Ticket.FirstOrDefault(x => x.TicketType == ticketType);
                List<DateTime> result = new List<DateTime>();

                return ticket.Price;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
