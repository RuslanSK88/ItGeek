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
    public class AuthorsController : Controller
    {
        private readonly UnitOfWork _uow;

        public AuthorsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/Tags
        public async Task<IActionResult> Index()
        {
            return View(await _uow.AuthorRepository.ListAllAsync());
        }

        public async Task<IActionResult> Delete(int id)
        {
            Author author = await _uow.AuthorRepository.GetByIDAsync(id);
            if (author != null)
            {
                await _uow.AuthorRepository.DeleteAsync(author);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                await _uow.AuthorRepository.InsertAsync(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Author author = await _uow.AuthorRepository.GetByIDAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Author author)
        {
            if (ModelState.IsValid)
            {
                await _uow.AuthorRepository.UpdateAsync(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
    }
}
