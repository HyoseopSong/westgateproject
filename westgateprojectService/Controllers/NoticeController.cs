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
    public class NoticeController : ApiController
    {
        public IDictionary<string, string> Get()
        {

            TableOperation retrieveOperation = TableOperation.Retrieve<NoticeInformation>("앱정보", "공지사항");
            TableResult retrievedResult = Startup.table.Execute(retrieveOperation);

            IDictionary<string, string> result = new Dictionary<string, string>();
            if(retrievedResult.Result != null)
            {
                result.Add("개발현황", (((NoticeInformation)retrievedResult.Result).개발현황));
                return result;
            }
            else
            {
                return null;
            }
            
        }
    }
}
