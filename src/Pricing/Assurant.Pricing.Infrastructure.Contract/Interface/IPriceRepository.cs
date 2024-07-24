using System;
using System.Collections.Generic;
using System.Text;

namespace Assurant.Pricing.Infrastructure.Contract.Interface
{
    public interface IPriceRepository
    {
        public double GetPrice(string ticketType);
    }
}
