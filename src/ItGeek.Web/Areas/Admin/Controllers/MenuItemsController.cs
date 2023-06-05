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
    public class MenuItemsController : Controller
    {
        private readonly UnitOfWork _uow;

        public MenuItemsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index(int menuid)
        {
            ViewBag.Id = menuid;
            return View(await _uow.MenuItemRepository.GetByMenuIdAsync(menuid));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.MenuItemRepository.GetByIDAsync(id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            MenuItem menuItem = await _uow.MenuItemRepository.GetByIDAsync(id);
            if (menuItem != null)
            {
                await _uow.MenuItemRepository.DeleteAsync(menuItem);
            }
            return RedirectToAction(nameof(Index), new {menuid = menuItem.MenuId});
        }
        [HttpGet]
        public async Task<IActionResult> Create(int menuid)
        {
            ViewBag.Id = menuid;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MenuItem menuItem, int menuid)
        {
            ViewBag.Id = menuid;
            if (ModelState.IsValid)
            {
                await _uow.MenuItemRepository.InsertAsync(menuItem);
                return RedirectToAction(nameof(Index), new { menuid = menuid });
            }
            return View(menuItem);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id, int menuid)
        {
            MenuItem menuItem = await _uow.MenuItemRepository.GetByIDAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            ViewBag.Id = menuid;
            return View(menuItem);
        }
        [HttpPost]
        public async Task<IActionResult> Update(MenuItem menuItem, int menuid)
        {
            if (ModelState.IsValid)
            {
                await _uow.MenuItemRepository.UpdateAsync(menuItem);
                return RedirectToAction(nameof(Index), new { menuid = menuItem.MenuId });
            }
            ViewBag.Id = menuid;
            return View(menuItem);
        }
    }
}