using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace westgateprojectService.DataObjects
{
    public class ShopInfoEntity : TableEntity
    {
        public ShopInfoEntity(string floor, string name, string id, string location)
        {
            PartitionKey = floor;
            RowKey = location;
            Owner = id;
            Name = name;
        }
        public ShopInfoEntity() { }

        public string Owner { get; set; }
        public string Name { get; set; }

        
    }
}