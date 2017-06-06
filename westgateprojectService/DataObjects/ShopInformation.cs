using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace westgateprojectService.DataObjects
{
    public class ShopInformation : TableEntity
    {
        public ShopInformation(string Category, string shopLocation)
        {
            PartitionKey = Category;
            RowKey = shopLocation;
        }
        public ShopInformation() { }

        public string 내용 { get; set; }

    }
}