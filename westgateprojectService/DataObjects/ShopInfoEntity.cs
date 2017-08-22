using Microsoft.WindowsAzure.Storage.Table;

namespace westgateprojectService.DataObjects
{
    public class ShopInfoEntity : TableEntity
    {
        public ShopInfoEntity(string floor, string location, string id, string name, bool onService)
        {
            PartitionKey = floor;
            RowKey = location;
            OwnerID = id;
            ShopName = name;
            OnService = onService;
        }
        public ShopInfoEntity() { }

        public string OwnerID { get; set; }
        public string ShopName { get; set; }
        public bool OnService { get; set; }


    }
}