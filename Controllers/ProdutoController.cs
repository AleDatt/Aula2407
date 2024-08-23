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

        public async Task<IActionResult> BuscaProdutos(int pagina = 1)
        {
            var QtdeTProdutos = 2;

            var items = await _context.Produtos.OrderBy(c => c.Nome).ToListAsync();
            // var pagedItems = items.Skip((pagina - 1) * QtdeTProdutos).Take(QtdeTClientes).ToList();

            //Passando os dados e informações de paginação para a view
            ViewBag.QtdePaginas = (int)Math.Ceiling((double)items.Count / QtdeTProdutos);
            ViewBag.PaginaAtual = pagina;
            ViewBag.QtdeTProdutos = QtdeTProdutos;

            return View(items);
            //return View(await _context.Clientes.TolistAsync());

        }

        public async Task<IActionResult> CadastroProduto(int? id)
        {
            if(id  == null)
            {
                return View();
            }
           else
            {
                return View(await _context.Produtos.FindAsync(id));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> CadastroProduto([Bind("Id,Nome,Quantidade,Marca,Tamanho,DataValidade,ValorUnitario")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (produto.Id != 0)
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "2";
                }


                else
                {
                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "1";
                }

                return RedirectToAction("BuscaProdutos");
            }
            return View(produto);
        }

        public async Task<IActionResult> DetalhesProduto(int Id)
        {
            return View(await _context.Produtos.FindAsync(Id));
        }

        public async Task<IActionResult> DeletarProdutos(int Id)
        {
            if (Id != 0)
            {
                var produto = await _context.Produtos.FindAsync(Id);

                if (produto != null)
                
                    _context.Remove<Produto>(produto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("BuscaProdutos");
            }
            return RedirectToAction("BuscaProdutos");
        }
    }
}








      
