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
        public IActionResult CadastroCliente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroCliente([Bind("Id,Nome,RG,CPF,Usuario,Senha,CEP,UF,Cidade,Bairro,Rua,Numero,Complemento")] Cliente cliente)
        {
            if (ModelState.IsValid) 
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("BuscaClientes");
            }
            return View(cliente);
        }
    }
}

