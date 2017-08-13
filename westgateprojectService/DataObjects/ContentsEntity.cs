﻿using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace westgateprojectService.DataObjects
{
    public class ContentsEntity : TableEntity
    {
        public ContentsEntity(string id, string blobName, string shopName, string text)
        {
            PartitionKey = id;
            RowKey = blobName;
            ShopName = shopName;
            Context = text;
        }

        public ContentsEntity() { }

        public string ShopName { get; set; }
        public string Context { get; set; }
    }
}