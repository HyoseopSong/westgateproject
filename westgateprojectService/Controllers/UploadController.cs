using Microsoft.Azure;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using westgateprojectService.DataObjects;

namespace westgateprojectService.Controllers
{
    [MobileAppController]
    public class UploadController : ApiController
    {
        public List<MyEntity> Get(string id)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            var containerName = id.Split('@');
            CloudTable table = tableClient.GetTableReference(containerName[0]);
            table.CreateIfNotExistsAsync();
            TableQuery<ContentsEntity> query = new TableQuery<ContentsEntity>();

            List<MyEntity> myActivity = new List<MyEntity>();
            // Print the fields for each customer.
            foreach (ContentsEntity entity in table.ExecuteQuery(query))
            {
                MyEntity result = new MyEntity(entity.RowKey, entity.ShopName, entity.Context);
                
                myActivity.Add(result);
            }
            return myActivity;
        }

        public void Post(string content, string id, string blobName, string shopName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            var containerName = id.Split('@');
            CloudTable table = tableClient.GetTableReference(containerName[0]);
            table.CreateIfNotExists();
            ContentsEntity contents = new ContentsEntity(id, blobName, shopName, content);
            TableOperation insertOperation = TableOperation.Insert(contents);
            TableResult result = table.Execute(insertOperation);

            CloudTable recentTable = tableClient.GetTableReference("Recent");
            RecentEntity recentContents = new RecentEntity(id, blobName, shopName, content);
            TableOperation recentOperation = TableOperation.Insert(recentContents);
            recentTable.CreateIfNotExists();
            TableResult recentResult = recentTable.Execute(recentOperation);
        }

        public void Delete(string id, string blobName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            var containerName = id.Split('@');
            CloudTable table = tableClient.GetTableReference(containerName[0]);
            TableOperation retrieveOperation = TableOperation.Retrieve<ContentsEntity>(id, blobName);
            TableResult retrievedResult = table.Execute(retrieveOperation);
            ContentsEntity deleteEntity = (ContentsEntity)retrievedResult.Result;
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                table.Execute(deleteOperation);
            }


            CloudTable recentTable = tableClient.GetTableReference("Recent");
            TableQuery<RecentEntity> rangeQuery = new TableQuery<RecentEntity>().Where(
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, blobName));
            foreach (RecentEntity entity in recentTable.ExecuteQuery(rangeQuery))
            {
                if (entity.ID == id)
                {

                    TableOperation recentDeleteOperation = TableOperation.Delete(entity);
                    recentTable.Execute(recentDeleteOperation);
                }
            }
        }
        
    }
}