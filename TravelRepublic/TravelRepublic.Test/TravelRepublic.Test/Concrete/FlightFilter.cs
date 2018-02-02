using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRepublic.Test.Abstract;
using TravelRepublic.Test.Entities;

namespace TravelRepublic.Test.Concrete
{
    /// <summary>
    /// FilterClass which returns all flights
    /// Any new filter class should inherit this class and provide its own implementation of GetFilteResult method
    /// </summary>
    public class FlightFilter : IFlightFilter
    {
        private IFlightBuilder flightBuilder;
        protected int _parameterCount;
        public FlightFilter(IFlightBuilder flightBuilder)
        {
            this.flightBuilder = flightBuilder;
        }

        public IList<Flight> FlightList { get { return flightBuilder.GetFlights(); } }
        //member functions
        public virtual IList<Flight> GetFilterResult() { return FlightList; }
        public virtual object[] ParameterArray { get; set; }
        public int ParameterCount { get { return _parameterCount; } }
    }

    /// <summary>
    /// Class implementing the Filter :Flights departing before given date
    /// </summary>
    public class FlightFilter1 : FlightFilter
    {
        private DateTime searchDate = DateTime.Now;

        public FlightFilter1(IFlightBuilder flightBuilder) : base(flightBuilder) { }
        //member functions
        public override IList<Flight> GetFilterResult()
        {
            IList<Flight> resultFlights = new List<Flight>();
            if (FlightList != null)
            {
                resultFlights = FlightList
                    .Where(a => (a.Segments.Any(b => b.DepartureDate < searchDate))).ToList();
               
                #region foreach loop
                //foreach (Flight flight in FlightList)
                //{
                //    foreach (Segment segment in flight.Segments)
                //    {
                //        if (segment.DepartureDate < searchDate)
                //        {
                //            resultFlights.Add(flight);
                //            break;
                //        }
                //    }
                //}
                #endregion
            }
            return resultFlights;
        }
    }

    /// <summary>
    /// Class which implement the filter where Arrival time is less than departure time
    /// </summary>
    public class FlightFilter2 : FlightFilter
    {
        public FlightFilter2(IFlightBuilder flightBuilder) : base(flightBuilder) { }

        public override IList<Flight> GetFilterResult()
        {
            IList<Flight> resultFlights = new List<Flight>();
            if (FlightList != null)
            {
                resultFlights =
                   FlightList.Where(a => a.Segments.Any(b => b.ArrivalDate < b.DepartureDate)).ToList();
                #region foreach loop
                //foreach (Flight flight in FlightList)
                //{
                //    foreach (Segment segment in flight.Segments)
                //    {
                //        if (segment.ArrivalDate < segment.DepartureDate)
                //        {
                //            resultFlights.Add(flight);
                //            break;
                //        }
                //    }
                //}
                #endregion
            }
            return resultFlights;
        }
    }

    /// <summary>
    /// Class which implement the filter where flight spends more than given number of hours in the airport
    /// And implement additional filters related this class (no Of hours)
    /// </summary>
    public class FlightFilter3 : FlightFilter
    {
        private int noOfHours = 2;

        public FlightFilter3(IFlightBuilder flightBuilder) : base(flightBuilder)
        {
            _parameterCount = 1;
        }

        public override IList<Flight> GetFilterResult()
        {
            IList<Flight> resultFlights = new List<Flight>();
            if (FlightList != null)
            {
                resultFlights =
                     FlightList.Where(a => a.Segments
                         .Any(b => (a.Segments.Count != a.Segments.IndexOf(b) + 1) && (a.Segments[a.Segments.IndexOf(b) + 1].DepartureDate - b.ArrivalDate).Hours > noOfHours)).ToList();
                
                #region foreach loop
                //foreach (Flight flight in FlightList)
                //{
                //    for (int i = 0; i < flight.Segments.Count; i++)
                //    {
                //        if (i + 1 < flight.Segments.Count)
                //        {
                //            if ((flight.Segments[i + 1].DepartureDate - flight.Segments[i].ArrivalDate).Hours > noOfHours)
                //            {
                //                resultFlights.Add(flight);
                //                break;
                //            }
                //        }
                //    }
                //}
                #endregion
            }
            return resultFlights;
        }

        public override object[] ParameterArray
        {
            set
            {
                if (value != null && value.Count() > 0)
                {
                    foreach (string parameter in value)
                    {
                        int outInt;
                        if(int.TryParse(parameter,out outInt))
                        {
                            noOfHours = outInt;
                        }
                    }
                }
            }
        }
    }
}
