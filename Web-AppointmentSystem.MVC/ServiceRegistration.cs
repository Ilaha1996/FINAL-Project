﻿using Web_AppointmentSystem.MVC.Services.Implementations;
using Web_AppointmentSystem.MVC.Services.Interfaces;

namespace Web_AppointmentSystem.MVC;

public static class ServiceRegistration
{
    public static void RegisterService(this IServiceCollection services)
    {
        services.AddScoped<ICRUDService, CRUDService>();
        services.AddScoped<IAuthService, AuthService>();
    }
}

