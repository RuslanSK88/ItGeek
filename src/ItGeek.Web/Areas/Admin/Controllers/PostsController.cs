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

        public PostsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            List<Post> allPosts = (List<Post>)await _uow.PostRepository.ListAllAsync();
            List<PostContent> allPostsContent = (List<PostContent>)await _uow.PostContentRepository.ListAllAsync();
            
            
            List<PostViewModel> post = new List<PostViewModel>();
            foreach (var onePost in allPosts)
            {
                PostContent onePostsContent = allPostsContent.First(x=>x.PostId == onePost.Id);
                post.Add(new PostViewModel()
                    {
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
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
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
