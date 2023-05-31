using System.ComponentModel.DataAnnotations;

namespace ItGeek.Web.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Не указан Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
