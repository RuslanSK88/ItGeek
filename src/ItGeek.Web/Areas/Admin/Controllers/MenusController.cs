using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItGeek.DAL.Entities;
using ItGeek.BLL;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenusController : Controller
    {
        private readonly UnitOfWork _uow;

        public MenusController(UnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _uow.MenuRepository.ListAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.MenuRepository.GetByIDAsync(id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Menu menu = await _uow.MenuRepository.GetByIDAsync(id);
            if (menu != null)
            {
                await _uow.MenuRepository.DeleteAsync(menu);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _uow.MenuRepository.InsertAsync(menu);
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Menu menu = await _uow.MenuRepository.GetByIDAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _uow.MenuRepository.UpdateAsync(menu);
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }
    }
}
