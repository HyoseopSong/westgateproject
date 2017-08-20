using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace westgateprojectService.DataObjects
{
    public class MyEntity : TableEntity
    {
        public MyEntity() { }

        public MyEntity(string blobName, string shopName, string text)
        {
            PartitionKey = blobName;
            RowKey = shopName;
            Text = text;
        }

        public string Text { get; set; }
    }
}