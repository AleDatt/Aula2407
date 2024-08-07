using Microsoft.AspNetCore.Mvc;

namespace Aula2407.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult CadastroProduto()
        {
            return View();
        }
    }
}
