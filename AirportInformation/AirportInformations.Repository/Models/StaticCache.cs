using System;
using System.Collections.Generic;
using System.Text;

namespace AirportInformations.Repository.Models
{
    public class StaticCache<T>
    {
        public DateTime StaticDataDate { get; set; }

        public T Data { get; set; }
    }
}
