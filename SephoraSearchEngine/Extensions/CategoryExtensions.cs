using SephoraSearchEngine.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SephoraSearchEngine.Extensions
{
    public static class CategoryExtensions
    {
        public static IEnumerable<TreeNode> ToTreeNodes(this IList<Category> categories)
        {
            return categories.Select(category =>
            {
                var subNodes = category.SubCategories?.ToTreeNodes().ToArray() ?? new TreeNode[] { };
                return new TreeNode(category.Name, subNodes) { Tag = category };
            });
        }
    }
}
