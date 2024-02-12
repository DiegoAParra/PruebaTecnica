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

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product != null)
            {
                return View(product); // Puedes devolver una vista que muestre el producto
            }
            else
            {
                return HttpNotFound(); // Puedes devolver una página de error 404 si el producto no se encuentra
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product != null)
            {
                return View(product); // Devuelve la vista de confirmación de eliminación
            }
            else
            {
                return HttpNotFound(); // Devuelve una respuesta HTTP 404 si el producto no se encuentra
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteProductConfirmed(int id)
        {
            var success = await _productService.DeleteProductByIdAsync(id);

            if (success)
            {
                return RedirectToAction("Index"); // Redirige a la página principal o a donde desees después de la eliminación
            }
            else
            {
                return HttpNotFound(); // Devuelve una respuesta HTTP 404 si el producto no se encuentra
            }
        }
    }
}
