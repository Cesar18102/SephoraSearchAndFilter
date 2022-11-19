using Newtonsoft.Json;
using System.Collections.Generic;

namespace SephoraSearchEngine.Models
{
    public sealed class Category
    {
        [JsonProperty("categoryId")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string Name { get; set; }

        [JsonProperty("hasChildCategories")]
        public bool HasChildCategories { get; set; }

        public Category[] SubCategories { get; set; }
        public Product[] Products { get; set; }
    }
}
