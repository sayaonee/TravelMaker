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
    
    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            this.Comment1 = new HashSet<Comment>();
        }
    
        public int cm_Id { get; set; }
        public Nullable<int> target_Id { get; set; }
        public Nullable<int> user_Id { get; set; }
        public string cmRate { get; set; }
        public Nullable<int> cmFront { get; set; }
        public string cmGB { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Attraction Attraction { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment1 { get; set; }
        public virtual Comment Comment2 { get; set; }
        public virtual Travel Travel { get; set; }
    }
}