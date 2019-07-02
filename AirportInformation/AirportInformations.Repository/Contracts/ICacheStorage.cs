
using System.Collections.Generic;
using AirportInformations.Repository.Models;

namespace AirportInformations.Repository.Contracts
{

    public interface ICacheStorage
    {
        bool InsertIntoDataTable(string IntodataTableName, object obj);

        bool SaveData<T>(object obj, string dataTableName);

        Airport FindOneDataByFieldValue(string FromdataTableName, string fieldName, string fieldValue);

        IEnumerable<T> FindAllDataFromDataTable<T>(string FromdataTableName);

        void InitDataBase(string cacheKey);

        void ClearData(string cacheKey);
    }  
}
