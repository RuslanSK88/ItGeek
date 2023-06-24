using ItGeek.BLL.Repositories;
using ItGeek.DAL.Data;
using ItGeek.DAL.Interfaces;

namespace ItGeek.BLL;

public class UnitOfWork
{
	private readonly AppDbContext _db;

	public UnitOfWork(AppDbContext db)
    {
		_db = db;
	}

	private AuthorRepository _authorRepository;
	private CategoryRepository _categoryRepository;
	private CommentRepository _commentRepository ;
	private PostRepository _postRepository;
	private RoleRepository _roleRepository ;
	private TagRepository _tagRepository ;
	private UserRepository _userRepository ;
	private UserProfileRepository _userProfileRepository;
    private MenuRepository _menuRepository;
	private MenuItemRepository _menuItemRepository;
    private AuthorsSocialRepository _authorsSocialRepository;
    private PostCategoryRepository _postCategoryRepository;
    private PostAuthorRepository _postAuthorRepository;
    private PostContentRepository _postContentRepository;
	private PostTagRepository _postTagRepository;
	private PostCommentRepository _postCommentRepository;

    public PostCommentRepository PostCommentRepository
    {
        get
        {
            if (_postCommentRepository == null)
            {
                _postCommentRepository = new PostCommentRepository(_db);
            }
            return _postCommentRepository;
        }
    }
    public PostTagRepository PostTagRepository
    {
        get
        {
            if (_postTagRepository == null)
            {
                _postTagRepository = new PostTagRepository(_db);
            }
            return _postTagRepository;
        }
    }

    public AuthorRepository AuthorRepository
	{
		get
		{
			if (_authorRepository == null)
			{
				_authorRepository = new AuthorRepository(_db);
			}
			return _authorRepository;
		}
	}
	public CategoryRepository CategoryRepository
	{
		get
		{
			if (_categoryRepository == null)
			{
				_categoryRepository = new CategoryRepository(_db);
			}
			return _categoryRepository;
		}
	}
	public CommentRepository CommentRepository
	{
		get
		{
			if (_commentRepository == null)
			{
				_commentRepository = new CommentRepository(_db);
			}
			return _commentRepository;
		}
	}
	public PostRepository PostRepository
	{
		get
		{
			if (_postRepository == null)
			{
				_postRepository = new PostRepository(_db);
			}
			return _postRepository;
		}
	}
	public RoleRepository RoleRepository
	{
		get
		{
			if (_roleRepository == null)
			{
				_roleRepository = new RoleRepository(_db);
			}
			return _roleRepository;
		}
	}
	public TagRepository TagRepository
	{
		get
		{
			if (_tagRepository == null)
			{
				_tagRepository = new TagRepository(_db);
			}
			return _tagRepository;
		}
	}
	public UserRepository UserRepository
	{
		get
		{
			if (_userRepository == null)
			{
				_userRepository = new UserRepository(_db);
			}
			return _userRepository;
		}
	}
	public MenuRepository MenuRepository
    {
        get
        {
			if (_menuRepository == null)
			{
                _menuRepository = new MenuRepository(_db);
			}
			return _menuRepository;
		}
	}
	public MenuItemRepository MenuItemRepository
    {
        get
        {
			if (_menuItemRepository == null)
			{
                _menuItemRepository = new MenuItemRepository(_db);
			}
			return _menuItemRepository;
		}
	}
	public AuthorsSocialRepository AuthorsSocialRepository
    {
        get
        {
			if (_authorsSocialRepository == null)
			{
                _authorsSocialRepository = new AuthorsSocialRepository(_db);
			}
			return _authorsSocialRepository;
		}
	}
	public UserProfileRepository UserProfileRepository
    {
        get
        {
			if (_userProfileRepository == null)
			{
                _userProfileRepository = new UserProfileRepository(_db);
			}
			return _userProfileRepository;
		}
	}
	public PostContentRepository PostContentRepository
	{
		get
		{
			if (_postContentRepository == null)
			{
				_postContentRepository = new PostContentRepository(_db);
			}
			return _postContentRepository;
		}
	}
	public PostAuthorRepository PostAuthorRepository
	{
        get
        {
			if (_postAuthorRepository == null)
			{
				_postAuthorRepository = new PostAuthorRepository(_db);
			}
			return _postAuthorRepository;
		}
	}	
	public PostCategoryRepository PostCategoryRepository
    {
        get
        {
			if (_postCategoryRepository == null)
			{
				_postCategoryRepository = new PostCategoryRepository(_db);
			}
			return _postCategoryRepository;
		}
	}
}
