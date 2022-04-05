namespace TravelMaker.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    [MetadataType(typeof(AttractionMakerMetadata))]
    public partial class Attraction
    {
        private class AttractionMakerMetadata
        {
            [JsonIgnore]
            public virtual Account Account { get; set; }
            [JsonIgnore]
            public virtual ICollection<Comment> Comment { get; set; }
            [JsonIgnore]
            public virtual ICollection<Message> Message { get; set; }
        }
    }
}
