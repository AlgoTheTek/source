using AirportInformations.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportInformations.Repository.Contracts
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetEuropeenCountries();
        IEnumerable<Country> GetCountriesFromDataTable();
    }
}
