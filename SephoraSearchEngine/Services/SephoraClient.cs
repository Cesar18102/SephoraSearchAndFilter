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
            var response = await ExecuteRequest<RootCategoriesDto>(getRootCategoriesRequest);
            return response.RootCategories;
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

            var response = await ExecuteRequest<ChildCategoriesDto>(getSubCategoriesRequest);
            category.SubCategories = response.ChildCategories;

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
                var productsResponse = await ExecuteRequest<ProductsListDto>(getProductsRequest);

                if (productsResponse.Products.Length == 0)
                {
                    break;
                }

                foreach (var product in productsResponse.Products)
                {
                    await LoadProductDetails(product);
                    OnProductLoaded?.Invoke(this, product);

                    if (product.IncludedBadWords.Length == 0)
                    {
                        await CheckAvailability(product, 50.061597, 19.938049, 15);
                    }
                }

                category.Products = category.Products.Concat(productsResponse.Products).ToArray();
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
            var productDetailsResponse = await ExecuteRequest<dynamic>(productDetailsRequest);

            product.Ingredients = productDetailsResponse?.currentSku?.ingredientDesc;
        }

        public async Task CheckAvailability(Product product, double latitude, double longitude, double radius)
        {
            if (product.CurrentSku == null)
            {
                return;
            }

            var productAvailabilityRequest = new RestRequest("products/check-availability");
            productAvailabilityRequest.AddParameter("skuId", product.CurrentSku.Id);
            productAvailabilityRequest.AddParameter("latitude", latitude.ToString().Replace(',', '.'));
            productAvailabilityRequest.AddParameter("longitude", longitude.ToString().Replace(',', '.'));
            productAvailabilityRequest.AddParameter("radius", radius);
            var productAvailabilityResponse = await ExecuteRequest<AvailabilityDto>(productAvailabilityRequest);

            product.IsAvailable = productAvailabilityResponse.Stores.Length != 0;
        }

        private async Task<TDataItem> ExecuteRequest<TDataItem>(RestRequest restRequest)
        {
            try
            {
                AddRapidApiHeaders(restRequest);
                var response = await _restClient.ExecuteAsync(restRequest);
                var result = JsonConvert.DeserializeObject<TDataItem>(response.Content);

                if(!response.IsSuccessful)
                {
                    throw new Exception(response.Content);
                }

                return result;
            }
            catch(Exception ex)
            {
                OnApiKeyBroken?.Invoke(this, new EventArgs());
                await Task.Delay(1000);
                return await ExecuteRequest<TDataItem>(restRequest);
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
