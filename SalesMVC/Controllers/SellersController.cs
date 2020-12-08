using Microsoft.AspNetCore.Mvc;
using SalesMVC.Services;
using SalesMVC.Models;
using SalesMVC.Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Threading.Tasks;

namespace SalesMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController (SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel{Departments = departments};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? ID)
        {
            if (ID == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int ID)
        {
            try
            {
                await _sellerService.RemoveAsync(ID);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? ID)
        {
            if (ID == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }


        public async Task<IActionResult> Edit(int? ID)
        {
            if(ID==null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIDAsync(ID.Value);
            if(obj==null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel {Seller = obj, Departments = departments};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? ID, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            if (ID != seller.ID)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
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