using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace westgateprojectService.DataObjects
{
    public class BuildingInformation : TableEntity
    {
        public BuildingInformation(string building, string category)
        {
            PartitionKey = building;
            RowKey = category;
        }
        public BuildingInformation()
        { }
        
        public string 지하1층 { get; set; }
        public string 지상1층 { get; set; }
        public string 지상2층 { get; set; }
        public string 지상3층 { get; set; }
        public string 지상4층 { get; set; }


    }
}