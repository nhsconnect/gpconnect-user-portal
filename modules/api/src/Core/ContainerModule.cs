﻿using Autofac;
using GpConnect.NationalDataSharingPortal.Api.Dal;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Service;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators;

namespace GpConnect.NationalDataSharingPortal.Api.Core
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DataService>().As<IDataService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TransparencySiteRequestValidator>().As<ITransparencySiteRequestValidator>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TransparencySiteService>().As<ITransparencySiteService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<CareSettingRequestValidator>().As<ICareSettingRequestValidator>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<CareSettingService>().As<ICareSettingService>().InstancePerLifetimeScope();
        }
    }
}
