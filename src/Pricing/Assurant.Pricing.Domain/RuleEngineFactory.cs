using Assurant.Pricing.Domain.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assurant.Pricing.Domain
{
    public static class RuleEngineFactory
    {
        public static IRuleEngine CreateEngine(string engineName, IRuleEngine ruleEngine)
        {
            IRuleEngine engineInstance = (IRuleEngine)Activator.CreateInstance("Assurant.Pricing.Domain", "Assurant.Pricing.Domain.RuleEngine." + engineName).Unwrap();
            engineInstance.SetComponent(ruleEngine);
            return engineInstance;
        }
    }
}
