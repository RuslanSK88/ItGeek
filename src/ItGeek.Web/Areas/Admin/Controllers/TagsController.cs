﻿using ItGeek.BLL;
using ItGeek.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
	public class TagsController : Controller
	{
		private readonly UnitOfWork _uow;

		public TagsController(UnitOfWork uow)
        {
			_uow = uow;
		}

        public async Task<IActionResult> Index()
		{
			return View(await _uow.TagRepository.ListAllAsync());
		}

		public async Task<IActionResult> Details(int id)
		{
			return View(await _uow.TagRepository.GetByIDAsync(id));
		}

		public async Task<IActionResult> Delete(int id)
		{
			Tag tag = await _uow.TagRepository.GetByIDAsync(id);
			if(tag != null)
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
