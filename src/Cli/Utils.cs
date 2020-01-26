using System;
using System.Globalization;
using Core;

namespace Cli
{
    public static class Utils
    {
        public static void Error(string message)
        {
            Console.WriteLine(message);
            Environment.Exit(0);
        }

        public static (int weekDaysAmount, int weekendDaysAmount) CountDaysAmount(string[] args)
        {
            var daysAmount = (weekDaysAmount: 0, weekendDaysAmount: 0);
            for (int i = 1; i < args.Length; i++)
            {
                args[i] = args[i].Replace(",", "");
                var resultDateTimeTryParseExact = DateTime.TryParseExact(args[i], "ddMMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
                if (!resultDateTimeTryParseExact) Utils.Error("A data ou formato dela esta invalido, o correto seria: 16Mar2009");

                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        daysAmount.weekDaysAmount++;
                        break;

                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        daysAmount.weekendDaysAmount++;
                        break;

                    default:
                        break;
                }
            }

            return daysAmount;
        }
    }
}