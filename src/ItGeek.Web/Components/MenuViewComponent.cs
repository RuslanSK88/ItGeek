using ItGeek.BLL;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Components;

public class MenuViewComponent : ViewComponent
{
    private readonly UnitOfWork _uow;

    public MenuViewComponent(UnitOfWork uow)
    {
        _uow = uow;
    }
	public async Task<IViewComponentResult> InvokeAsync()
	{
		// Здесь вы можете добавить логику для получения данных для вашего меню
		var menuItems = await _uow.MenuItemRepository.GetByMenuIdAsync(1);
		return View(menuItems);
	}
}
