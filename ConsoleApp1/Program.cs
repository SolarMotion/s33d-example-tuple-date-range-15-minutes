using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static ConsoleApp1.CommomExtension;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var order_date_from = "20201127";
                var order_date_to = "20201127";
                var orderFromDT = order_date_from.ToDate();
                var orderToDT = order_date_to.ToDate().AddDays(1).AddSeconds(-1);

                var dateRanges = new List<Tuple<string, string>>();
                var currentDT = orderFromDT;
                while (orderToDT.Ticks >= currentDT.Ticks)
                {
                    // Change .AddMinutes(15) to adjust the duration
                    var newDT = currentDT.AddMinutes(15).AddSeconds(-1);

                    var fromDate = currentDT.AddHours(-8).ToString("s"); //Adjustment for Shopify timezone
                    var toDate = newDT.AddHours(-8).ToString("s"); //Adjustment for Shopify timezone
                    dateRanges.Add(new Tuple<string, string>(fromDate, toDate));

                    // Update fromDate
                    currentDT = newDT.AddSeconds(1);
                }

                foreach (var dateRange in dateRanges)
                {
                    WriteLine($"{dateRange.Item1} --- {dateRange.Item2}");
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.ToString());
            }
            finally
            {
                ReadLine();
            }
        }
    }

    public static class CommomExtension
    {
        public static DateTime ToDate(this string value, string format = "yyyyMMdd")
        {
            DateTime temp;
            DateTime.TryParseExact(value, format, null, DateTimeStyles.None, out temp);

            return temp;
        }
    }

}
