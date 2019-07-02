

namespace AirportInformations.ConcretService.Services
{
    using AirportInformations.Repository.Contracts;
    using AirportInformations.Repository.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AirportInformations.Common;
    using Microsoft.Extensions.Logging;

    public class AirportRepository : IAirportRepository
    {
        private readonly string airportsUrl = ConfigFile.Instance.Url;
        private readonly ILogger<AirportRepository> _logger;

        public DistanceCalculated CalculateDistance(string source, string destination)
        {
            var result = new DistanceCalculated();
            try
            {
                var airports = this.GetAirports();
                var sourceAirport = airports.Where(airport => airport.iata == source).Select(airport => airport).FirstOrDefault<Airport>();
                var destinationAirport = airports.Where(airport => airport.iata == destination).Select(airport => airport).FirstOrDefault<Airport>();

                if (sourceAirport != null && destinationAirport != null)
                {
                    var distance = sourceAirport.CalculateDistance(destinationAirport);

                    distance.Distance = distance.Distance / 1000;
                    result.Distance = Math.Round(distance.Distance, 2);
                }

            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }           
            return result;
        }

        public virtual void saveIntoDataBase(List<Airport> airports, IEnumerable<Country> country)
        {
            ICacheStorage db = new LiteLinqDataBase(CommonConstants.AirportDataBase, CommonConstants.AirportTableName);
            const string tableName = CommonConstants.AirportTableName;

            foreach (Airport airport in airports)
            {
                try
                {
                    airport.country = country.Where(c => c.iso == airport.iso).Select(c => c.country).First().ToString();
                }
                catch (Exception)
                {
                    airport.country = "unknow";
                }

                try
                {
                    if (db.FindOneDataByFieldValue(tableName, "iata", airport.iata) == null)
                        db.SaveData<Airport>(airport, tableName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<Airport> GetAirports()
        {
            LiteLinqDataBase db = new LiteLinqDataBase(CommonConstants.AirportDataBase, CommonConstants.AirportTableName);
            Country AirportCountry = new Country();
            IEnumerable<Airport> airports = db.FindAllDataFromDataTable<Airport>(CommonConstants.AirportTableName);

            return airports;
        }

        public DistanceCalculated CalculateDistance(Airport source, Airport destination)
        {
            throw new NotImplementedException();
        }
    }
}
