﻿using Microsoft.Azure;
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
        public IDictionary<string, string> Get(string id)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            var containerName = id.Split('@');
            CloudTable table = tableClient.GetTableReference(containerName[0]);
            TableQuery<ContentsEntity> query = new TableQuery<ContentsEntity>();

            IDictionary<string, string> myActivity = new Dictionary<string, string>();
            // Print the fields for each customer.
            foreach (ContentsEntity entity in table.ExecuteQuery(query))
            {
                myActivity.Add(entity.RowKey, entity.Context);
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
            RecentEntity recentContents = new RecentEntity(id, shopName, blobName, content);
            TableOperation recentOperation = TableOperation.Insert(recentContents);
            table.CreateIfNotExists();
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

            CloudTable recentTable = tableClient.GetTableReference("Recent");
            TableOperation recentRetrieveOperation = TableOperation.Retrieve<RecentEntity>(id, blobName);
            TableResult recentRetrievedResult = recentTable.Execute(recentRetrieveOperation);
            RecentEntity recentDeleteEntity = (RecentEntity)recentRetrievedResult.Result;

            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                table.Execute(deleteOperation);

                TableOperation recentDeleteOperation = TableOperation.Delete(recentDeleteEntity);
                recentTable.Execute(recentDeleteOperation);

            }
            else
            {

            }
        }
        
    }
}