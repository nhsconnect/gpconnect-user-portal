﻿using Autofac;

namespace gpconnect_user_portal.Framework.Configuration.Infrastructure
{
    public static class ContainerExtensions
    {
        public static void ConfigureContainer(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new ContainerModule());
        }
    }
}
