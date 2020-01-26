using System;
using System.Collections.Generic;
using System.Globalization;
using Core;

namespace Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            args[0] = args[0].Replace(":", "");
            var resultEnumTryParse = Enum.TryParse(typeof(CustomerType), args[0], false, out var customerType);
            if (!resultEnumTryParse) Utils.Error("Não foi encontrado o tipo de cliente informado.");

            var daysAmount = Utils.CountDaysAmount(args);

            var interError = "Houve um erro interno referente a rede de hoteis.";
            var (nerio, resultNerio) = Hotel.Construct("Nerio", 3, 110, 90, 80, 80);
            if (!resultNerio) Utils.Error(interError);

            var (coimbra, resultCoimbra) = Hotel.Construct("Coimbra", 4, 160, 60, 110, 50);
            if (!resultCoimbra) Utils.Error(interError);

            var (manso, resultManso) = Hotel.Construct("Manso", 5, 220, 150, 100, 40);
            if (!resultManso) Utils.Error(interError);

            var hotels = new List<Hotel> { nerio!, coimbra!, manso! };

            var (hotelChain, resultHotelChain) = HotelChain.Construct(hotels);
            if (!resultHotelChain) Utils.Error(interError);

            var (hotelCheaper, resultHotelCheaper) = hotelChain.CalculateCheaper((CustomerType)customerType, daysAmount.weekDaysAmount, daysAmount.weekendDaysAmount);
            if (!resultHotelCheaper) Utils.Error(interError);

            Console.WriteLine($"O Hotel mais barato da rede é o: {hotelCheaper}");
        }
    }
}
