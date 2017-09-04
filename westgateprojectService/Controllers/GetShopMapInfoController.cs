using Microsoft.Azure;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using westgateprojectService.DataObjects;

namespace westgateprojectService.Controllers
{
    [MobileAppController]
    public class GetShopMapInfoController : ApiController
    {
        public List<ShopMapInfoEntity> Get(string buildingFloor)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("ShopMapInfo");

            TableQuery<ShopMapInfoEntity> queryID = new TableQuery<ShopMapInfoEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, buildingFloor));

            List<ShopMapInfoEntity> result = new List<ShopMapInfoEntity>();
            foreach (ShopMapInfoEntity entity in table.ExecuteQuery(queryID))
            {
                result.Add(entity);
            }

            return result;
        }
    }
}