using System.Collections;
using System.Data.SqlClient;
using System.Globalization;

namespace Assurant.Assessments.SkiLiftPricing
{
  public class PriceCalculator
  {
    public SqlConnection _sqlConnection;

    public double CalculatePrice(Ticket ticket)
    {
      _sqlConnection = new SqlConnection("MyConnectionString");
      _sqlConnection.Open();

      double basePrice;
      using (var costCmd = new SqlCommand( //
               "SELECT cost FROM base_price " + //
               "WHERE type = @type", _sqlConnection))
      {
        costCmd.Parameters.AddWithValue("@type", ticket.Type);
        costCmd.Prepare();
        basePrice = (int)costCmd.ExecuteScalar();

        if (ticket.Age != null && ticket.Age < 6)
        {
          return 0;
        }
        else
        {
          if (!"night".Equals(ticket.Type))
          {
            var holidaysDates = new ArrayList();
            using (var holidayCmd = new SqlCommand( //
                     "SELECT * FROM holidays", _sqlConnection))
            {
              holidayCmd.Prepare();
              using (var holidays = holidayCmd.ExecuteReader())
              {
                while (holidays.Read())
                {
                  holidaysDates.Add(holidays.GetDateTime(3));
                }
              }
            }

            int reduction = 0;
            var isHoliday = false;
            foreach (DateTime holiday in holidaysDates)
            {
              if (ticket.Date != null)
              {
                DateTime d = DateTime.ParseExact(ticket.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (d.Year == holiday.Year &&
                    d.Month == holiday.Month &&
                    d.Date == holiday.Date)
                {
                  isHoliday = true;
                }
              }
            }

            if (ticket.Date != null)
            {
              DateTime d = DateTime.ParseExact(ticket.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
              if (!isHoliday && (int)d.DayOfWeek == 1)
              {
                reduction = 35;
              }
            }

            if (ticket.Age != null && ticket.Age < 15)
            {
              return Math.Ceiling(basePrice * .7);
            }
            else
            {
              if (ticket.Age == null)
              {
                double cost = basePrice * (1 - reduction / 100.0);
                return cost;
              }
              else
              {
                if (ticket.Age > 64)
                {
                  double cost = basePrice * .75 * (1 - reduction / 100.0);
                  return cost;
                }
                else
                {
                  double cost = basePrice * (1 - reduction / 100.0);
                  return cost;
                }
              }
            }
          }
          else
          {
            if (ticket.Age != null && ticket.Age >= 6)
            {
              if (ticket.Age > 64)
              {
                return Math.Ceiling(basePrice * .4);
              }
              else
              {
                return basePrice;
              }
            }
            else
            {
              return 0;
            }
          }
        }
      }
    }

    public double CalculatePrice(List<Ticket> tickets)
    {
      double totalPrice = 0;
      for (int i = 0; i < tickets.Count; i++)
      {
        Ticket ticket = tickets[i];
        double basePrice;
        using (var costCmd = new SqlCommand( //
                "SELECT cost FROM base_price " + //
                "WHERE type = @type", _sqlConnection))
        {
          costCmd.Parameters.AddWithValue("@type", ticket.Type);
          costCmd.Prepare();
          basePrice = (int)costCmd.ExecuteScalar();

          if (ticket.Age != null && ticket.Age < 6)
          {
            totalPrice += 0;
          }
          else
          {
            if (!"night".Equals(ticket.Type))
            {
              var holidaysDates = new ArrayList();
              using (var holidayCmd = new SqlCommand( //
                       "SELECT * FROM holidays", _sqlConnection))
              {
                holidayCmd.Prepare();
                using (var holidays = holidayCmd.ExecuteReader())
                {
                  while (holidays.Read())
                  {
                    holidaysDates.Add(holidays.GetDateTime(3));
                  }
                }
              }

              int reduction = 0;
              var isHoliday = false;
              foreach (DateTime holiday in holidaysDates)
              {
                if (ticket.Date != null)
                {
                  DateTime d = DateTime.ParseExact(ticket.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                  if (d.Year == holiday.Year &&
                      d.Month == holiday.Month &&
                      d.Date == holiday.Date)
                  {
                    isHoliday = true;
                  }
                }
              }

              if (ticket.Date != null)
              {
                DateTime d = DateTime.ParseExact(ticket.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (!isHoliday && (int)d.DayOfWeek == 1)
                {
                  reduction = 35;
                }
              }

              if (ticket.Age != null && ticket.Age < 15)
              {
                totalPrice += Math.Ceiling(basePrice * .7);
              }
              else
              {
                if (ticket.Age == null)
                {
                  double cost = basePrice * (1 - reduction / 100.0);
                  totalPrice += cost;
                }
                else
                {
                  if (ticket.Age > 64)
                  {
                    double cost = basePrice * .75 * (1 - reduction / 100.0);
                    totalPrice += cost;
                  }
                  else
                  {
                    double cost = basePrice * (1 - reduction / 100.0);
                    totalPrice += cost;
                  }
                }
              }
            }
            else
            {
              if (ticket.Age != null && ticket.Age >= 6)
              {
                if (ticket.Age > 64)
                {
                  totalPrice += Math.Ceiling(basePrice * .4);
                }
                else
                {
                  totalPrice += basePrice;
                }
              }
              else
              {
                totalPrice += 0;
              }
            }
          }
        }
      }
      return totalPrice;
    }
  }
}