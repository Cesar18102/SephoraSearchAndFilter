using SephoraSearchEngine.Extensions;
using SephoraSearchEngine.Models;
using SephoraSearchEngine.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SephoraSearchEngine
{
    public partial class Form1 : Form
    {
        private List<string> _apiKeys = new List<string>();
        private string[] _badWords = new string[] { };
        private readonly SephoraClient _sephoraClient = new SephoraClient();
        private string[] CrueltyFreeBrands;

        public Form1()
        {
            InitializeComponent();

            CrueltyFreeBrands = GetTextFromResourceFile("CrueltyFree.txt").Replace("\r", "").Split('\n')
                .Select(brand => brand.ToLower().Replace(" ", "")).ToArray();

            _sephoraClient.OnApiKeyBroken += _sephoraClient_OnApiKeyBroken;
            _sephoraClient.OnCategoryLoaded += _sephoraClient_OnCategoryLoaded;
            _sephoraClient.OnProductLoaded += _sephoraClient_OnProductLoaded;
        }

        private string GetTextFromResourceFile(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(nameof(SephoraSearchEngine) + "." + fileName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private void _sephoraClient_OnProductLoaded(object sender, Product e)
        {
            (ProductsGrid.DataSource as BindingList<Product>).Add(e);
            CheckProduct(e);

            Log($"Product {e.Name} is loaded");
        }

        private void _sephoraClient_OnCategoryLoaded(object sender, Category e)
        {
            Log($"Subcategories for {e.Name} are loaded");
        }

        private void Log(string text)
        {
            var dateTime = DateTime.Now;
            var logText = $"{dateTime.ToShortDateString()} {dateTime.ToLongTimeString()}: {text}\n";

            LogWindow.AppendText(logText);
            LogWindow.ScrollToCaret();
        }

        private void _sephoraClient_OnApiKeyBroken(object sender, EventArgs e)
        {
            _apiKeys.Remove(_sephoraClient.ApiKey);
            _sephoraClient.ApiKey = _apiKeys.FirstOrDefault();
        }

        private void ApiKeysTextBox_TextChanged(object sender, EventArgs e)
        {
            _apiKeys = ApiKeysTextBox.Text.Replace("\r", "").Split('\n').ToList();
        }

        private async void LoadCategoriesButton_Click(object sender, EventArgs e)
        {
            var rootCategories = await _sephoraClient.GetRootCategories();

            foreach(var rootCategory in rootCategories)
            {
                await _sephoraClient.LoadSubCategories(rootCategory);
            }
            
            CategoryTree.Nodes.Clear();
            CategoryTree.Nodes.AddRange(rootCategories.ToTreeNodes().ToArray());
        }

        private async void CategoryTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var category = e.Node.Tag as Category;

            foreach(var subCategory in category.SubCategories)
            {
                await _sephoraClient.LoadSubCategories(subCategory);

                var subCategoryNode = e.Node.Nodes.OfType<TreeNode>().FirstOrDefault(node => node.Tag == subCategory);

                subCategoryNode.Nodes.Clear();
                subCategoryNode.Nodes.AddRange(subCategory.SubCategories.ToTreeNodes().ToArray());
            }
        }

        private async void CategoryTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var category = e.Node.Tag as Category;
            ProductsGrid.DataSource = new BindingList<Product>();

            if(category.Products == null)
            {
                await _sephoraClient.LoadProducts(category);
            }
        }

        private void BadWordsTextBox_TextChanged(object sender, EventArgs e)
        {
            _badWords = BadWordsTextBox.Text.Split(';').Where(word => !string.IsNullOrEmpty(word)).ToArray();

            foreach (var row in ProductsGrid.Rows)
            {
                CheckProduct((row as DataGridViewRow).DataBoundItem as Product);
            }
        }

        private void CheckProduct(Product product)
        {
            var name = product.Name.ToLower();
            var ingredients = product.Ingredients?.ToLower();

            var productBadWords = new List<string>();
            foreach(var badWord in _badWords)
            {
                if(name.Contains(badWord) || (ingredients?.Contains(badWord) ?? false))
                {
                    productBadWords.Add(badWord);
                }
            }
            if(!CrueltyFreeBrands.Contains(product.Brand.ToLower().Replace(" ", "")))
            {
                productBadWords.Add("Animal Testing");
            }
            product.IncludedBadWords = string.Join(";", productBadWords);

            if (!string.IsNullOrEmpty(product.IncludedBadWords))
            {
                var foundRow = ProductsGrid.Rows.OfType<DataGridViewRow>()
                    .FirstOrDefault(row => row.DataBoundItem == product);

                if (foundRow != null)
                {
                    foundRow.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            var result = string.Join("", (ProductsGrid.DataSource as BindingList<Product>)
                .Where(product => string.IsNullOrEmpty(product.IncludedBadWords))
                .OrderByDescending(product => product.Rating)
                .Select(product => $"<tr><td>{product.Brand}</td><td>{product.Name}</td><td>{product.Rating}</td><td>{product.Reviews}</td><td>{product.Ingredients}</td></tr>"));
            var textToCopy = $"<table>{result}</table>";
            Clipboard.SetDataObject(textToCopy, true);
        }
    }
}
