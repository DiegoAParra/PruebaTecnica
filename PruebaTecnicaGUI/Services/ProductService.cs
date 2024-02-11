using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PruebaTecnicaGUI.Models;

namespace PruebaTecnicaGUI.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<ProductModel>> GetProductsAsync()
        {
            string apiUrl = "https://api.escuelajs.co/api/v1/products";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<ProductModel> products = JsonConvert.DeserializeObject<List<ProductModel>>(json);
                return products;
            }

            return null;
        }
    }
}