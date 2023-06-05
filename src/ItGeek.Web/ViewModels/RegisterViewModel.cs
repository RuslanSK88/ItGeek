using System.ComponentModel.DataAnnotations;

namespace ItGeek.Web.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "Почта")]
    [Required(ErrorMessage = "Не указан Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Подтверждение пароля")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Не указана проверка пароля")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
}
