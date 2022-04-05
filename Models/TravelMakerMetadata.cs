namespace TravelMaker.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    [MetadataType(typeof(TravelMakerMetadata))]
    public partial class Travel
    {
        private class TravelMakerMetadata
        {
            [JsonIgnore]
            public virtual Account Account { get; set; }
            [JsonIgnore]
            public virtual ICollection<Comment> Comment { get; set; }
        }
    }
}
