using ItGeek.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItGeek.Web.Areas.Admin.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        //from Post
        [Required]
        [StringLength(250)]
        [Display(Name ="Ссылка")]
        public string Slug { get; set; }
        [Display(Name = "Черновик")]
        public bool IsDeleted { get; set; } = false;

        //from PostContent
        [Required]
        [StringLength(250)]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Текст новости")]
        public string PostBody { get; set; }

        [Display(Name = "Изображение")]
        public string? PostImage { get; set; }
        [Display(Name = "Открыть комментарии")]
        public bool CommentsClosed { get; set; } = true;

        [NotMapped]
        [Display(Name = "Картинка")]
        public IFormFile? ImageFile { get; set; }

		[Display(Name = "Категория")]
		public int[] CategoryId { get; set; }


		[Display(Name = "Автор")]
		public int[] AuthorId { get; set; }
	}
}
