using Assurant.Pricing.Domain.Contract.Interface;
using Assurant.Pricing.Domain.DomainModel;
using Assurant.Pricing.Infrastructure.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace Assurant.Pricing.Domain.RuleEngine
{
    public class DateRuleEngineDecorator : RuleEngineDecorator
    {
        

        public DateRuleEngineDecorator()
        {
            
        }
        public DateRuleEngineDecorator(RuleEngine ruleEngine) : base(ruleEngine)
        {
            this._ruleEngine = ruleEngine;
        }
        public override double CalculatePrice(ITicket ticket, IPriceRepository priceRepository, IHolidayRepository holidayRepository)
        {
            /*   -If the pass is a night pass and the skier's age is greater than or equal to 6, the price is 40% off.
   - If the pass is a night pass and the skier's age is less than 6, the price is free.
   - If the pass is not a holiday pass and it is a Monday, there is a $35 reduction in the price.
            */
            double price = base.CalculatePrice(ticket, priceRepository, holidayRepository);
            if (Constants.Night == ticket.Type)
            {
                if (ticket.Age >= 6)
                {
                    price = price * .40;

                }
                else
                {
                    price = 0;
                }
            }
            List<DateTime> holidaysDates = holidayRepository.GetHolidays();
      
            var isHoliday = CheckDate(ticket, holidaysDates);


            if (ticket.Date != null)
            {
           
                if (!isHoliday && (int)ticket.Date.DayOfWeek == 1)
                {
                   price = price * .35;
                }
            }
            return price;
        }

        public override void SetComponent(IRuleEngine ruleEngine)
        {
            
        }

        private bool CheckDate(ITicket ticket, List<DateTime> holidaysDates)
        {
            bool isHoliday = false;
            foreach (DateTime holiday in holidaysDates)
            {
                if (ticket.Date != null)
                {

                    if (ticket.Date.Year == holiday.Year &&
                        ticket.Date.Month == holiday.Month &&
                        ticket.Date.Date == holiday.Date)
                    {
                        isHoliday = true;
                    }
                }
            }
            return isHoliday;
        }
    }
}
