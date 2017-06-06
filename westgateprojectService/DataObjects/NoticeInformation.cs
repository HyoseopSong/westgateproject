using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace westgateprojectService.DataObjects
{
    public class NoticeInformation : TableEntity
    {
        public NoticeInformation(string appInfo, string notice)
        {
            PartitionKey = appInfo;
            RowKey = notice;
        }
        public NoticeInformation() { }
        
        public string 개발현황 { get; set; }

    }
}