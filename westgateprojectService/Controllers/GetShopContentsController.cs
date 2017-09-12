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
    public class GetShopContentsController : ApiController
    {
        public List<ContentsEntity> Get(string shopOwner, string shopName, string userID)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable tableOwner = tableClient.GetTableReference(shopOwner);

            TableQuery<ContentsEntity> rangeQuery = new TableQuery<ContentsEntity>().Where(
                    TableQuery.GenerateFilterCondition("ShopName", QueryComparisons.Equal, shopName));

            TableQuery<ContentsEntity> query = new TableQuery<ContentsEntity>();

            List<ContentsEntity> shopContents = new List<ContentsEntity>();
            foreach (ContentsEntity entity in tableOwner.ExecuteQuery(rangeQuery))
            {
                if (entity.LikeMember.IndexOf(userID) >= 0)
                {
                    entity.LikeMember = "True";
                }
                else
                {
                    entity.LikeMember = "False";
                }

                shopContents.Add(entity);
            }
            
            return shopContents;

        }

        public void Put(string shopOwner, string blobName, string likeMember, string change)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable tableOwner = tableClient.GetTableReference(shopOwner.Split('@')[0]);

            // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<ContentsEntity>(shopOwner, blobName);

            // Execute the operation.
            TableResult retrievedResult = tableOwner.Execute(retrieveOperation);

            // Assign the result to a CustomerEntity.
            ContentsEntity updateEntity = (ContentsEntity)retrievedResult.Result;

            switch(change)
            {
                case "up":
                    updateEntity.LikeMember += likeMember + ":";
                    updateEntity.Like++;
                    break;
                case "down":
                    updateEntity.LikeMember = updateEntity.LikeMember.Replace(likeMember + ":", "");
                    updateEntity.Like--;
                    break;
            }
            
            TableOperation updateOperation = TableOperation.Replace(updateEntity);
            tableOwner.Execute(updateOperation);
            
        }
    }
}