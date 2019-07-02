using System;
using System.Collections.Generic;
using System.Text;

namespace AirportInformations.Repository.Models
{
    public sealed class ConfigFile
    {
        private static readonly Lazy<ConfigFile> lazy =
            new Lazy<ConfigFile>(() => new ConfigFile());
 
        public static ConfigFile Instance { get { return lazy.Value; } }

        public string Url { get; set; }
        public string localEuCountries { get; set; }
        public string localDbPath { get; set; }

        private ConfigFile()
        {
        }
    }
}
