using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace westgateprojectService.DataObjects
{
    public class RecentEntity : TableEntity
    {
        public RecentEntity(string id, string blobName, string shopName, string text)
        {
            PartitionKey = blobName.Split('.')[0];
            RowKey = blobName;
            ShopName = shopName;
            Context = text;
            ID = id;
        }

        public RecentEntity() { }

        public string ShopName { get; set; }
        public string Context { get; set; }
        public string ID { get; set; }
    }
}