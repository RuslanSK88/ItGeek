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

        public IActionResult Index()
		{
			return View();
		}
        public async Task<IActionResult> Post(string categorySlug, string postSlug)
        {
            Post postOne = await _uow.PostRepository.GetBySlugAsync(postSlug);
            Category category = await _uow.CategoryRepository.GetBySlugAsync(categorySlug);
            List<Post> allPosts = await _uow.PostRepository.ListByCategoryIdAsync(category.Id);

            List<PostContent> allPostContent = await _uow.PostContentRepository.ListByCategoryIdAsync(category.Id);

            PostContentViewModel postContent = new PostContentViewModel()
            {
                category = category,
                post = postOne,
                postContent = await _uow.PostContentRepository.GetByPostIDAsync(postOne.Id),
                posts = allPosts,
                postContents = allPostContent
            };

            return View(postContent);
        }
    }
}
