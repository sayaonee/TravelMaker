namespace TravelMaker.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    [MetadataType(typeof(AccountMakerMetadata))]
    public partial class Account
    {
        private class AccountMakerMetadata
        {
            [JsonIgnore]
            public virtual ICollection<Attraction> Attraction { get; set; }
            [JsonIgnore]
            public virtual ICollection<Comment> Comment { get; set; }
            [JsonIgnore]
            public virtual ICollection<Message> Message { get; set; }
            [JsonIgnore]
            public virtual ICollection<Travel> Travel { get; set; }
        }
    }
}
