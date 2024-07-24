
using Assurant.Pricing.Domain.Contract.Interface;
using Assurant.Pricing.Infrastructure.Contract.Interface;


namespace Assurant.Pricing.Domain.RuleEngine
{
    public class DefaultRuleEngine : RuleEngine
    {

        
      
        public DefaultRuleEngine()
        {
           

        }
        public override double CalculatePrice(ITicket ticket, IPriceRepository priceRepository, IHolidayRepository holidayRepository)
        { 
            double price = priceRepository.GetPrice(ticket.Type);
            return price;
        }

        public override void SetComponent(IRuleEngine ruleEngine)
        {
           
        }
    }
}
