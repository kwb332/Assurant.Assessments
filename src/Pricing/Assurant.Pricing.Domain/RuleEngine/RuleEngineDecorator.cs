using Assurant.Pricing.Domain.Contract.Interface;
using Assurant.Pricing.Infrastructure.Contract.Interface;
using System.ComponentModel;
using System.Text;

namespace Assurant.Pricing.Domain.RuleEngine
{
    public abstract class RuleEngineDecorator : RuleEngine
    {
        protected RuleEngine _ruleEngine;

        public RuleEngineDecorator(RuleEngine ruleEngine)
        {
            this._ruleEngine = ruleEngine;
        }
        public RuleEngineDecorator()
        {
          
        }
        public void SetComponent(RuleEngine ruleEngine)
        {
            this._ruleEngine = ruleEngine;
        }

        public override double CalculatePrice(ITicket ticket, IPriceRepository priceRepository, IHolidayRepository holidayRepository)
        {
            if(this._ruleEngine != null)
            {
                return this._ruleEngine.CalculatePrice(ticket, priceRepository, holidayRepository);
            }
            else
            {
                return 0;
            }
        }
    }
}
