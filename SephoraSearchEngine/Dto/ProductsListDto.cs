using Newtonsoft.Json;
using SephoraSearchEngine.Models;

namespace SephoraSearchEngine.Dto
{
    public class ProductsListDto
    {
        [JsonProperty("products")]
        public Product[] Products { get; set; }

        [JsonProperty("totalProducts")]
        public int TotalProduct { get; set; }
    }
}
