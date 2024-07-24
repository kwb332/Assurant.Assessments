using System;
using System.Collections.Generic;
using System.Text;

namespace Assurant.Pricing.Domain.Contract.Interface
{
    public interface IRuleService
    {
        public IRuleEngine BuildRuleEngine();
        public decimal CalculatePrice(IRuleEngine ruleEngine, List<ITicket> tickets);
        public decimal CalculatePrice(IRuleEngine ruleEngine, ITicket ticket);
    }
}
