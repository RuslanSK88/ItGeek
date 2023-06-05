using ItGeek.DAL.Entities;
using ItGeek.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.DAL.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}
	public DbSet<Author> Authors { get; set; }
	public DbSet<AuthorsSocial> AuthorsSocials { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Comment> Comments { get; set; }
	public DbSet<Menu> Menus { get; set; }
	public DbSet<MenuItem> MenuItems { get; set; }
	public DbSet<Post> Posts { get; set; }
	public DbSet<PostContent> PostContents { get; set; }
	public DbSet<PostAuthor> PostAuthors { get; set; }
	public DbSet<PostCategory> PostCategories { get; set; }
	public DbSet<PostComment> PostComments { get; set; }
	public DbSet<PostTag> PostTags { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<UserProfile> UserProfiles { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Post>()
				   .HasMany(e => e.Tags)
				   .WithMany(e => e.Posts);

		modelBuilder.Entity<Post>()
				   .HasMany(e => e.Authors)
				   .WithMany(e => e.Posts);

		modelBuilder.Entity<Post>()
				   .HasMany(e => e.Categories)
				   .WithMany(e => e.Posts);

		modelBuilder.Entity<Post>()
				   .HasMany(e => e.Comments)
				   .WithMany(e => e.Posts);


		modelBuilder.Entity<Menu>().HasData(new Menu
		{
			Id = 1,
			Name = "Меню в шапке",
		});
		modelBuilder.Entity<Menu>().HasData(new Menu
		{
			Id = 2,
			Name = "Меню в подвале",
		});
        modelBuilder.Entity<Role>().HasData(new Role
        {
            Id = 1,
            RoleName = RoleName.SuperAdmin,
        }, new Role
        {
            Id = 2,
            RoleName = RoleName.Admin,
        }, new Role
        {
            Id = 3,
            RoleName = RoleName.Moderator,
        }, new Role
        {
            Id = 4,
            RoleName = RoleName.Basic,
        }
        );
    }
}
