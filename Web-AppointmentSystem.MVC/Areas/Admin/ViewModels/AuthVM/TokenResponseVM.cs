﻿namespace Web_AppointmentSystem.MVC.Areas.Admin.ViewModels;

public class TokenResponseVM
{
    public string AccessToken { get; set; }
    public DateTime ExpireDate { get; set; }
}
