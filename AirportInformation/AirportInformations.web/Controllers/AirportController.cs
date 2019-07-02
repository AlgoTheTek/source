

namespace AirportInformations.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using AirportInformations.Repository.Models;
    using AirportInformations.Attributs;
    using AirportInformations.Repository.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AirportInformations.Common;
    using System;
    using Microsoft.Extensions.Logging;

    public class AirportController : Controller
    {        
        private readonly ILogger<AirportController> _logger;

        //[ResponseCache(VaryByHeader = "from-feed", Duration = 300)]
        public IActionResult Index()
        {
            initData();
            IEnumerable<Airport> airports = _airportRepository.GetAirports();
            return View(airports);
        }

        [HttpGet]
        [ResponseCache(VaryByHeader = "from-feed", Duration = 300)]
        public ActionResult Distance()
        {
            //initData();
            IEnumerable<Airport> airports = _airportRepository.GetAirports();
            return View(airports);
        }
        
        public AirportController(IAirportRepository airportRepository)
        {
            this._airportRepository = airportRepository;
            _countryRepository = new Country();
            _httpMgr = new HttpManager();
        }

        private IAirportRepository _airportRepository { get; set; }
        private ICountryRepository _countryRepository { get; set; }
        private IHttpManager _httpMgr { get; set; }

        [HttpGet]
        public JsonResult CalculateDistance(Airport source, Airport destination)
        {
            var distance = this._airportRepository.CalculateDistance(source, destination);
            return this.Json(distance);
        }

        [HttpGet]        
        [FromFeedHeader]
        public JsonResult GetAirportsList()
        {
            var airports = this._airportRepository.GetAirports();
            return this.Json(airports);
        }

        private void initData()
        {
            List<Airport> airports;
            IEnumerable<Country> country;

            //Get from url
            try
            {
                string url = ConfigFile.Instance.Url;

                var task = Task.Run(async () => await _httpMgr.PostUrlAPI(url));
                airports = task.GetAwaiter().GetResult();

                country = _countryRepository.GetCountriesFromDataTable();               
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }

            this._airportRepository.saveIntoDataBase(airports, country);
        }        
    }
}