using Assurant.Pricing.Domain.Contract.DomainModel;
using Assurant.Pricing.Domain.DomainModel;

namespace Assurant.Pricing.Domain
{
    public class ApplicationOptions
    {
        public ApplicationOptions()
        {

        }
        public Rule[] Rules { get; set; }
    }
}
