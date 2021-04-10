using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDSCore.Core
{
    public class Plugin : IPlugin
    {
        public IServiceProvider serviceProvider;
        public IPluginExecutionContext pluginExecutionContext;
        public ITracingService tracingService;
        public Guid userId;
        public IOrganizationService orgService;

        public void Execute(IServiceProvider serviceProvider)
        {

            this.serviceProvider = serviceProvider;
            pluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            userId = ((IExecutionContext)pluginExecutionContext).UserId;
            orgService = ((IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(new Guid?(userId));


            tracingService.Trace("CDSCore.Core.Plugin.Execute");
            ExecutePlugin();
        }
        public virtual void ExecutePlugin()
        {
            tracingService.Trace("CDSCore.Core.Plugin.ExecutePlugin");
        }
    }
}
