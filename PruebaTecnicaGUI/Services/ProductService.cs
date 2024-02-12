using Newtonsoft.Json;
using PruebaTecnicaGUI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task<ProductModel> GetProductByIdAsync(int productId)
        {
            string apiUrl = $"https://api.escuelajs.co/api/v1/products/{productId}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                ProductModel product = JsonConvert.DeserializeObject<ProductModel>(json);

                // Aquí puedes realizar operaciones adicionales si es necesario

                return product;
            }

            return null;
        }

        public async Task<bool> DeleteProductByIdAsync(int productId)
        {
            try
            {
                string apiUrl = $"https://api.escuelajs.co/api/v1/products/{productId}";

                HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Manejar excepciones según tus necesidades (por ejemplo, loguear el error)
                Console.WriteLine($"Error al eliminar el producto: {ex.Message}");
                return false;
            }
        }
    }
}