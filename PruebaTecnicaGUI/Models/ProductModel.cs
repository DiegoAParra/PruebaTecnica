using Newtonsoft.Json;
using System.Collections.Generic;

namespace PruebaTecnicaGUI.Models
{
    public class ProductModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("images")]
        public List<string> Images { get; set; }
    }
}