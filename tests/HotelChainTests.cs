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
            var (nerio, _) = Hotel.Construct("Nerio", 3, 110, 80, 90, 80);
            var (coimbra, _) = Hotel.Construct("Coimbra", 4, 160, 110, 60, 50);
            var (manso, _) = Hotel.Construct("Manso", 5, 220, 100, 150, 40);

            var hotels = new List<Hotel> { nerio!, coimbra!, manso! };

            //Action
            var (hotelChain, result) = HotelChain.Construct(hotels);

            //Assert
            Assert.NotNull(hotelChain);
            Assert.True(result);

            Assert.Equal(hotels.Count, hotelChain!.Quantity);
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
        [InlineData(CustomerType.Regular, 3, 0, "Nerio")]
        [InlineData(CustomerType.Regular, 1, 2, "Coimbra")]
        [InlineData(CustomerType.Rewards, 2, 1, "Manso")]
        public void ShouldIndicateTheHotelCheaperAtThePeriodOfDatesOfAReservationAndTheTypeOfCustomer(CustomerType customerType, int weekDaysAmount, int weekendDaysAmount, string expectedResult)
        {
            //Arrange
            var (nerio, _) = Hotel.Construct("Nerio", 3, 110, 90, 80, 80);
            var (coimbra, _) = Hotel.Construct("Coimbra", 4, 160, 60, 110, 50);
            var (manso, _) = Hotel.Construct("Manso", 5, 220, 150, 100, 40);

            var hotels = new List<Hotel> { nerio!, coimbra!, manso! };

            var (hotelChain, _) = HotelChain.Construct(hotels);

            //Action
            var (hotelCheaper, result) = hotelChain!.CalculateCheaper(customerType, weekDaysAmount, weekendDaysAmount);

            //Assert
            Assert.True(result);

            Assert.NotNull(hotelCheaper);
            Assert.NotEmpty(hotelCheaper);
            Assert.Equal(expectedResult, hotelCheaper);
        }
    }
}