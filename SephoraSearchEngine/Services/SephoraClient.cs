using Newtonsoft.Json;
using RestSharp;
using SephoraSearchEngine.Dto;
using SephoraSearchEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SephoraSearchEngine.Services
{
    public sealed class SephoraClient
    {
        public event EventHandler<EventArgs> OnApiKeyBroken;
        public event EventHandler<Category> OnCategoryLoaded;
        public event EventHandler<Product> OnProductLoaded;

        private readonly RestClient _restClient = new RestClient("https://sephora.p.rapidapi.com/");
        public string ApiKey { get; set; }

        public async Task<IList<Category>> GetRootCategories()
        {
            var getRootCategoriesRequest = new RestRequest("categories/v2/list-root", Method.Get);
            var response = await ExecuteRequest(getRootCategoriesRequest);
            return JsonConvert.DeserializeObject<RootCategoriesDto>(response.Content).RootCategories;
        }

        public async Task LoadSubCategories(Category category)
        {
            if(category.SubCategories != null)
            {
                return;
            }

            if(!category.HasChildCategories)
            {
                category.SubCategories = new Category[] { };
                return;
            }

            var getSubCategoriesRequest = new RestRequest("categories/list", Method.Get);
            getSubCategoriesRequest.AddParameter("categoryId", category.Id);

            var response = await ExecuteRequest(getSubCategoriesRequest);
            category.SubCategories = JsonConvert.DeserializeObject<ChildCategoriesDto>(response.Content).ChildCategories;

            OnCategoryLoaded?.Invoke(this, category);
        }

        public async Task LoadProducts(Category category)
        {
            if(category.Products == null)
            {
                category.Products = new Product[] { };
            }

            for(int page = 1; ; ++page)
            {
                var getProductsRequest = new RestRequest("products/list");
                getProductsRequest.AddParameter("categoryId", category.Id);
                getProductsRequest.AddParameter("currentPage", page);
                var productsResponse = await ExecuteRequest(getProductsRequest);
                var productsAtPage = JsonConvert.DeserializeObject<ProductsListDto>(productsResponse.Content).Products;

                if (productsAtPage.Length == 0)
                {
                    break;
                }

                foreach (var product in productsAtPage)
                {
                    await LoadProductDetails(product);
                    OnProductLoaded?.Invoke(this, product);
                }

                category.Products = category.Products.Concat(productsAtPage).ToArray();
            }
        }

        public async Task LoadProductDetails(Product product)
        {
            if(product.CurrentSku == null)
            {
                return;
            }

            var productDetailsRequest = new RestRequest("products/detail");
            productDetailsRequest.AddParameter("productId", product.Id);
            productDetailsRequest.AddParameter("preferedSku", product.CurrentSku.Id);
            var productDetailsResponse = await ExecuteRequest(productDetailsRequest);

            dynamic data = JsonConvert.DeserializeObject(productDetailsResponse.Content);
            product.Ingredients = data?.currentSku?.ingredientDesc;
        }

        private string SubstringByBoundText(string source, string leftBound, string rightBound)
        {
            return SubstringByIndexes(source, source.IndexOf(leftBound) + leftBound.Length - 1, source.LastIndexOf(rightBound));
        }

        private string SubstringByIndexes(string source, int startIndex, int endIndex)
        {
            return source.Substring(startIndex, endIndex - startIndex + 1);
        }

        private async Task<RestResponse> ExecuteRequest(RestRequest restRequest)
        {
            try
            {
                AddRapidApiHeaders(restRequest);
                return await _restClient.ExecuteAsync(restRequest);
            }
            catch(Exception ex)
            {
                OnApiKeyBroken?.Invoke(this, new EventArgs());
                await Task.Delay(1000);
                return await ExecuteRequest(restRequest);
            }
        }

        private void AddRapidApiHeaders(RestRequest restRequest)
        {
            if(ApiKey == null)
            {
                throw new InvalidOperationException("No Api Key set");
            }

            restRequest.AddHeader("X-RapidAPI-Key", ApiKey);
            restRequest.AddHeader("X-RapidAPI-Host", "sephora.p.rapidapi.com");
        }
    }
}
