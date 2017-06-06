using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using westgateprojectService.DataObjects;

namespace westgateprojectService.Controllers
{
    // Use the MobileAppController attribute for each ApiController you want to use  
    // from your mobile clients 
    [MobileAppController]
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<ShopInformation>("2지구", "1층");
            TableResult retrievedResult = Startup.table.Execute(retrieveOperation);

            if (retrievedResult.Result != null)
                return ((ShopInformation)retrievedResult.Result).내용;
            else
                return null;
        }

        // POST api/values
        public string Post()
        {
            return "Here is your backend!!";
        }
    }
}
