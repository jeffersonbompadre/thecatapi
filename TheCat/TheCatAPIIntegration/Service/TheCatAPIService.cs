using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TheCatDomain.Interfaces.Integration;
using TheCatDomain.Models;

namespace TheCatAPIIntegration.Service
{
    public class TheCatAPIService : ITheCatAPI
    {
        readonly AppSettings appSettings;

        public TheCatAPIService(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public async Task<ICollection<BreedsSearchResponse>> GetBreeds()
        {
            var jsonResult = await GetHttpResponse(appSettings.TheCatSettings.BreedsMethod);
            if (string.IsNullOrEmpty(jsonResult))
                return null;
            else
            {
                var result = JsonConvert.DeserializeObject<ICollection<BreedsSearchResponse>>(jsonResult);
                return result;
            }
        }

        public async Task<ICollection<CategorySearchResponse>> GetCategories()
        {
            var jsonResult = await GetHttpResponse(appSettings.TheCatSettings.CategoryMethod);
            if (string.IsNullOrEmpty(jsonResult))
                return null;
            else
            {
                var result = JsonConvert.DeserializeObject<ICollection<CategorySearchResponse>>(jsonResult);
                return result;
            }
        }

        public async Task<ICollection<ImageSearchResponse>> GetImagesByCategory(string categoryId, int limitImages = 4)
        {
            var jsonResult = await GetHttpResponse($"{appSettings.TheCatSettings.ImageMethod}?category_ids={categoryId}&limit={limitImages}&include_categories=false");
            if (string.IsNullOrEmpty(jsonResult))
                return null;
            else
            {
                var result = JsonConvert.DeserializeObject<ICollection<ImageSearchResponse>>(jsonResult);
                return result;
            }
        }

        public async Task<ICollection<ImageSearchResponse>> GetImagesByBreeds(string breedsId, int limitImages = 3)
        {
            var jsonResult = await GetHttpResponse($"{appSettings.TheCatSettings.ImageMethod}?breeds_id={breedsId}&limit={limitImages}&include_categories=false&include_breeds=false");
            if (string.IsNullOrEmpty(jsonResult))
                return null;
            else
            {
                var result = JsonConvert.DeserializeObject<ICollection<ImageSearchResponse>>(jsonResult);
                return result;
            }
        }

        async Task<string> GetHttpResponse(string method)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(appSettings.TheCatSettings.BaseURL);
                var httpResponse = await httpClient.GetAsync(method);
                if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    return string.Empty;
                else
                {
                    var responseAsString = await httpResponse.Content.ReadAsStringAsync();
                    return responseAsString;
                }
            }
        }
    }
}
