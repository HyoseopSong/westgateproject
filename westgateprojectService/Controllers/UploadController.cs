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
        public IDictionary<string, string> Get(string id)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            var containerName = id.Split('@');
            CloudTable table = tableClient.GetTableReference(containerName[0]);
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<ContentsEntity> query = new TableQuery<ContentsEntity>();

            IDictionary<string, string> myActivity = new Dictionary<string, string>();
            // Print the fields for each customer.
            foreach (ContentsEntity entity in table.ExecuteQuery(query))
            {
                myActivity.Add(entity.RowKey, entity.Context);
            }
            return myActivity;
        }

        public void Post(string content, string id, string blobName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            var containerName = id.Split('@');
            CloudTable table = tableClient.GetTableReference(containerName[0]);
            table.CreateIfNotExists();
            ContentsEntity contents = new ContentsEntity(id, blobName, content);
            TableOperation insertOperation = TableOperation.Insert(contents);
            TableResult result = table.Execute(insertOperation);
        }

        public void Delete(string id, string blobName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            var containerName = id.Split('@');
            CloudTable table = tableClient.GetTableReference(containerName[0]);

            // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<ContentsEntity>(id, blobName);

            // Execute the operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Assign the result to a CustomerEntity.
            ContentsEntity deleteEntity = (ContentsEntity)retrievedResult.Result;

            // Create the Delete TableOperation.
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                // Execute the operation.
                table.Execute(deleteOperation);
                
            }
            else
            {

            }
        }
        
    }
}