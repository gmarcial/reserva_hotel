using System;

namespace Core
{
    public class Hotel
    {
        public string Name { get; private set; }
        public int Classification { get; private set; }
        public float WeeklyValue { get; private set; }
        public float WeekendValue { get; private set; }
        public float WeeklyValueReward { get; private set; }
        public float WeekendValueReward { get; private set; }

        private Hotel(string name, int classification, float weeklyValue, float weekendValue, float weeklyValueReward, float weekendValueReward)
        {
            Name = name;
            Classification = classification;
            WeeklyValue = weeklyValue;
            WeekendValue = weekendValue;
            WeeklyValueReward = weeklyValueReward;
            WeekendValueReward = weekendValueReward;
        }

        public static (Hotel hotel, bool result) Construct(string name, int classification, float dayWeekValue, float dayWeekendValue, float dayWeekRewardValue, float dayWeekendRewardValue)
        {
            var fail = (hotel: (Hotel)null, result: false);

            if (string.IsNullOrEmpty(name)) return fail;
            if (classification < 0) return fail;
            if (dayWeekValue <= 0) return fail;
            if (dayWeekendValue <= 0) return fail;
            if (dayWeekRewardValue <= 0) return fail;
            if (dayWeekendRewardValue <= 0) return fail;

            return (hotel: new Hotel(name, classification, dayWeekValue, dayWeekendValue, dayWeekRewardValue, dayWeekendRewardValue), result: true);
        }

        public (float totalValue, bool result) CalculateDaysValue(CustomerType type, int weekDaysAmount, int weekendDaysAmount)
        {
            var fail = (totalValue: 0, result: false);
            if(!Enum.IsDefined(typeof(CustomerType), type)) return fail;

            switch (type)
            {
                case CustomerType.Regular:
                    return (calculateRegularDaysValue(weekDaysAmount, weekendDaysAmount), true);

                case CustomerType.Reward:
                    return (calculateRewardDaysValue(weekDaysAmount, weekendDaysAmount), true);

                default:
                    return fail;
            }
        }

        private float calculateRegularDaysValue(int weekDaysAmount, int weekendDaysAmount) => ((this.WeeklyValue * weekDaysAmount) + (this.WeekendValue * weekendDaysAmount));

        private float calculateRewardDaysValue(int weekDaysAmount, int weekendDaysAmount) => ((this.WeeklyValueReward * weekDaysAmount) + (this.WeekendValueReward * weekendDaysAmount));
    }
}
