using Newtonsoft.Json;

namespace SephoraSearchEngine.Dto
{
    public class AvailabilityDto
    {
        [JsonProperty("stores")]
        public StoreDto[] Stores { get; set; }
    }

    public class StoreDto
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
