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
    public class LikeContentsController : ApiController
    {
        public List<LikeEntity> Get(string userId)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable tableOwner = tableClient.GetTableReference(userId.Split('@')[0]);

            TableQuery<LikeEntity> rangeQuery = new TableQuery<LikeEntity>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual, userId));
            
            List<LikeEntity> shopContents = new List<LikeEntity>();
            foreach (LikeEntity entity in tableOwner.ExecuteQuery(rangeQuery))
            {
                shopContents.Add(entity);
            }

            return shopContents;

        }

        public void Post(string shopOwner, string blobName, string likeMember)
        {
            var shopOwnerId = shopOwner.Split('@')[0];
            if(shopOwnerId == likeMember)
            {
                return;
            }
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable tableOwner = tableClient.GetTableReference(shopOwnerId);
            CloudTable table = tableClient.GetTableReference(likeMember);
            LikeEntity contents = new LikeEntity(shopOwner, blobName);
            TableOperation insertOperation = TableOperation.Insert(contents);
            TableResult result = table.Execute(insertOperation);
        }

        public void Delete(string shopOwner, string blobName, string likeMember)
        {
            var shopOwnerId = shopOwner.Split('@')[0];
            if (shopOwnerId == likeMember)
            {
                return;
            }
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            
            CloudTable table = tableClient.GetTableReference(likeMember);
                                   
            TableOperation retrieveOperation = TableOperation.Retrieve<LikeEntity>(shopOwner, blobName);
            TableResult retrievedResult = table.Execute(retrieveOperation);
            LikeEntity deleteEntity = (LikeEntity)retrievedResult.Result;
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                table.Execute(deleteOperation);
            }           
        }
    }
}