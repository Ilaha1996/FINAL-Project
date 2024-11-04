﻿using Web.AppointmentSystem.DATA.DAL;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web.AppointmentSystem.DATA.Repostories;

public class ServiceImageRepo : GenericRepo<ServiceImage>, IServiceImageRepo
{
    public ServiceImageRepo(AppDbContext context) : base(context)
    {
    }
}

