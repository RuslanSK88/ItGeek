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
    public class AuthorsSocialsController : Controller
    {
        private readonly UnitOfWork _uow;

        public AuthorsSocialsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/Tags
        public async Task<IActionResult> Index()
        {
            return View(await _uow.AuthorsSocialRepository.ListAllAsync());
        }

        public async Task<IActionResult> Delete(int id)
        {
            AuthorsSocial authorsSocial = await _uow.AuthorsSocialRepository.GetByIDAsync(id);
            if (authorsSocial != null)
            {
                await _uow.AuthorsSocialRepository.DeleteAsync(authorsSocial);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuthorsSocial authorsSocial)
        {
            if (ModelState.IsValid)
            {
                await _uow.AuthorsSocialRepository.InsertAsync(authorsSocial);
                return RedirectToAction(nameof(Index));
            }
            return View(authorsSocial);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            AuthorsSocial authorsSocial = await _uow.AuthorsSocialRepository.GetByIDAsync(id);
            if (authorsSocial == null)
            {
                return NotFound();
            }
            return View(authorsSocial);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AuthorsSocial authorsSocial)
        {
            if (ModelState.IsValid)
            {
                await _uow.AuthorsSocialRepository.UpdateAsync(authorsSocial);
                return RedirectToAction(nameof(Index));
            }
            return View(authorsSocial);
        }
    }
}
