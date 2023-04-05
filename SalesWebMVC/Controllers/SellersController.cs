using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using System.Linq;
using SalesWebMVC.Models.ViewModels;
using System.Collections.Generic;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        //private readonly SellerService _sellerService;
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService; // objeto do construtor para utilizar a classe SellerSer
            _departmentService = departmentService;
        }

        public IActionResult Index() // metodo IActionResult
        {
            var list = _sellerService.FindAll(); // utilizacao do metodo da classe de servico, a qual possui o objeto de comunicacao com o banco de dados.
            return View(list); // retorna a lista para a camada view na pagina index

            // fluxo do objeto de conexao do banco: SellersController --> SellerService --> SalesWebMVCContext
        }

        public IActionResult Create() // pagina create
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel {Departments = departments};
            return View(viewModel); // retorna visualizacao pagina create
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewmodel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewmodel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
                return BadRequest();
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }

            catch (NotFoundException)
            {
                return NotFound();
            }

            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
    
}