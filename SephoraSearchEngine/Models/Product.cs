using Newtonsoft.Json;

namespace SephoraSearchEngine.Models
{
    public class Product
    {
        [JsonProperty("productId")]
        public string Id { get; set; }

        [JsonProperty("brandName")]
        public string Brand { get; set; }
        
        [JsonProperty("displayName")]
        public string Name { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("reviews")]
        public int Reviews { get; set; }

        [JsonProperty("currentSku")]
        public Sku CurrentSku { get; set; }

        public string Ingredients { get; set; }
        public string IncludedBadWords { get; set; }
    }
}
