using System.Collections.Generic;

namespace Core
{
    public class HotelChain
    {
        private IList<Hotel> hotels;
        public int Quantity => hotels.Count;

        private HotelChain(IList<Hotel> hotels)
        {
            this.hotels = hotels;
        }

        public static (HotelChain hotelChain, bool result) Construct(IList<Hotel> hotels)
        {
            var fail = (hotelChain: (HotelChain)null, result: false);

            if (hotels == null) return fail;
            if (hotels.Count <= 0) return fail;

            var hotelChain = new HotelChain(hotels);
            return (hotelChain: hotelChain, result: true);
        }

        public (string cheaper, bool result) CalculateCheaper(CustomerType customerType, int weekDaysAmount, int weekendDaysAmount)
        {
            var lastCheaperName = "";
            float lastCheaperResult = 0;
            var lastCheaperClassification = 0;

            var returnResult = (cheaper: (string)null, result: false);

            foreach (var hotel in this.hotels)
            {
                var (totalValue, result) = hotel.CalculateDaysValue(customerType, weekDaysAmount, weekendDaysAmount);
                if (!result) return returnResult;

                if (lastCheaperResult != 0 && totalValue > lastCheaperResult) continue;
                else if((totalValue == lastCheaperResult && hotel.Classification < lastCheaperClassification)) continue;

                lastCheaperName = hotel.Name;
                lastCheaperResult = totalValue;
                lastCheaperClassification = hotel.Classification;
            }

            returnResult.cheaper = lastCheaperName;
            returnResult.result = true;

            return returnResult;
        }
    }
}