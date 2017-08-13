using Microsoft.WindowsAzure.Storage.Table;

namespace westgateprojectService.DataObjects
{
    public class RecentEntity : TableEntity
    {
        public RecentEntity(string id, string shopName, string blobName, string text)
        {
            PartitionKey = id;
            RowKey = blobName;
            ShopName = shopName;
            Text = text;
        }

        public RecentEntity() { }

        public string ShopName { get; set; }
        public string Text { get; set; }

    }
}