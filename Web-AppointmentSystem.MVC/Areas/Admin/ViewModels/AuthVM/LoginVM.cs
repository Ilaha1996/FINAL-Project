using System.ComponentModel.DataAnnotations;

namespace Web_AppointmentSystem.MVC.Areas.Admin.ViewModels;
public record LoginVM
{
    [Required]
    public string Username { get; init; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; }

    public bool RememberMe { get; init; }
}


