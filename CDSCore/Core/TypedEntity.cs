using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDSCore.Core
{    
    public class TypedEntity
    {
        #region Default Columns
        public virtual List<string> Columns { get; set; }
        #endregion

        public TypedEntity()
        {
            Entity = new Entity();
            Columns = new List<string>();
        }
        public Entity Entity { get; set; }

        public string LogicalName
        {
            get => Entity.LogicalName;
            set => Entity.LogicalName = value;
        }

        public Guid Id
        {
            get => Entity.Id;
            set => Entity.Id = value;
        }

        public AttributeCollection Attributes
        {
            get => Entity.Attributes;
            set => Entity.Attributes = value;
        }

        public EntityReference EntityReference
        {
            get => new EntityReference(LogicalName, Entity.Id);
        }


        public void Create(IOrganizationService orgService) => orgService.Create(Entity);
        public void Update(IOrganizationService orgService) => orgService.Update(Entity);
        public void Delete(IOrganizationService orgService) => orgService.Delete(Entity.LogicalName, Entity.Id);


        public virtual System.Nullable<T> GetAttributeValue<T>(string attributeLogicalName) where T : struct
        {
            return Entity.GetAttributeValue<System.Nullable<T>>(attributeLogicalName);

        }
    }
}
