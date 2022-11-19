using Newtonsoft.Json;

namespace SephoraSearchEngine.Models
{
    public class Sku
    {
        [JsonProperty("skuId")]
        public string Id { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}
