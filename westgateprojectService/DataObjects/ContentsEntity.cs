using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace westgateprojectService.DataObjects
{
    public class ContentsEntity : TableEntity
    {
        public ContentsEntity(string patKey, string blobName, string text)
        {
            PartitionKey = patKey;
            RowKey = blobName;
            Text = text;
        }

        public ContentsEntity() { }

        public string Text { get; set; }
        public string blobName { get; set; }
    }
}