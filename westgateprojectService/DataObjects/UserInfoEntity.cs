using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace westgateprojectService.DataObjects
{
    public class UserInfoEntity : TableEntity
    {
        public UserInfoEntity(string id, string shopName, string phoneNumber)
        {
            PartitionKey = id;
            RowKey = shopName;
            PhoneNumber = phoneNumber;
        }

        public UserInfoEntity() { }

        public string PhoneNumber { get; set; }
    }

}