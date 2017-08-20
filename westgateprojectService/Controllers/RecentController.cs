using Microsoft.Azure;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using westgateprojectService.DataObjects;

namespace westgateprojectService.Controllers
{
    [MobileAppController]
    public class RecentController : ApiController
    {
        // GET: Recent
        public List<ContentsEntity> Get()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Recent");
            TableQuery<ContentsEntity> query = new TableQuery<ContentsEntity>();

            List<ContentsEntity> myActivity = new List<ContentsEntity>();
            // Print the fields for each customer.
            foreach (ContentsEntity entity in table.ExecuteQuery(query))
            {
                myActivity.Add(entity);
            }
            return myActivity;
        }


        public IDictionary<string, string> Get(string id, string shopName)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("UserInformation");
            TableQuery<UserInfoEntity> rangeQuery = new TableQuery<UserInfoEntity>().Where(
                    TableQuery.GenerateFilterCondition("ShopName", QueryComparisons.Equal, shopName));

            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (UserInfoEntity entity in table.ExecuteQuery(rangeQuery))
            {
                if (entity.PartitionKey == id)
                {
                    var resultString = entity.RowKey.Split(':');
                    result.Add("Building", resultString[0]);
                    result.Add("Floor", resultString[1]);
                    result.Add("Location", resultString[2]);
                    return result;
                }
            }
            return null;
        }
    }
}