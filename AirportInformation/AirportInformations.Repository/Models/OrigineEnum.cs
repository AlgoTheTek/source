using System;
using System.Collections.Generic;
using System.Text;

namespace AirportInformations.Repository.Models
{
    public abstract class OrigineEnum
    {
        public OrigineEnum()
        {
            None = 0;
            FromCache = 0;
            FromHttpRequest = 0;
        }

        public byte None { get;  set; }
        public byte FromCache { get;  set; }
        public byte FromHttpRequest { get;  set; }

    }
}
