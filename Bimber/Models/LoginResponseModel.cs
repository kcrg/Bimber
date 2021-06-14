using Newtonsoft.Json;

namespace Bimber.Models
{
    public class LoginResponseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("error")]
        public string? Error { get; set; }
    }
}
