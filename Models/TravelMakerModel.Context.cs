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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TravelMakerEntities : DbContext
    {
        public TravelMakerEntities()
            : base("name=TravelMakerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Attraction> Attraction { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Travel> Travel { get; set; }
        public virtual DbSet<MapCenter> MapCenter { get; set; }
    }
}
