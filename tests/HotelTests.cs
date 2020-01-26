using Core;
using Xunit;

namespace Tests
{
    public class HotelTests
    {
        [Fact]
        public void ShouldInstantiateWithSuccessAHotel()
        {
            //Arrange
            var name = "Lakewood";
            var classification = 3;
            var weeklyValue = 110;
            var weekendValue = 80;
            var weeklyValueReward = 90;
            var weekendValueReward = 80;

            //Action
            var (hotel, result) = Hotel.Construct(name, classification, weeklyValue, weekendValue, weeklyValueReward, weekendValueReward);

            //Assert
            Assert.NotNull(hotel);
            Assert.True(result);

            Assert.Equal(name, hotel!.Name);
            Assert.Equal(classification, hotel.Classification);
            Assert.Equal(weeklyValue, hotel.WeeklyValue);
            Assert.Equal(weekendValue, hotel.WeekendValue);
            Assert.Equal(weeklyValueReward, hotel.WeeklyValueReward);
            Assert.Equal(weekendValueReward, hotel.WeekendValueReward);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DontShouldInstantiateAHotelWhenNameIsNullOrEmpty(string name)
        {
            //Arrange
            var classification = 3;
            var weeklyValue = 110f;
            var weekendValue = 80f;
            var weeklyValueReward = 90f;
            var weekendValueReward = 80f;

            //Action
            var (hotel, result) = Hotel.Construct(name, classification, weeklyValue, weekendValue, weeklyValueReward, weekendValueReward);

            //Assert 
            Assert.Null(hotel);
            Assert.False(result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-123234)]
        [InlineData(-21111)]
        public void DontShouldInstantiateAHotelWhenClassificaitonIsLessThanZero(int classification)
        {
            //Arrange
            var name = "Lakewood";
            var weeklyValue = 110f;
            var weekendValue = 80f;
            var weeklyValueReward = 90f;
            var weekendValueReward = 80f;

            //Action
            var (hotel, result) = Hotel.Construct(name, classification, weeklyValue, weekendValue, weeklyValueReward, weekendValueReward);

            //Assert 
            Assert.Null(hotel);
            Assert.False(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-123234)]
        [InlineData(-21111)]
        public void DontShouldInstantiateAHotelWhenWeeklyValueIsLessThanOrEqualTheZero(float weeklyValue)
        {
            //Arrange
            var name = "Lakewood";
            var classification = 5;
            var weekendValue = 80f;
            var weeklyValueReward = 90f;
            var weekendValueReward = 80f;

            //Action
            var (hotel, result) = Hotel.Construct(name, classification, weeklyValue, weekendValue, weeklyValueReward, weekendValueReward);

            //Assert 
            Assert.Null(hotel);
            Assert.False(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-123234)]
        [InlineData(-21111)]
        public void DontShouldInstantiateAHotelWhenWeekendValueIsLessThanOrEqualTheZero(float weekendValue)
        {
            //Arrange
            var name = "Lakewood";
            var classification = 5;
            var weeklyValue = 10f;
            var weeklyValueReward = 90f;
            var weekendValueReward = 80f;

            //Action
            var (hotel, result) = Hotel.Construct(name, classification, weeklyValue, weekendValue, weeklyValueReward, weekendValueReward);

            //Assert 
            Assert.Null(hotel);
            Assert.False(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-123234)]
        [InlineData(-21111)]
        public void DontShouldInstantiateAHotelWhenWeeklyValueRewardIsLessThanOrEqualTheZero(float weeklyValueReward)
        {
            //Arrange
            var name = "Lakewood";
            var classification = 5;
            var weeklyValue = 10f;
            var weekendValue = 80f;
            var weekendValueReward = 80f;

            //Action
            var (hotel, result) = Hotel.Construct(name, classification, weeklyValue, weekendValue, weeklyValueReward, weekendValueReward);

            //Assert 
            Assert.Null(hotel);
            Assert.False(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-123234)]
        [InlineData(-21111)]
        public void DontShouldInstantiateAHotelWhenWeekendValueRewardIsLessThanOrEqualTheZero(float weekendValueReward)
        {
            //Arrange
            var name = "Lakewood";
            var classification = 5;
            var weeklyValue = 10f;
            var weekendValue = 80f;
            var weeklyValueReward = 1f;

            //Action
            var (hotel, result) = Hotel.Construct(name, classification, weeklyValue, weekendValue, weeklyValueReward, weekendValueReward);

            //Assert 
            Assert.Null(hotel);
            Assert.False(result);
        }

        [Theory]
        [InlineData(4, 10)]
        [InlineData(15, 5)]
        [InlineData(7, 9)]
        public void ShouldCalculateTheTotalValueTheToBePayStartingOfTheQuantityRegularDays(int weekDaysAmount, int weekendDaysAmount)
        {
            //Arrange
            var name = "Lakewood";
            var classification = 3;
            var weeklyValue = 110;
            var weekendValue = 80;
            var weeklyValueReward = 90;
            var weekendValueReward = 80;

            var expectedTotalValue = ((weeklyValue * weekDaysAmount) + (weekendValue * weekendDaysAmount));

            var (hotel, _) = Hotel.Construct(name, classification, weeklyValue, weekendValue, weeklyValueReward, weekendValueReward);

            //Action
            var (totalValue, result) = hotel!.CalculateDaysValue(CustomerType.Regular, weekDaysAmount, weekendDaysAmount);

            //Assert
            Assert.True(result);
            Assert.IsType<float>(totalValue);
            Assert.True(totalValue > 0);
            Assert.Equal(expectedTotalValue, totalValue);
        }

        [Theory]
        [InlineData(4, 10)]
        [InlineData(15, 5)]
        [InlineData(7, 9)]
        public void ShouldCalculateTheTotalValueTheToBePayStartingOfTheQuantityRewardDays(int weekDaysAmount, int weekendDaysAmount)
        {
            //Arrange
            var name = "Lakewood";
            var classification = 3;
            var weeklyValue = 110;
            var weekendValue = 80;
            var weeklyValueReward = 90;
            var weekendValueReward = 80;

            var expectedTotalValue = ((weeklyValueReward * weekDaysAmount) + (weekendValueReward * weekendDaysAmount));

            var (hotel, _) = Hotel.Construct(name, classification, weeklyValue, weekendValue, weeklyValueReward, weekendValueReward);

            //Action
            var (totalValue, result) = hotel!.CalculateDaysValue(CustomerType.Reward, weekDaysAmount, weekendDaysAmount);

            //Assert
            Assert.True(result);
            Assert.IsType<float>(totalValue);
            Assert.True(totalValue > 0);
            Assert.Equal(expectedTotalValue, totalValue);
        }
    }
}