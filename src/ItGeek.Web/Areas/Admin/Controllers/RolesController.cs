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
    public class RolesController : Controller
    {
        private readonly UnitOfWork _uow;

        public RolesController(UnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _uow.RoleRepository.ListAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.RoleRepository.GetByIDAsync(id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Role role = await _uow.RoleRepository.GetByIDAsync(id);
            if (role != null)
            {
                await _uow.RoleRepository.DeleteAsync(role);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            if (ModelState.IsValid)
            {
                await _uow.RoleRepository.InsertAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Role role = await _uow.RoleRepository.GetByIDAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Role role)
        {
            if (ModelState.IsValid)
            {
                await _uow.RoleRepository.UpdateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
    }
}
