using Microsoft.Azure;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using westgateprojectService.DataObjects;

namespace westgateprojectService.Controllers
{
    [MobileAppController]
    public class SampleController:ApiController
    {
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
       

        public IDictionary<string, string> Post(string temp, string temp2)
        {
            IDictionary<string, string> postDictionary = new Dictionary<string, string>
            {
                { "First", temp },
                { "Second", temp2 }
            };
            return postDictionary;
        }
    }
}