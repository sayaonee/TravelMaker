//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelMaker.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int order_Id { get; set; }
        public Nullable<int> user_Id { get; set; }
        public Nullable<int> travel_Id { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public string orderMessage { get; set; }
        public Nullable<int> orderCount { get; set; }
        public Nullable<decimal> orderPrice { get; set; }
        public Nullable<double> orderCost { get; set; }
        public string orderType { get; set; }
    }
}
