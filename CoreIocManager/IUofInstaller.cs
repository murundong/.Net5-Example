using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreEntityFramework.Uow;
using System;

namespace CoreIocManager
{
    public class IUofInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IUnitOfWork<>)).ImplementedBy(typeof(UnitOfWork<>)));
        }
    }
}
