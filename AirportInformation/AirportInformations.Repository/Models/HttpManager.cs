using AirportInformations.Repository.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AirportInformations.Repository.Contracts;
using Microsoft.Extensions.Logging;

namespace AirportInformations.Repository.Models
{
    public class HttpManager : IHttpManager
    {
        private readonly ILogger<HttpManager> _logger;


        /// <summary>
        /// This Get JsonResponse from url
        /// <param name=a> url</param>
        /// </summary>
        /// <returns>Json response</returns>
        public async Task<List<Airport>> PostUrlAPI(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (response != null)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();

                        List<Airport> airports = JsonConvert.DeserializeObject<List<Airport>>(jsonString.ToString());
                        return airports.FindAll(c => (c.continent == "EU") && (c.type == "airport") && (c.size == "medium" || c.size == "large"));
                    }
                }
            }
            catch (Exception ex)
            {
               _logger.LogWarning(ex.Message);
            }
            return null;
        }
    }
}
