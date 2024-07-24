using Assurant.Pricing.Domain.RuleEngine;
using Assurant.Pricing.Domain.Contract.Interface;
using Microsoft.Extensions.Options;
using Assurant.Pricing.Infrastructure.Contract.Interface;
using Assurant.Pricing.Domain.Contract.DomainModel;

namespace Assurant.Pricing.Domain.Service
{
    public class RuleService : IRuleService
    {
        private IOptions<ApplicationOptions> _options;
        private IPriceRepository _priceRepository;
        private IHolidayRepository _holidayRepository;
        public RuleService(IOptions<ApplicationOptions> options, IPriceRepository priceRepository, IHolidayRepository holidayRepository)
        {
            this._options = options;
            this._priceRepository = priceRepository;
            this._holidayRepository = holidayRepository;
        }
        public IRuleEngine BuildRuleEngine()
        {
            IRuleEngine engine = new DefaultRuleEngine();
            foreach (Rule ruleEngine in _options.Value.Rules)
            {
                engine = RuleEngineFactory.CreateEngine(ruleEngine.RuleName, engine);
            }
            return engine;
        }

        public double CalculatePrice(IRuleEngine ruleEngine, List<ITicket> tickets)
        {
            double price = 0;
            foreach(ITicket ticket in tickets)
            {
                price = price + ruleEngine.CalculatePrice(ticket, _priceRepository, _holidayRepository);
            }
            return price;
        }

        public double CalculatePrice(IRuleEngine ruleEngine, ITicket ticket)
        {
            return ruleEngine.CalculatePrice(ticket, _priceRepository, _holidayRepository);
        }
    }
}
