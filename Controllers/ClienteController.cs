using Aula2407.Data;
using Aula2407.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


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
        public async Task<IActionResult> BuscaClientes()
        {
            return View(await _context.Clientes.ToListAsync());
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
                }
                //se o id do cliente for zero,adiciona um novo cliente ao banco de dados
                else
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                }
                // redirediciona para o metodo  buscarClientes apos o cadastro ou atualizaçao
                return RedirectToAction("BuscaClientes");
            }
            // se o modelo nao for valido retorna a view com os dados do cliente para correçao
            return View(cliente);
        }
        public async Task<IActionResult> DetalhesClientes(int Id)
        {

            // retorna uma view com os detalhes do clientes encontrado pelo id
            return View(await _context.Clientes.FindAsync(Id));
        }
    }
}

