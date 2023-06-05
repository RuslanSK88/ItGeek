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
    public class TagsController : Controller
    {
        private readonly UnitOfWork _uow;

        public TagsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/Tags
        public async Task<IActionResult> Index()
        {
            return View(await _uow.TagRepository.ListAllAsync());
        }

        public async Task<IActionResult> Delete(int id)
        {
            Tag tag = await _uow.TagRepository.GetByIDAsync(id);
            if (tag != null)
            {
                await _uow.TagRepository.DeleteAsync(tag);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _uow.TagRepository.InsertAsync(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Tag tag = await _uow.TagRepository.GetByIDAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _uow.TagRepository.UpdateAsync(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }
    }
}
