
using Assurant.Pricing.Domain.Contract.Interface;
using Assurant.Pricing.Infrastructure.Contract.Interface;


namespace Assurant.Pricing.Domain.RuleEngine
{
    public class DefaultRuleEngine : RuleEngine
    {

        
      
        public DefaultRuleEngine()
        {
           

        }
        public override decimal CalculatePrice(ITicket ticket, IPriceRepository priceRepository, IHolidayRepository holidayRepository)
        { 
            decimal price = priceRepository.GetPrice(ticket.Type);
            return price;
        }

        public override void SetComponent(IRuleEngine ruleEngine)
        {
           
        }
    }
}
