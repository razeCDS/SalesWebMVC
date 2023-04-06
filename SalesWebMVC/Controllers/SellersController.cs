using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using System.Linq;
using SalesWebMVC.Models.ViewModels;
using System.Collections.Generic;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index() // metodo IActionResult
        {
            var list = await _sellerService.FindAllAsync(); // utilizacao do metodo da classe de servico, a qual possui o objeto de comunicacao com o banco de dados.
            return View(list); // retorna a lista para a camada view na pagina index

            // fluxo do objeto de conexao do banco: SellersController --> SellerService --> SalesWebMVCContext
        }

        public async Task<IActionResult> Create() // pagina create
        {
            var departments = await _departmentService.FindAllAsync(); // lista obtida a partir da classe Department Service
            var viewModel = new SellerFormViewModel {Departments = departments};
            return View(viewModel); // retorna visualizacao pagina create
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(seller);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewmodel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewmodel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {

            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(seller);
            }

            if (id != seller.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            try
            {
               await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }

            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}