using CDSCore.Core;
using Dynamic_365_Role_Extractor.Models;
using Dynamic_365_Role_Extractor.Utilities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_365_Role_Extractor.Handlers
{
    public class MetaDataHandler
    {
        private IOrganizationService service;

        public MetaDataHandler(IOrganizationService service)
        {
            this.service = service;
        }

        public List<EntityMetaData> GetEntitiesMetDataList()
        {
            List<EntityMetaData> list = new List<EntityMetaData>();
            EntityMetadata[] entities = GetEntitiesMetData();

            foreach (var metaEntity in entities)
            {
                EntityMetaData entity = new EntityMetaData();
                entity.LogicalName = metaEntity.LogicalName;
                entity.DisplayName = metaEntity?.DisplayName?.UserLocalizedLabel?.Label;
                entity.SchemaName = metaEntity.SchemaName;
                entity.FieldsCount = metaEntity.Attributes != null ? metaEntity.Attributes.Length : 0;
                entity.ObjectTypeCode = metaEntity.ObjectTypeCode;
                entity.GroupName = entity.DisplayName!=null && Utility.EntityGroups.ContainsKey(entity.DisplayName) && metaEntity.LogicalName.IndexOf("_")<0  ?
                                   Utility.EntityGroups[entity.DisplayName]: "Custom Entities";
                list.Add(entity);
                
            }
            return list;
        }
        public  EntityMetadata[] GetEntitiesMetData()
        {            
            RetrieveAllEntitiesRequest metaDataRequest = new RetrieveAllEntitiesRequest();
            RetrieveAllEntitiesResponse metaDataResponse = new RetrieveAllEntitiesResponse();
            metaDataRequest.EntityFilters = EntityFilters.Entity;
            metaDataResponse = (RetrieveAllEntitiesResponse)service.Execute(metaDataRequest);
            var entitiesMetaData = metaDataResponse.EntityMetadata;
            return entitiesMetaData;
        }
    }
   
}
