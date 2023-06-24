using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ItGeek.DAL.Entities;

public class Author : BaseEntity
{
    public string Name{ get; set; }
    public string Slug{ get; set; }
    public string Regalia { get; set; }
    public string Description { get; set; }
    public string? AuthorImage { get; set; }

    [NotMapped]
    [Display(Name = "Картинка")]
    public IFormFile? ImageFile { get; set; }
    public string Email { get; set; }
	public List<Post> Posts { get; } = new();
	
}
