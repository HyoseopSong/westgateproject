﻿using Microsoft.Azure;
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

            TableQuery<UserInfoEntity> queryID = new TableQuery<UserInfoEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id));

            IDictionary<string, UserInfoEntity> result = new Dictionary<string, UserInfoEntity>();
            foreach (UserInfoEntity entity in table.ExecuteQuery(queryID))
            {
                result.Add(entity.ShopName, entity);
            }

            return result;
        }

        public IDictionary<string, string> Get(string id, string building, string floor, string location)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("UserInformation");

            string shopLocation = building + ":" + floor + ":" + location;

            TableQuery<UserInfoEntity> rangeQuery = new TableQuery<UserInfoEntity>().Where(
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, shopLocation));
            
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (UserInfoEntity entity in table.ExecuteQuery(rangeQuery))
            {
                if(entity.PartitionKey == id)
                {
                    result.Add("ShopName", entity.ShopName);
                    result.Add("PhoneNumber", entity.PhoneNumber);
                    result.Add("AddInfo", entity.AddInfo);
                    return result;
                }                
            }
            return null;
        }

        public void Post(string id, string name, string building, string floor, string location, string number, string addInfo)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            string shopLocation = building + ":" + floor + ":" + location;

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("UserInformation");
            table.CreateIfNotExists();
            UserInfoEntity contents = new UserInfoEntity(id, shopLocation, name, number, addInfo);
            TableOperation insertOperation = TableOperation.InsertOrReplace(contents);
            TableResult result = table.Execute(insertOperation);
        }

        public void Delete(string id, string shopLocation)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("UserInformation");

            // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<ContentsEntity>(id, shopLocation);

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