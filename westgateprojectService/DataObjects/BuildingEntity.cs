using Microsoft.WindowsAzure.Storage.Table;

namespace westgateprojectService.DataObjects
{
    public class BuildingEntity : TableEntity
    {
        public BuildingEntity(string floor, string location, string ownerID, string shopName, bool onService)
        {
            PartitionKey = floor;
            RowKey = location;
            OwnerID = ownerID;
            ShopName = shopName;
            OnService = onService;
        }

        public BuildingEntity() { }

        public string OwnerID { get; set; }
        public string ShopName { get; set; }
        public bool OnService { get; set; }

    }
}