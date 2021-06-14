using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bimber.Models
{
    public class ResponseModel
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("data")]
        public IList<PersonModel>? Data { get; set; }
    }
}
