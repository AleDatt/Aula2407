using Aula2407.Data;
using Aula2407.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;


namespace Aula2407.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AulaContext _context;

        public ClienteController(AulaContext context)
        {
            _context = context;
        }

        //BUSCAR CLIENTES
        public async Task<IActionResult> BuscaClientes(int pagina = 1)
        {
            var QtdeTClientes = 2;

            var items = await _context.Clientes.OrderBy(c => c.Nome).ToListAsync();
           // var pagedItems = items.Skip((pagina - 1) * QtdeTClientes).Take(QtdeTClientes).ToList();
            
            //Passando os dados e informações de paginação para a view
            ViewBag.QtdePaginas = (int)Math.Ceiling((double)items.Count() / QtdeTClientes);
            ViewBag.PaginaAtual = pagina;
            ViewBag.QtdeTClientes = QtdeTClientes;

            return View(items);
            //return View(await _context.Clientes.TolistAsync());
        }

        //CADASTRO DE CLIENTES
        public async Task<IActionResult> CadastroCliente(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                return View(await _context.Clientes.FindAsync(id));
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroCliente([Bind("Id,Nome,RG,CPF,Usuario,Senha,CEP,UF,Cidade,Bairro,Rua,Numero,Complemento")] Cliente cliente)
        {
            // verifica se o modelo e valido.
            if (ModelState.IsValid)
            {
                // se o id do cliente for diferente de zero,atualiza o cliente existente
                if (cliente.Id != 0)
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "2";
                }
                //se o id do cliente for zero,adiciona um novo cliente ao banco de dados
                else
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "1";
                }
                // redirediciona para o metodo  buscarClientes apos o cadastro ou atualizaçao
                return RedirectToAction("BuscaClientes");
            }
            // se o modelo nao for valido retorna a view com os dados do cliente para correçao
            return View(cliente);
        }
        public async Task<IActionResult> DetalhesCliente(int Id)
        {

            // retorna uma view com os detalhes do clientes encontrado pelo id
            return View(await _context.Clientes.FindAsync(Id));
        }

       //Action para deletar clientes
        public async Task<IActionResult> DeletarCliente(int Id)
        {
            if (Id != 0)
            {
                var cliente = await _context.Clientes.FindAsync(Id);

                if (cliente != null)
                    _context.Remove<Cliente>(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("BuscaClientes");
            }
            return RedirectToAction("BuscaClientes");

        }
    }
}

