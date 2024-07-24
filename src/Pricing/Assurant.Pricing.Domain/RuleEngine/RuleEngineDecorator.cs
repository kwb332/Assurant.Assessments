using Assurant.Pricing.Domain.Contract.Interface;
using Assurant.Pricing.Infrastructure.Contract.Interface;
using System.ComponentModel;
using System.Text;

namespace Assurant.Pricing.Domain.RuleEngine
{
    public abstract class RuleEngineDecorator : RuleEngine
    {
        protected IRuleEngine _ruleEngine;

        public RuleEngineDecorator(IRuleEngine ruleEngine)
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

       
    }
}
