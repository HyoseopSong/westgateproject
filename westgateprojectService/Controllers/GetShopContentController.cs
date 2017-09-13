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
    public class GetShopContentController : ApiController
    {
        public ContentsEntity Get(string shopOwner, string blobName, string userID)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable tableOwner = tableClient.GetTableReference(shopOwner.Split('@')[0]);

            TableOperation retrieveOperation = TableOperation.Retrieve<ContentsEntity>(shopOwner, blobName);
            TableResult retrievedResult = tableOwner.Execute(retrieveOperation);
            ContentsEntity contentsItem = (ContentsEntity)retrievedResult.Result;
            
            if (contentsItem.LikeMember.IndexOf(userID) >= 0)
            {
                contentsItem.LikeMember = "True";
            }
            else
            {
                contentsItem.LikeMember = "False";
            }
            

            return contentsItem;

        }
    }
}