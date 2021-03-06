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
    
    public partial class Attraction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attraction()
        {
            this.Comment = new HashSet<Comment>();
            this.Message = new HashSet<Message>();
        }
    
        public int attr_Id { get; set; }
        public string position { get; set; }
        public Nullable<int> attractionOwner { get; set; }
        public string attractionCover { get; set; }
        public string attractionImages { get; set; }
        public string attractionPhone { get; set; }
        public string attractionAddress { get; set; }
        public string attractionMenu { get; set; }
        public string attractionCity { get; set; }
        public string attractionType { get; set; }
        public string attractionInfo { get; set; }
        public string attractionComment { get; set; }
        public string attractionRate { get; set; }
        public Nullable<decimal> attractionPrice { get; set; }
        public string travelReferral { get; set; }
        public Nullable<System.DateTime> lastUpdate { get; set; }
        public string attrName { get; set; }
        public string attractionDistrict { get; set; }
        public Nullable<int> attractionCount { get; set; }
    
        public virtual Account Account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Message { get; set; }
    }
}
