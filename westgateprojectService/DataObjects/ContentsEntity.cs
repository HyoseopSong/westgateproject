using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace westgateprojectService.DataObjects
{
    public class ContentsEntity : TableEntity
    {
        public ContentsEntity(string id, string blobName, string text)
        {
            PartitionKey = id;
            RowKey = blobName;
            Context = text;
        }

        public ContentsEntity() { }


        public string Context { get; set; }
    }
}