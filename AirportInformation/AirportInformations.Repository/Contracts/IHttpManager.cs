using AirportInformations.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirportInformations.Repository.Contracts
{
    public interface IHttpManager
    {
        Task<List<Airport>> PostUrlAPI(string url);
    }
}
