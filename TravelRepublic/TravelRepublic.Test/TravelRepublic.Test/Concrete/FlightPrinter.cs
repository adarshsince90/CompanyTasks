using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRepublic.Test.Abstract;
using TravelRepublic.Test.Entities;

namespace TravelRepublic.Test.Concrete
{
    public class FlightPrinterConsole: IFlightPrinter
    {
        public void PrintFlights(IList<Flight> flights)
        {
            if (flights != null && flights.Count > 0)
            {
                Console.WriteLine("Result:");
                foreach (Flight flight in flights)
                {
                    Console.WriteLine("Flight Number: {0}", flights.IndexOf(flight) + 1);
                    Console.WriteLine("No. of Segments:{0}", flight.Segments.Count);
                    foreach (Segment segment in flight.Segments)
                    {
                        Console.WriteLine("\tSegment Number: {0}", flight.Segments.IndexOf(segment) + 1);
                        Console.WriteLine("\t\tDepartureDate : {0}", segment.DepartureDate.ToLongDateString()+" "+ segment.DepartureDate.ToLongTimeString());
                        Console.WriteLine("\t\tArrivalDate : {0}\n", segment.ArrivalDate.ToLongDateString()+" " + segment.ArrivalDate.ToLongTimeString());
                    }
                }
            }
            else
            {
                Console.WriteLine("\t\tSorry no flights found. Try another Filter.");
            }
            Console.WriteLine("-----------------------------------------------------------------------");
        }
    }
}
