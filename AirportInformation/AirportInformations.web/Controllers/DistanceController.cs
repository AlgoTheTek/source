
namespace AirportInformations.web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using AirportInformations.Repository.Models;
    using AirportInformations.Attributs;
    using AirportInformations.Repository.Contracts;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using AirportInformations.Common;
    using System;
    public class DistanceController : Controller
    {
        private IAirportRepository _airportRepository { get; set; }

        [HttpGet]
        [ResponseCache(VaryByHeader = "from-feed", Duration = 300)]
        public ActionResult Distance()
        {
            //initData();
            IEnumerable<Airport> airports = _airportRepository.GetAirports();
            return View(airports);
        }

    }
    
}