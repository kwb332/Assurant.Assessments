﻿using Assurant.Pricing.Infrastructure.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assurant.Pricing.Domain.Contract.Interface
{
    public interface IRuleEngine
    {
        public decimal CalculatePrice(ITicket ticket, IPriceRepository priceRepository, IHolidayRepository holidayRepository);
        public void SetComponent(IRuleEngine ruleEngine);

        public bool EndCalulations {  get; set; }
        
    }
}
