using MyHelpersApp.Data;
using System;

namespace MyHelpersApp.BL.Services
{
    public static class HelperService
    {
        public static DateTime GetNextDate(DateTime firstDate, int repetitionType, int index)
        {
            RepetitionType repeat = (RepetitionType)repetitionType;
            switch (repeat)
            {
                case RepetitionType.weekly: return firstDate.AddDays(7 * (index + 1));
                case RepetitionType.monthly: return firstDate.AddMonths(index + 1);
                case RepetitionType.yearly: return firstDate.AddYears(index + 1);
            }

            return firstDate;
        }
    }
}
