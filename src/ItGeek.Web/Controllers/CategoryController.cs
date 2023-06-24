using ItGeek.BLL;
using ItGeek.DAL.Entities;
using ItGeek.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Controllers
{
	public class CategoryController : Controller
	{
        private readonly UnitOfWork _uow;

        public CategoryController(UnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpGet("[Controller]/{categorySlug}")]
        public async Task<IActionResult> Index(string categorySlug)
		{
			Category category = await _uow.CategoryRepository.GetBySlugAsync(categorySlug);
			return View(category);
		}

		[HttpGet("[Controller]/{categorySlug}/{postSlug}")]
		public async Task<IActionResult> Post(string categorySlug, string postSlug)
        {
			Post postOne = await _uow.PostRepository.GetBySlugAsync(postSlug);
            ViewBag.Category = await _uow.CategoryRepository.GetBySlugAsync(categorySlug);
			return View(postOne);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment, string categorySlugOld, string postSlugOld)
        {
            comment.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                await _uow.CommentRepository.InsertAsync(comment);

                Post postOne = await _uow.PostRepository.GetBySlugAsync(postSlugOld);
                PostComment postComment = new PostComment()
                {
                    PostId = postOne.Id,
                    CommentId = comment.Id,
                };
                await _uow.PostCommentRepository.InsertAsync(postComment);

            }
            return RedirectToAction("Post", new { categorySlug = categorySlugOld, postSlug = postSlugOld });
        }
    }
}
