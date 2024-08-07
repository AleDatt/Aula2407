using Aula2407.Data;
using Aula2407.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Aula2407.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AulaContext _context;

        public ProdutoController(AulaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> BuscaProdutos()
        {
            return View(await _context.Produtos.ToListAsync());
        }

        public IActionResult CadastroProduto()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> CadastroProduto([Bind("Id,Nome,Quantidade,Marca,Tamanho,DataValidade,ValorUnitario")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction("BuscaProdutos");
            }
            return View(produto);
        }

    }
}





  

       
        