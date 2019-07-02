using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Device.Location;
using AirportInformations.Common;
using System.Linq;


namespace AirportInformations.Repository.Models
{
    [DataContract]
    [Serializable]
    public class Airport
    {
        public int Id { get; set; }

        [DataMember(Name = "country")]
        public string country { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }
        [DataMember(Name = "iso")]
        public string iso { get; set; }
        [DataMember(Name = "iata")]
        public string iata { get; set; }
        [DataMember(Name = "continent")]
        public string continent { get; set; }
        [DataMember(Name = "lon")]
        public double lon { get; set; }

        [DataMember(Name = "status")]
        public int status { get; set; }

        [DataMember(Name = "type")]
        public string type { get; set; }

        [DataMember(Name = "lat")]
        public double lat { get; set; }

        [DataMember(Name = "size")]
        public string size { get; set; }

        public DistanceCalculated CalculateDistance(Airport destination)
        {
            var result = new DistanceCalculated();

            var FromCoord = new GeoCoordinate(this.lat, this.lon);
            var DestCoord = new GeoCoordinate(destination.lat, destination.lon);

            result.Distance = FromCoord.GetDistanceTo(DestCoord);
            return result;
        }

        public DistanceCalculated CalculateDistance(Airport source, Airport destination)
        {
            throw new NotImplementedException();
        }


    }
}

