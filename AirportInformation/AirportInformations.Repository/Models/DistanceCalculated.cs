using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AirportInformations.Repository.Models
{

    [DataContract]
    [Serializable]
    public class DistanceCalculated
    {
        public DistanceCalculated()
        {
            this.Unit = "Km";
        }

        [DataMember]
        public double Distance { get; set; }

        [DataMember]
        public string Unit { get; set; }
    }

}
