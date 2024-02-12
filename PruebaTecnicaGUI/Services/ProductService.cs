using Newtonsoft.Json;
using PruebaTecnicaGUI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task<ProductModel> CreateProductAsync(ProductModel productCreateModel)
        {
            try
            {
                string apiUrl = "https://api.escuelajs.co/api/v1/products/";

                // Serializar el objeto productCreateModel a JSON
                string jsonBody = JsonConvert.SerializeObject(productCreateModel);

                // Configurar la solicitud POST con el cuerpo JSON
                using (var request = new HttpRequestMessage(HttpMethod.Post, apiUrl))
                {
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Enviar la solicitud y obtener la respuesta
                    using (var response = await _httpClient.SendAsync(request))
                    {
                        // Verificar el estado de la respuesta
                        if (response.IsSuccessStatusCode)
                        {
                            // Leer y deserializar el contenido de la respuesta
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            ProductModel createdProduct = JsonConvert.DeserializeObject<ProductModel>(jsonResponse);
                            return createdProduct;
                        }
                        else
                        {
                            // Manejar el caso en que la creación del producto falla
                            Console.WriteLine($"Error al crear el producto. StatusCode: {response.StatusCode}");
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones según tus necesidades (por ejemplo, loguear el error)
                Console.WriteLine($"Error al crear el producto: {ex.Message}");
                return null;
            }
        }

        public async Task<ProductModel> GetProductByIdAsync(int productId)
        {
            string apiUrl = $"https://api.escuelajs.co/api/v1/products/{productId}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                ProductModel product = JsonConvert.DeserializeObject<ProductModel>(json);

                return product;
            }

            return null;
        }

        public async Task<ProductModel> UpdateProductAsync(int productId, ProductModel productUpdateModel)
        {
            try
            {
                string apiUrl = $"https://api.escuelajs.co/api/v1/products/{productId}";

                // Serializar solo las propiedades que se desean actualizar a JSON
                string jsonBody = JsonConvert.SerializeObject(new
                {
                    title = productUpdateModel.Title,
                    price = productUpdateModel.Price
                    // Agrega más propiedades según sea necesario
                });

                // Configurar la solicitud PUT con el cuerpo JSON
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, apiUrl);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    ProductModel updatedProduct = JsonConvert.DeserializeObject<ProductModel>(jsonResponse);
                    return updatedProduct;
                }

                return null;
            }
            catch (Exception ex)
            {
                // Manejar excepciones según tus necesidades (por ejemplo, loguear el error)
                Console.WriteLine($"Error al actualizar el producto: {ex.Message}");
                return null;
            }
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
                Console.WriteLine($"Error al eliminar el producto: {ex.Message}");
                return false;
            }
        }
    }
}