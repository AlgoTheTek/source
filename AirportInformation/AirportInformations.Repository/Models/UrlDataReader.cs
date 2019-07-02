using System;
using System.Collections.Generic;
using System.Text;

namespace AirportInformations.Repository.Models
{
    public class UrlDataReader
    {
        public virtual string Data { get; set; }

        public virtual OrigineEnum OrigineEnum { get; set; }
    }
}
