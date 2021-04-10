using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDSCore.Core
{
    public class CDSDataAccess<TEntity> 
        where TEntity : TypedEntity, new()
    {
        public Plugin plugin;
        public ITracingService tracingService;
        public Guid userId;
        public IOrganizationService orgService;
        
        public CDSDataAccess(Plugin plugin)
        {
            this.plugin = plugin;
            tracingService = plugin.tracingService;
            userId = plugin.userId;
            orgService = plugin.orgService;
        }
        public CDSDataAccess(IOrganizationService orgService)
        {
            this.orgService = orgService;
        }
        public TEntity RetrieveById(Guid id, string[] columns=null)
        {
            var t = new TEntity();
            if (columns == null) {
                columns = t.Columns.ToArray();
            }            
            return orgService.Retrieve(t.LogicalName, id, new ColumnSet(columns))?.ToTypedEntity<TEntity>();
        }
        public TEntity RetrieveSingle(string attributeName, object attributeValue, string[] columns = null)
        {
            var t = new TEntity();
            if (columns == null)
            {
                columns = t.Columns.ToArray();
            }
            var query = new QueryExpression {
                EntityName = t.LogicalName,
                ColumnSet = new ColumnSet(columns)
            };
            query.Criteria.AddCondition(new ConditionExpression(attributeName, ConditionOperator.Equal, attributeValue));
            var lst = orgService.RetrieveMultiple(query);
            return lst?.Entities?.Count > 0 ? lst.Entities[0].ToTypedEntity<TEntity>() : null;
        }
        public List<TEntity> RetrieveMultiple(string attributeName, object attributeValue, string[] columns = null)
        {
            var t = new TEntity();
            if (columns == null)
            {
                columns = t.Columns.ToArray();
            }
            var query = new QueryExpression
            {
                EntityName = t.LogicalName,
                ColumnSet = new ColumnSet(columns)
            };
            query.Criteria.AddCondition(new ConditionExpression(attributeName, ConditionOperator.Equal, attributeValue));
            var lst = orgService.RetrieveMultiple(query);
            return lst?.Entities!=null ? lst.Entities.Select(x=>  x.ToTypedEntity<TEntity>() ).ToList() : null;
        }
        public List<TEntity> RetrieveMultiple(ConditionExpression expression, string[] columns = null)
        {
            var t = new TEntity();
            if (columns == null)
            {
                columns = t.Columns.ToArray();
            }
            var query = new QueryExpression
            {
                EntityName = t.LogicalName,
                ColumnSet = new ColumnSet(columns)
            };
            query.Criteria.AddCondition(expression);
            var lst = orgService.RetrieveMultiple(query);
            return lst?.Entities != null ? lst.Entities.Select(x => x.ToTypedEntity<TEntity>()).ToList() : null;
        }
        public List<TEntity> RetrieveMultiple(QueryExpression expression)
        {
            var lst = orgService.RetrieveMultiple(expression);
            return lst?.Entities != null ? lst.Entities.Select(x => x.ToTypedEntity<TEntity>()).ToList() : null;
        }
        public List<TEntity> RetrieveMultiple(string[] columns = null)
        {
            var t = new TEntity();
            if (columns == null)
            {
                columns = t.Columns.ToArray();
            }
            var query = new QueryExpression
            {
                EntityName = t.LogicalName,
                ColumnSet = new ColumnSet(columns)
            };
            var lst = orgService.RetrieveMultiple(query);
            return lst?.Entities?.Count > 0 ? lst.Entities.Select(x => x.ToTypedEntity<TEntity>()).ToList() : null;
        }
        public void Create(TEntity entity) => entity.Id = orgService.Create(entity.Entity);

    }
}
