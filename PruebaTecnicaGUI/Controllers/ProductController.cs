using PruebaTecnicaGUI.Models;
using PruebaTecnicaGUI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PruebaTecnicaGUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        public async Task<ActionResult> Index()
        {
            List<ProductModel> products = await _productService.GetProductsAsync();
            return View(products);
        }
    }
}
