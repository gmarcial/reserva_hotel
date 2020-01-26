using System.Collections.Generic;
using Core;
using Xunit;

namespace Tests
{
    public class HotelChainTests
    {
        [Fact]
        public void ShouldInstantiateWithSuccessAHotelChain()
        {
            //Arrange
            var (lakewood, _) = Hotel.Construct("Lakewood", 3, 110, 80, 90, 80);
            var (bridgewood, _) = Hotel.Construct("Bridgewood", 4, 160, 110, 60, 50);
            var (ridgewood, _) = Hotel.Construct("Ridgewood", 5, 220, 100, 150, 40);

            var hotels = new List<Hotel> { lakewood, bridgewood, ridgewood };

            //Action
            var (hotelChain, result) = HotelChain.Construct(hotels);

            //Assert
            Assert.NotNull(hotelChain);
            Assert.True(result);

            Assert.Equal(hotels.Count, hotelChain.Quantity);
        }

        [Fact]
        public void DontShouldInstantiateAHotelChainWhenHotelIsNull()
        {
            //Arrange
            //Action
            var (hotelChain, result) = HotelChain.Construct(null);

            //Assert
            Assert.False(result);
            Assert.Null(hotelChain);
        }

        [Fact]
        public void DontShouldInstantiateAHotelChainWhenDontHaveAtLeastOneHotel()
        {
            //Arrange
            var hotels = new List<Hotel>();

            //Action
            var (hotelChain, result) = HotelChain.Construct(hotels);

            //Assert
            Assert.False(result);
            Assert.Null(hotelChain);
        }

        [Theory]
        [InlineData(CustomerType.Regular, 3, 0, "Lakewood")]
        [InlineData(CustomerType.Regular, 1, 2, "Bridgewood")]
        [InlineData(CustomerType.Reward, 2, 1, "Ridgewood")]
        public void ShouldIndicateTheHotelCheaperAtThePeriodOfDatesOfAReservationAndTheTypeOfCustomer(CustomerType customerType, int weekDaysAmount, int weekendDaysAmount, string expectedResult)
        {
            //Arrange
            var (lakewood, _) = Hotel.Construct("Lakewood", 3, 110, 90, 80, 80);
            var (bridgewood, _) = Hotel.Construct("Bridgewood", 4, 160, 60, 110, 50);
            var (ridgewood, _) = Hotel.Construct("Ridgewood", 5, 220, 150, 100, 40);

            var hotels = new List<Hotel> { lakewood, bridgewood, ridgewood };

            var (hotelChain, _) = HotelChain.Construct(hotels);

            //Action
            var (hotelCheaper, result) = hotelChain.CalculateCheaper(customerType, weekDaysAmount, weekendDaysAmount);

            //Assert
            Assert.True(result);

            Assert.NotNull(hotelCheaper);
            Assert.NotEmpty(hotelCheaper);
            Assert.Equal(expectedResult, hotelCheaper);
        }
    }
}