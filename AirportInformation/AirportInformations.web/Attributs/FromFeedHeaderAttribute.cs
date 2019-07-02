

namespace AirportInformations.Attributs
{
    using AirportInformations.Common;
    using Microsoft.AspNetCore.Mvc.Filters;
    using AirportInformations.Repository.Models;
    using Microsoft.AspNetCore.Mvc;
    using AirportInformations.Repository.Contracts;
    using System.Collections.Generic;

    public class FromFeedHeaderAttribute : ActionFilterAttribute
    {
        private readonly string headerName = "from-feed";

        public FromFeedHeaderAttribute()
        {
             
        }

        private ICacheStorage CacheStorage { get; set; }

        [NonAction]
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            IEnumerable<Airport> cacheData = this.CacheStorage.FindAllDataFromDataTable<Airport>(CommonConstants.AirportTableName);
           // context.HttpContext.Response.Headers.Add(this.headerName, );
        }

        protected void Init(ICacheStorage cacheHandler)
        {
            //this.CacheHandler = cacheHandler;
        }
    }
}
