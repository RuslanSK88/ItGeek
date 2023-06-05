using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Entities;

public class Author : BaseEntity
{
    public string Name{ get; set; }
    public string Slug{ get; set; }
    public string Regalia { get; set; }
    public string Description { get; set; }
    public string AuthorImage { get; set; }
    public string Email { get; set; }
	public List<Post> Posts { get; } = new();
	
}
