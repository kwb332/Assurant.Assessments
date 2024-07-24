
using Assurant.Pricing.Infrastructure.Contract.DataModel;
using Assurant.Pricing.Infrastructure.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assurant.Pricing.Infrastructure.Persistence
{
    public class HolidayRepository : IHolidayRepository
    {
        public readonly PricingContext _db;

        public HolidayRepository(PricingContext db)
        {
            _db = db;
           
        }
        public List<DateTime> GetHolidays()
        {
            try
            {
                List<Holiday> holidays = _db.Holiday.Select(x => x).ToList();
                List<DateTime> result = new List<DateTime>();
                foreach (Holiday holiday in holidays)
                {
                    DateTime dateTime = holiday.HolidayDate;
                    result.Add(dateTime);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
