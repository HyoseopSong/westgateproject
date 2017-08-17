using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace westgateprojectService.DataObjects
{
    public class UserInfoEntity : TableEntity
    {
        public UserInfoEntity(string id, string shopLocation, string shopName, string phoneNumber, string addInfo, string payment)
        {
            PartitionKey = id;
            RowKey = shopLocation;

            ShopName = shopName;
            PhoneNumber = phoneNumber;
            Paid = false;
            AddInfo = addInfo;
            Payment = payment;
        }

        public UserInfoEntity() { }

        
        public string ShopName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Paid { get; set; }
        public string AddInfo { get; set; }
        public string Payment { get; set; }
    }

}