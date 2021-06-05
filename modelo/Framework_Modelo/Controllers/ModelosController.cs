using Framework_Modelo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Framework_Modelo.Controllers
{
    public class ModelosController : Controller
    {
        private readonly IModelosService _repository;

        public ModelosController(IModelosService repository)
        {
            _repository = repository;
        }

        public IActionResult Tabela()
        {
            ViewBag.Tabela = _repository.GetTabela();

            return View();
        }

        public IActionResult Pagina()
        {
            return View();
        }
    }
}
