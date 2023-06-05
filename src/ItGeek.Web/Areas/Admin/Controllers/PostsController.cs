using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItGeek.DAL.Entities;
using ItGeek.BLL;
using ItGeek.Web.Areas.Admin.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly UnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PostsController(UnitOfWork uow, IWebHostEnvironment hostEnvironment)
        {
            _uow = uow;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<Post> allPosts = (List<Post>)await _uow.PostRepository.ListAllAsync();
            List<PostContent> allPostsContent = (List<PostContent>)await _uow.PostContentRepository.ListAllAsync();


            List<PostViewModel> post = new List<PostViewModel>();
            foreach (var onePost in allPosts)
            {
                PostContent onePostsContent = allPostsContent.First(x => x.PostId == onePost.Id);
                post.Add(new PostViewModel()
                {
                    Id = onePost.Id,
                    Slug = onePost.Slug,
                    IsDeleted = onePost.IsDeleted,
                    Title = onePostsContent.Title,
                    PostBody = onePostsContent.PostBody,
                    PostImage = onePostsContent.PostImage,
                    CommentsClosed = onePostsContent.CommentsClosed,
                }
                );
            }

            return View(post);
        }

        public async Task<IActionResult> Details(int id)
        {
            Post post = await _uow.PostRepository.GetByIDAsync(id);
            PostContent postContent = await _uow.PostContentRepository.GetByPostIDAsync(id);

            PostViewModel postViewModel = new PostViewModel()
            {
                Id = id,
                Slug = post.Slug,
                IsDeleted = post.IsDeleted,
                Title = postContent.Title,
                PostBody = postContent.PostBody,
                PostImage = postContent.PostImage,
                CommentsClosed = postContent.CommentsClosed
            };

            return View(postViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            PostContent postContent = await _uow.PostContentRepository.GetByPostIDAsync(id);
            if (postContent != null)
            {
                await _uow.PostContentRepository.DeleteAsync(postContent);
            }
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
            ViewBag.Authors = await _uow.AuthorRepository.ListAllAsync();
            ViewBag.Categories = await _uow.CategoryRepository.ListAllAsync();
            ViewBag.PostCategories = new int[0];
            ViewBag.PostAuthors = new int[0];
            ViewBag.CategoryCount = null;
            ViewBag.AuthorCount = null;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                postViewModel.PostImage = await ProcessUploadFile(postViewModel);

                Post post = new Post()
                {
                    Id = postViewModel.Id,
                    Slug = postViewModel.Slug,
                    IsDeleted = postViewModel.IsDeleted,
                    CreatedAt = DateTime.Now,
                    EditedAt = DateTime.Now,
                };
                PostContent postContent = new PostContent()
                {
                    PostId = postViewModel.Id,
                    Post = post,
                    Title = postViewModel.Title,
                    PostBody = postViewModel.PostBody,
                    PostImage = postViewModel.PostImage,
                    CommentsNum = 0,
                    CommentsClosed = postViewModel.CommentsClosed
                };

                await _uow.PostRepository.InsertAsync(post);
                await _uow.PostContentRepository.InsertAsync(postContent);

                foreach (int catId in postViewModel.CategoryId)
                {
                    PostCategory postCategory = new PostCategory()
                    {
                        PostId = post.Id,
                        CategoryId = catId
                    };
                    await _uow.PostCategoryRepository.InsertAsync(postCategory);
                }
                foreach (int authId in postViewModel.AuthorId)
                {
                    PostAuthor postAuthor = new PostAuthor()
                    {
                        PostId = post.Id,
                        AuthorId = authId
                    };
                    await _uow.PostAuthorRepository.InsertAsync(postAuthor);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Authors = await _uow.AuthorRepository.ListAllAsync();
            ViewBag.Categories = await _uow.CategoryRepository.ListAllAsync();
            ViewBag.PostCategories = await _uow.PostCategoryRepository.ListByPostIdAsync(postViewModel.Id);
            ViewBag.PostAuthors = await _uow.PostAuthorRepository.ListByPostIdAsync(postViewModel.Id);
            ViewBag.CategoryCount = ((IEnumerable<int>)ViewBag.PostCategories).ToList().Count;
            ViewBag.AuthorCount = ((IEnumerable<int>)ViewBag.PostAuthors).ToList().Count;
            return View(postViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Post post = await _uow.PostRepository.GetByIDAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            PostContent postContent = await _uow.PostContentRepository.GetByPostIDAsync(id);


            PostViewModel postViewModel = new PostViewModel()
            {
                Id = id,
                Slug = post.Slug,
                IsDeleted = post.IsDeleted,
                Title = postContent.Title,
                PostBody = postContent.PostBody,
                PostImage = postContent.PostImage,
                CommentsClosed = postContent.CommentsClosed,
            };
            ViewBag.Authors = await _uow.AuthorRepository.ListAllAsync();
            ViewBag.Categories = await _uow.CategoryRepository.ListAllAsync();
            ViewBag.PostCategories = await _uow.PostCategoryRepository.ListByPostIdAsync(id);
            ViewBag.PostAuthors = await _uow.PostAuthorRepository.ListByPostIdAsync(id);
            ViewBag.CategoryCount = ((IEnumerable<int>)ViewBag.PostCategories).ToList().Count;
            ViewBag.AuthorCount = ((IEnumerable<int>)ViewBag.PostAuthors).ToList().Count;

            return View(postViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                Post post = await _uow.PostRepository.GetByIDAsync(postViewModel.Id);

                post.Slug = postViewModel.Slug;
                post.IsDeleted = postViewModel.IsDeleted;
                post.EditedAt = DateTime.Now;
                //TODO: post.EditedBy = User;

                await _uow.PostRepository.UpdateAsync(post);

                // Получим Пост контент
                PostContent postContent = await _uow.PostContentRepository.GetByPostIDAsync(postViewModel.Id);

                // Заполняем Пост контент из формы
                postContent.Title = postViewModel.Title;
                postContent.PostBody = postViewModel.PostBody;
                postContent.CommentsClosed = postViewModel.CommentsClosed;

                // Получили новую картинку 
                if (postViewModel.ImageFile != null)
                {
                    string newImage = await ProcessUploadFile(postViewModel);
                    postContent.PostImage = newImage;

                    //TODO удалить старую картинку
                }
                await _uow.PostContentRepository.UpdateAsync(postContent);

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Authors = await _uow.AuthorRepository.ListAllAsync();
            ViewBag.Categories = await _uow.CategoryRepository.ListAllAsync();
            ViewBag.PostCategories = await _uow.PostCategoryRepository.ListByPostIdAsync(postViewModel.Id);
            ViewBag.PostAuthors = await _uow.PostAuthorRepository.ListByPostIdAsync(postViewModel.Id);
            ViewBag.CategoryCount = ((IEnumerable<int>)ViewBag.PostCategories).ToList().Count;
            ViewBag.AuthorCount = ((IEnumerable<int>)ViewBag.PostAuthors).ToList().Count;
            return View(postViewModel);
        }

        protected async Task<string> ProcessUploadFile(PostViewModel postViewModel)
        {
            string uniqueFileName = "";
            if (postViewModel.ImageFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(postViewModel.ImageFile.FileName);
                string fileExtension = Path.GetExtension(postViewModel.ImageFile.FileName);
                uniqueFileName = fileName + DateTime.Now.ToString("yymmddssfff") + fileExtension;
                string path = Path.Combine(wwwRootPath + "/uploads/", uniqueFileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await postViewModel.ImageFile.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
        public async Task<JsonResult> GetTagByName(string tag)
        {

            List<Tag> result = await _uow.TagRepository.GetTagByNameAsync(tag);

            List<string> res = new List<string>();
            foreach (Tag item in result)
            {
                res.Add(item.Name);
            }

            return Json(res);
        }
    }
}