using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using westgateprojectService.DataObjects;

namespace westgateprojectService.Controllers
{
    [MobileAppController]
    public class GetBuildingInformationController : ApiController
    {
        public IDictionary<string, string> Get()
        {
            TableQuery<BuildingInformation> rangeQuery = new TableQuery<BuildingInformation>().Where(
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, "층별안내"));

            IDictionary<string, string> buildingInfo = new Dictionary<string, string>();
            foreach (BuildingInformation entity in Startup.table.ExecuteQuery(rangeQuery))
            {
                buildingInfo.Add(entity.PartitionKey + ":지하1층", entity.지하1층);
                buildingInfo.Add(entity.PartitionKey + ":지상1층", entity.지상1층);
                buildingInfo.Add(entity.PartitionKey + ":지상2층", entity.지상2층);
                buildingInfo.Add(entity.PartitionKey + ":지상3층", entity.지상3층);
                buildingInfo.Add(entity.PartitionKey + ":지상4층", entity.지상4층);
            }
            return buildingInfo;
        }
        public IDictionary<string, string> Get(string buildingName)
        {

            TableOperation retrieveOperation = TableOperation.Retrieve<BuildingInformation>(buildingName, "층별안내");
            TableResult retrievedResult = Startup.table.Execute(retrieveOperation);

            if (retrievedResult.Result != null)
            {
                IDictionary<string, string> buildingInfo = new Dictionary<string, string>();
                var result = ((BuildingInformation)retrievedResult.Result);
                buildingInfo.Add("지하1층", result.지하1층);
                buildingInfo.Add("지상1층", result.지상1층);
                buildingInfo.Add("지상2층", result.지상2층);
                buildingInfo.Add("지상3층", result.지상3층);
                buildingInfo.Add("지상4층", result.지상4층);
                return buildingInfo;
            }
            else
                return null;
        }        
    }
}
