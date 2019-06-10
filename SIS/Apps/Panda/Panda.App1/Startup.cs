using System;
using Panda.Data;
using SIS.MvcFramework;
using SIS.MvcFramework.DependencyContainer;
using SIS.MvcFramework.Routing;
using IServiceProvider = SIS.MvcFramework.DependencyContainer.IServiceProvider;

namespace Panda.App
{
    public class Startup : IMvcApplication
    {
        public Void Configure(IServerRoutingTable serverRoutingTable)
        {
            using (var context = new PandaDbContext())
            {
                context.Database.EnsureCreated();
            }
        }

        public Void ConfigureServices(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}