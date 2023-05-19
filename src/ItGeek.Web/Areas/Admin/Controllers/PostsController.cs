﻿using System;
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
    public class PostsController : Controller
    {
        private readonly UnitOfWork _uow;

        public PostsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _uow.PostRepository.ListAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.PostRepository.GetByIDAsync(id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Post post = await _uow.PostRepository.GetByIDAsync(id);
            if (post != null)
            {
                await _uow.PostRepository.DeleteAsync(post);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                await _uow.PostRepository.InsertAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Post post = await _uow.PostRepository.GetByIDAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Post post)
        {
            if (ModelState.IsValid)
            {
                await _uow.PostRepository.UpdateAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
    }
}
