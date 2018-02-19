using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace DayNightMode
{
    public class IocContainer
    {
        public static IContainer BaseContainer { get; private set; }

        public static void Build()
        {

            if (BaseContainer == null)
            {
                var builder = new ContainerBuilder();
                builder.RegisterType(typeof(AppInfo)).SingleInstance();
                BaseContainer = builder.Build();
            }
        }

        public static TService Resolve<TService>()
        {
            return BaseContainer.Resolve<TService>();
        }
    }
}
