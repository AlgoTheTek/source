
namespace AirportInformations.Repository.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using LiteDB;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.Extensions.Caching.Memory;
    using AirportInformations.Common;
    using AirportInformations.Repository.Models;
    using AirportInformations.Repository.Contracts;

    public class LiteLinqDataBase :ICacheStorage
    {

        private static IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private readonly int  cacheTimeoutMinutes= 5;
        private string defaultDataBase = CommonConstants.AirportDataBase;

        private static LiteDatabase dataBaseLite = null;

        public LiteLinqDataBase(string cacheKey, string useTable)
        {
            this.defaultDataBase = useTable;
            InitDataBase(cacheKey);
        }

        public void ClearData(string cacheKey = CommonConstants.CacheKeyNone)
        {
            _cache.Remove(cacheKey);
        }

        public virtual bool InsertIntoDataTable(string IntodataTableName, object obj)
        {
            bool success = dataBaseLite.GetCollection<object>(IntodataTableName).Insert(obj);

            //if (success)
            //    dataBaseLite.GetCollection<object>(IntodataTableName).EnsureIndex(x => x.id);
            return success;
        }

        public virtual Airport FindOneDataByFieldValue(string FromdataTableName, string fieldName, string fieldValue)
        {
            return dataBaseLite.GetCollection<Airport>(FromdataTableName).FindOne(Query.EQ(fieldName, fieldValue));
        }

        public virtual IEnumerable<T> FindAllDataFromDataTable<T>(string FromdataTableName)
        {
            return (IEnumerable<T>)dataBaseLite.GetCollection<T>(FromdataTableName).FindAll();
        }

        public virtual void InitDataBase(string strCacheKey)
        {
            object memoryDataBase = null;
            if (_cache.TryGetValue(strCacheKey, out memoryDataBase))
            {
                dataBaseLite = (LiteDatabase)memoryDataBase;
            }
            else
            {
                using (var db = new LiteDatabase(@ConfigFile.Instance.localDbPath))
                {
                    dataBaseLite = _cache.Set(strCacheKey, db, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(cacheTimeoutMinutes)));
                }
            }
        }

        public virtual bool SaveData<T>(object obj, string dataTableName)
        {
            dataBaseLite.GetCollection<T>(dataTableName).Insert((T)obj);
            _cache.Set(defaultDataBase, dataBaseLite, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(cacheTimeoutMinutes)));

            return true;
        }


        public bool SaveDate(string data, string cacheKey = "")
        {
            throw new NotImplementedException();
        }
    }
}
