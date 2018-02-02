using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRepublic.Test.Abstract;
using TravelRepublic.Test.Entities;

namespace TravelRepublic.Test.Concrete
{
    public class MyDependancyResolver
    {
        /// <summary>
        /// Depending on the filterCategory string value, provides the implementation for IFlightFilter interfce
        /// </summary>
        /// <param name="filterCategoty">FilterCategory string </param>
        /// <returns>object implementing the IFlightFilter interface</returns>
        public IFlightFilter GetFlightFilterImplementation(string filterCategoty,IFlightBuilder flightBuilder)
        {
            IFlightFilter flightFilter = null;
            switch (filterCategoty)
            {
                case "0":
                    flightFilter = (IFlightFilter)Activator.CreateInstance(typeof(FlightFilter),flightBuilder);
                    break;
                case "1":
                    flightFilter = (IFlightFilter)Activator.CreateInstance(typeof(FlightFilter1), flightBuilder);
                    break;
                case "2":
                    flightFilter = (IFlightFilter)Activator.CreateInstance(typeof(FlightFilter2), flightBuilder);
                    break;
                case "3":
                    flightFilter = (IFlightFilter)Activator.CreateInstance(typeof(FlightFilter3), flightBuilder);
                    break;
            }
            return flightFilter;
        }

        /// <summary>
        /// Gives concrete class of the interface
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetConcreteClass(Type type)
        {
            if (type == typeof(IFlightBuilder))
                return new FlightBuilder();
            else if (type == typeof(IFlightPrinter))
                return new FlightPrinterConsole();
            else return null;
        }
    }
}
