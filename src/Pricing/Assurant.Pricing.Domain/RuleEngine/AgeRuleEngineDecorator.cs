
using Assurant.Pricing.Domain.Contract.Interface;
using Assurant.Pricing.Infrastructure.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Text;


namespace Assurant.Pricing.Domain.RuleEngine
{
    public class AgeRuleEngineDecorator : RuleEngineDecorator
    {
        public IRuleEngine _ruleEngine;
        public AgeRuleEngineDecorator(IRuleEngine ruleEngine) : base(ruleEngine) 
        { 
            _ruleEngine = ruleEngine;
        }
        public AgeRuleEngineDecorator()
        {
            
        }
        /*
         * - Price is calculated based on the age of the skier and the type of pass.
- Child (0-6 years): Free
- Child (7-14 years): 70% off
- Student (15-23 years): 50% off
- Adult (15-63 years): Full price
- Senior (64+ years): 75% off
- If the age is not specified, the default price is the adult price.

         */
        public override decimal CalculatePrice(ITicket ticket, IPriceRepository priceRepository, IHolidayRepository holidayRepository)
        {
            decimal basePrice = _ruleEngine.CalculatePrice(ticket, priceRepository, holidayRepository);
           if(ticket.Age >= 0 && ticket.Age <= 6)
            {
                return 0;
            }
            else if (ticket.Age >= 7 && ticket.Age <= 14)
            {
                return basePrice - (basePrice * Convert.ToDecimal(0.70));
            }
            else if (ticket.Age >= 15 && ticket.Age <= 23)
            {
                return basePrice - (basePrice * Convert.ToDecimal(.50));
            }
            else if (ticket.Age > 23 && ticket.Age <= 63)
            {
                return basePrice * 1;
            }
            else if (ticket.Age >= 64)
            {
                return basePrice - (basePrice * Convert.ToDecimal((0.750)));
            }
            else
            {
                return basePrice;
            }

            
        }

        public override void SetComponent(IRuleEngine ruleEngine)
        {
            this._ruleEngine = ruleEngine;
        }
    }
}
