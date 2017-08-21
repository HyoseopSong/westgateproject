using Microsoft.WindowsAzure.Storage.Table;

namespace westgateprojectService.DataObjects
{
    public class ShopInfoEntity : TableEntity
    {
        public ShopInfoEntity(string floor, string name, string id, string location)
        {
            PartitionKey = floor;
            RowKey = location;
            OwnerID = id;
            ShopName = name;
        }
        public ShopInfoEntity() { }

        public string OwnerID { get; set; }
        public string ShopName { get; set; }
        public bool OnService { get; set; }


    }
}