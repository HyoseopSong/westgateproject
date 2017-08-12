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
    public class UserInformationController : ApiController
    {
        //public List<UserInfoEntity> Get(string id)
        //{
        //    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

        //    CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
        //    CloudTable table = tableClient.GetTableReference("UserInformation");
        //    // Construct the query operation for all customer entities where PartitionKey="Smith".

        //    TableQuery<UserInfoEntity> queryID = new TableQuery<UserInfoEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id));

        //    List<UserInfoEntity> result = new List<UserInfoEntity>();
        //    foreach (UserInfoEntity entity in table.ExecuteQuery(queryID))
        //    {
        //        UserInfoEntity resultEntity = new UserInfoEntity()
        //        {
        //            PartitionKey = entity.PartitionKey,
        //            RowKey = entity.RowKey,
        //            ShopBuilding = entity.ShopBuilding,
        //            ShopFloor = entity.ShopFloor,
        //            ShopLocation = entity.ShopLocation,
        //            PhoneNumber = entity.PhoneNumber
        //        };
        //        result.Add(resultEntity);
        //    }

        //    return result;
        //}
        public IDictionary<string, UserInfoEntity> Get(string id)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("UserInformation");
            // Construct the query operation for all customer entities where PartitionKey="Smith".

            TableQuery<UserInfoEntity> queryID = new TableQuery<UserInfoEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id));

            IDictionary<string, UserInfoEntity> result = new Dictionary<string, UserInfoEntity>();
            foreach (UserInfoEntity entity in table.ExecuteQuery(queryID))
            {
                result.Add(DateTime.Now.ToFileTime().ToString(), entity);
            }

            return result;
        }

        public void Post(string id, string name, string building, string floor, string location, string number)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));


            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("UserInformation");
            table.CreateIfNotExists();
            UserInfoEntity contents = new UserInfoEntity(id, name, building, floor, location, number);
            TableOperation insertOperation = TableOperation.InsertOrReplace(contents);
            TableResult result = table.Execute(insertOperation);
        }
    }
}