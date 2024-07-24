using Assurant.Pricing.Domain.Contract.Interface;
using Microsoft.AspNetCore.Mvc;
using Assurant.Pricing.Domain.DomainModel;
using Assurant.Pricing.Domain.Contract.DomainModel;

namespace Assurant.Pricing.Presentation.Controllers
{ 
  [ApiController]
[Route("[controller]")]
public class PricingController : ControllerBase
{

    private readonly IRuleService _ruleService;
    private readonly ILogger<PricingController> _logger;

    public PricingController(ILogger<PricingController> logger, IRuleService ruleService)
    {
        _logger = logger;
        _ruleService = ruleService;
    }

    [HttpGet(Name = "GetPrice")]
    public double Get()
    {
        IRuleEngine ruleEngine = _ruleService.BuildRuleEngine();
        ITicket ticket = new Ticket();

        double results = _ruleService.CalculatePrice(ruleEngine, ticket);

        return results;
    }
}
}
