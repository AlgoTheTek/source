using AirportInformations.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportInformations.Repository.Contracts
{
    public interface IAirportRepository
    {
        IEnumerable<Airport> GetAirports();
        DistanceCalculated CalculateDistance(Airport source, Airport destination);
        void saveIntoDataBase(List<Airport> airports, IEnumerable<Country> country);
    }
}
