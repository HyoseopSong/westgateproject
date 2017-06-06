using Microsoft.Azure.Mobile.Server.Config;
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
        public IDictionary<string, string> Get()
        {
            TableQuery<ShopInformation> rangeQuery = new TableQuery<ShopInformation>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "매장정보"));

            IDictionary<string, string> shopInfo = new Dictionary<string, string>();
            foreach (ShopInformation entity in Startup.table.ExecuteQuery(rangeQuery))
            {
                shopInfo.Add(entity.RowKey, entity.내용);
            }
            return shopInfo;
        }
    }
}
