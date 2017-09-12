using Microsoft.Azure;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using westgateprojectService.DataObjects;

namespace westgateprojectService.Controllers
{
    [MobileAppController]
    public class GetShopInformationController : ApiController
    {

        public IDictionary<string, string> Get(string building, string floor, string location)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(building);
            // Construct the query operation for all customer entities where PartitionKey="Smith".

            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<ShopInfoEntity>(floor, location);

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            ShopInfoEntity retrievedEntity = (ShopInfoEntity)retrievedResult.Result;
            // Print the phone number of the result.
            if (retrievedEntity != null)
            {
                CloudTable tableUserInfo = tableClient.GetTableReference("UserInformation");
                TableOperation retrieveUserInfoOperation = TableOperation.Retrieve<UserInfoEntity>(retrievedEntity.OwnerID, building + ":" + floor + ":" + location);
                TableResult retrievedUserInfoResult = tableUserInfo.Execute(retrieveUserInfoOperation);
                UserInfoEntity retrievedUserInfoEntity = (UserInfoEntity)retrievedUserInfoResult.Result;

                IDictionary<string, string> myActivity = new Dictionary<string, string>
                {
                    { "ShopName", retrievedEntity.ShopName },
                    { "ShopOwner", retrievedEntity.OwnerID },
                    { "PhoneNumber", retrievedUserInfoEntity.PhoneNumber }
                };
                if (!retrievedEntity.OnService)
                {
                    myActivity.Add("NotOnService", "NotAvailable");
                }

                return myActivity;
                
            }
            else
            {
                return null;
            }
            
        }
    }
}
