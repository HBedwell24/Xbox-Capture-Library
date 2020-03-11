using Newtonsoft.Json;

namespace XboxGameClipLibrary.Models.Profile
{
    public class Profile
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("gamerTag")]
        public string GamerTag { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("userXuid")]
        public long UserXuid { get; set; }
    }
}
