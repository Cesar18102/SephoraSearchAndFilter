using Newtonsoft.Json;
using SephoraSearchEngine.Models;

namespace SephoraSearchEngine.Dto
{
    public class ChildCategoriesDto
    {
        [JsonProperty("childCategories")]
        public Category[] ChildCategories { get; set; }
    }
}
