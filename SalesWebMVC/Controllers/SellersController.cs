using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using System.Linq;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        //private readonly SellerService _sellerService;
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService; // objeto do construtor para utilizar a classe SellerSer
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll(); // utilizacao do metodo da classe de servico, a qual possui o objeto de comunicacao com o banco de dados.
            return View(list); // retorna a lista para a camada view na pagina index

            // fluxo do objeto de conexao do banco: SellersController --> SellerService --> SalesWebMVCContext
        }

        public IActionResult Create() // pagina create
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
