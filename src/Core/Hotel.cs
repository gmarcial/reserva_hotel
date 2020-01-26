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
            var returnResult = (hotel: (Hotel)null, result: false);

            if (string.IsNullOrEmpty(name)) return returnResult;
            if (classification < 0) return returnResult;
            if (dayWeekValue <= 0) return returnResult;
            if (dayWeekendValue <= 0) return returnResult;
            if (dayWeekRewardValue <= 0) return returnResult;
            if (dayWeekendRewardValue <= 0) return returnResult;

            returnResult.hotel = new Hotel(name, classification, dayWeekValue, dayWeekendValue, dayWeekRewardValue, dayWeekendRewardValue);
            returnResult.result = true;
            
            return returnResult;
        }

        public (float totalValue, bool result) CalculateDaysValue(CustomerType type, int weekDaysAmount, int weekendDaysAmount)
        {
            var returnResult = (totalValue: 0f, result: false);
            if(!Enum.IsDefined(typeof(CustomerType), type)) return returnResult;

            switch (type)
            {
                case CustomerType.Regular:
                    returnResult.totalValue = calculateRegularDaysValue(weekDaysAmount, weekendDaysAmount);
                    returnResult.result = true;
                    return returnResult;

                case CustomerType.Reward:
                    returnResult.totalValue = calculateRewardDaysValue(weekDaysAmount, weekendDaysAmount);
                    returnResult.result = true;
                    return returnResult;

                default:
                    return returnResult;
            }
        }

        private float calculateRegularDaysValue(int weekDaysAmount, int weekendDaysAmount) => ((this.WeeklyValue * weekDaysAmount) + (this.WeekendValue * weekendDaysAmount));

        private float calculateRewardDaysValue(int weekDaysAmount, int weekendDaysAmount) => ((this.WeeklyValueReward * weekDaysAmount) + (this.WeekendValueReward * weekendDaysAmount));
    }
}
