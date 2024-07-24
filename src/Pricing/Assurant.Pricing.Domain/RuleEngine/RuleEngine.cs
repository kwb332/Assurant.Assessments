
using Assurant.Pricing.Domain.Contract.Interface;
using Assurant.Pricing.Infrastructure.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assurant.Pricing.Domain.RuleEngine
{
    public abstract class RuleEngine : IRuleEngine
    {
        public abstract double CalculatePrice(ITicket ticket, IPriceRepository priceRepository, IHolidayRepository holidayRepository);

        public abstract void SetComponent(IRuleEngine ruleEngine);
      
    }
}
