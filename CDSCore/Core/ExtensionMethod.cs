using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDSCore.Core
{
    public static class ExtensionMethod
    {
        public static T ToTypedEntity<T>(this Entity entity) where T : TypedEntity, new()
        {
            if (string.IsNullOrWhiteSpace(entity.LogicalName))
            {
                throw new NotSupportedException("logical name not set!");
            }

            var resultEntity = new T();
            resultEntity.Entity = entity;
            return resultEntity;
        }

        public static Entity GetTargetEntity(this IPluginExecutionContext context)
        {
            Entity entity = null;
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
                entity = (Entity)context.InputParameters["Target"];
            return entity;
        }
        public static T GetTargetEntity<T>(this IPluginExecutionContext context) where T : TypedEntity, new()
        {
            T typedEntity = null;
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                typedEntity = entity.ToTypedEntity<T>();
            }
            return typedEntity;
        }
    }
}
