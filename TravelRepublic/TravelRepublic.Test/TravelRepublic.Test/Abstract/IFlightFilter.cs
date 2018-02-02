using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRepublic.Test.Entities;

namespace TravelRepublic.Test.Abstract
{

    /// <summary>
    /// All FlightBuiders must implement this interface to provide Flight data from various datasources
    /// </summary>
    public interface IFlightBuilder
    {
        IList<Flight> GetFlights();
    }

    /// <summary>
    /// All the filter classes must implement this Interface
    /// </summary>
    public interface IFlightFilter
    {
        IList<Flight> GetFilterResult();
        int ParameterCount { get; }
        object[] ParameterArray { set; }
    }

    /// <summary>
    /// We need to implement this interface for Various printing purposes like : PrintToConsole/PrintToFile etc
    /// </summary>
    public interface IFlightPrinter
    {
        void PrintFlights(IList<Flight> flights);
    }
}
