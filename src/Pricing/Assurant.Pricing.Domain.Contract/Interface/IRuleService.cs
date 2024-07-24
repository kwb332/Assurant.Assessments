using System;
using System.Collections.Generic;
using System.Text;

namespace Assurant.Pricing.Domain.Contract.Interface
{
    public interface IRuleService
    {
        public IRuleEngine BuildRuleEngine();
        public double CalculatePrice(IRuleEngine ruleEngine, List<ITicket> tickets);
        public double CalculatePrice(IRuleEngine ruleEngine, ITicket ticket);
    }
}
