using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace westgateprojectService.DataObjects
{
    public class ShopMapInfoEntity : TableEntity
    {
        public ShopMapInfoEntity()
        {

        }

        public ShopMapInfoEntity(string BuildingFloor, string ShopName)
        {
            PartitionKey = BuildingFloor;
            RowKey = ShopName; // public string Text { get; set; }
        }


        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public int FontSize { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}