using ItGeek.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Entities;

public class AuthorsSocial : BaseEntity
{
    public int AuthorId { get; set; }
    public virtual Author? Author { get; set; }
    public SocialName SocialName { get; }
    public string SocialLink { get; set; }
}
