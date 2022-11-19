using Newtonsoft.Json;
using SephoraSearchEngine.Models;

namespace SephoraSearchEngine.Dto
{
    public class RootCategoriesDto
    {
        [JsonProperty("rootCategories")]
        public Category[] RootCategories { get; set; }
    }
}
