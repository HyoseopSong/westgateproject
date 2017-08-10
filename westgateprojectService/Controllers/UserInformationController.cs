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
        public void Post(string id, string shopName, string phoneNumber)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));


            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("UserInformation");
            table.CreateIfNotExists();
            UserInfoEntity contents = new UserInfoEntity(id, shopName, phoneNumber);
            TableOperation insertOperation = TableOperation.Insert(contents);
            TableResult result = table.Execute(insertOperation);
        }
    }
}