using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AirportInformations.Common;
using AirportInformations.Repository.Contracts;
using Newtonsoft.Json;

namespace AirportInformations.Repository.Models
{    public class Country : ICountryRepository
    {
        public string iso { get; set; }
        public string country { get; set; }
        public Airport airport { get; set; }

        /// <summary>
        /// This Get europeen countries from local file
        /// </summary>
        /// <returns>Countries list</returns>
        public virtual IEnumerable<Country> GetEuropeenCountries()
        {
            string path = @ConfigFile.Instance.localEuCountries;
            using (StreamReader r = new StreamReader(path, Encoding.UTF8))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Country>>(json);
            }
        }

        /// <summary>
        /// This Get europeen countries from Lite database
        /// </summary>
        /// <returns>Countries list or default from local json file</returns>
        public virtual IEnumerable<Country> GetCountriesFromDataTable()
        {

            /* // this Will works after insert into Countries in DB
             * IEnumerable<Country> countries;
            LiteLinqDataBase db = new LiteLinqDataBase(CommonConstants.AirportDataBase, CommonConstants.CountryTableName);
            try
            {
                countries = db.FindAllDataFromDataTable<Country>(CommonConstants.CountryTableName);                
            }
            catch (Exception)
            {
                countries = GetEuropeenCountries();
            }
            return countries;
            */
            return GetEuropeenCountries();
        }
    }
}
