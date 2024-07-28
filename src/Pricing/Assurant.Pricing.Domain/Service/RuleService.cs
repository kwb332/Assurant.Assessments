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


            RuleEngine.RuleEngine defaultRuleEngine = new DefaultRuleEngine();
            foreach (Rule ruleEngine in _options.Value.Rules.OrderBy(x=>x.Precedence))
            {
                defaultRuleEngine = (RuleEngine.RuleEngine) RuleEngineFactory.CreateEngine(ruleEngine.RuleName, defaultRuleEngine);
                if (!defaultRuleEngine.EndCalulations)
                    continue;
                break;

            }
            return defaultRuleEngine;
        }

        public decimal CalculatePrice(IRuleEngine ruleEngine, List<ITicket> tickets)
        {
            decimal price = 0;
            foreach(ITicket ticket in tickets)
            {
                price = price + ruleEngine.CalculatePrice(ticket, _priceRepository, _holidayRepository);
            }
            return price;
        }

        public decimal CalculatePrice(IRuleEngine ruleEngine, ITicket ticket)
        {
            return ruleEngine.CalculatePrice(ticket, _priceRepository, _holidayRepository);
        }

        public decimal CalculatePrice(List<ITicket> tickets)
        {
            IRuleEngine ruleEngine = BuildRuleEngine();
            decimal results = 0;

            foreach (Ticket ticket in tickets)
            {
                results = +CalculatePrice(ruleEngine, ticket);

            }
            results = results < 0 ? 0 : results;
            return results;
        }

        public decimal CalculatePrice(ITicket ticket)
        {
            IRuleEngine ruleEngine = BuildRuleEngine();
   

            decimal results = CalculatePrice(ruleEngine, ticket);
            results = results < 0 ? 0 : results;

            return results;
        }
    }
}
