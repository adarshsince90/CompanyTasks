using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRepublic.Test.Abstract;
using TravelRepublic.Test.Concrete;
using TravelRepublic.Test.Entities;

namespace TravelRepublic.Test
{
    class Program
    {
        private static string ValidationString = "Please enter valid filter category";
        private static string SearchAgainString = "Press Enter to repeat the search.";
        static void Main(string[] args)
        {
            string isRepeat = string.Empty;
            do
            {
                try
                {
                    
                    Console.WriteLine("Please select the search Categary:");
                    Console.WriteLine("0. Print all flights");
                    Console.WriteLine("1. Depart before the current date/time.");
                    Console.WriteLine("2. Have a segment with an arrival date before the departure date.");
                    Console.WriteLine("3. Spend more than [X] hours on the ground.");
                    Console.WriteLine("Press e for end");
                    Console.WriteLine("--------------------------------------------------------------");

                    MyDependancyResolver myDependancyResolver = new MyDependancyResolver();
                    IFlightBuilder flightBuilder =(IFlightBuilder)myDependancyResolver.GetConcreteClass(typeof(IFlightBuilder));
                    IFlightFilter flightFilter = null;
                    IList<Flight> flight = null;
                    
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "3":
                        case "2":
                        case "1":
                        case "0":
                            flightFilter = myDependancyResolver.GetFlightFilterImplementation(input, flightBuilder);
                            if (flightFilter.ParameterCount > 0)
                            {
                                Console.WriteLine("X value:");
                                flightFilter.ParameterArray = new object[] { Console.ReadLine() };
                            }
                            break;
                        case "e":
                            isRepeat = "true";
                            break;
                        default:
                            Console.WriteLine(ValidationString);
                            break;
                    }
                    if (flightFilter != null)
                    {
                        flight = flightFilter.GetFilterResult();
                        IFlightPrinter printFlight = (IFlightPrinter)myDependancyResolver.GetConcreteClass(typeof(IFlightPrinter));
                        printFlight.PrintFlights(flight);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(SearchAgainString);
                    isRepeat = "true";
                    continue;
                }
            } while (string.IsNullOrEmpty(isRepeat));
        }
    }
}
