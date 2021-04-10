using Microsoft.Xrm.Sdk;
using CDSCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_365_Role_Extractor.CDSEntities
{
    public static partial class Fields
    {
        public static class SecurityRole
        {
            public const string EntityLogicalName = "role";
            public const string RoleTemplate = "roletemplateid";
            public const string ModifiedByDelegate = "modifiedonbehalfby";
            public const string Solution = "supportingsolutionid";
            public const string CreatedByImpersonator = "createdonbehalfby";
            public const string Name = "name";
            public const string Customizable = "iscustomizable";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string Organization = "organizationid";
            public const string BusinessUnit = "businessunitid";
            public const string Role = "roleid";
            public const string ComponentState = "componentstate";
            public const string UniqueId = "roleidunique";
            public const string Versionnumber = "versionnumber";
            public const string ModifiedBy = "modifiedby";
            public const string CreatedBy = "createdby";
            public const string State = "ismanaged";
            public const string RecordOverwriteTime = "overwritetime";            
            public const string ModifiedOn = "modifiedon";
            public const string ParentRole = "parentroleid";
            public const string CreatedOn = "createdon";
            public const string ParentRootRole = "parentrootroleid";
            public const string RecordCreatedOn = "overriddencreatedon";
        }
    }
    [Serializable]
    public class SecurityRole : TypedEntity
    {
        public SecurityRole() : base()
        {
            LogicalName = Fields.SecurityRole.EntityLogicalName;
        }

        #region Default Columns
        public override List<string> Columns => new List<string>() {
            Fields.SecurityRole.RoleTemplate,
              Fields.SecurityRole.ModifiedByDelegate,              
              Fields.SecurityRole.CreatedByImpersonator,
              Fields.SecurityRole.Name,
              Fields.SecurityRole.Customizable,
              Fields.SecurityRole.ImportSequenceNumber,
              Fields.SecurityRole.Organization,
              Fields.SecurityRole.BusinessUnit,
              Fields.SecurityRole.Role,
              Fields.SecurityRole.ComponentState,
              Fields.SecurityRole.UniqueId,
              Fields.SecurityRole.Versionnumber,
              Fields.SecurityRole.ModifiedBy,
              Fields.SecurityRole.CreatedBy,
              Fields.SecurityRole.State,
              Fields.SecurityRole.RecordOverwriteTime,              
              Fields.SecurityRole.ModifiedOn,
              Fields.SecurityRole.ParentRole,
              Fields.SecurityRole.CreatedOn,
              Fields.SecurityRole.ParentRootRole,
              Fields.SecurityRole.RecordCreatedOn,

        };
        #endregion Default Columns

        public EntityReference RoleTemplate
        {
            get => Entity.GetAttributeValue<EntityReference>(Fields.SecurityRole.RoleTemplate);
            set => Entity.Attributes[Fields.SecurityRole.RoleTemplate] = value;
        }
        public EntityReference ModifiedByDelegate
        {
            get => Entity.GetAttributeValue<EntityReference>(Fields.SecurityRole.ModifiedByDelegate);
            set => Entity.Attributes[Fields.SecurityRole.ModifiedByDelegate] = value;
        }

        public EntityReference CreatedByImpersonator
        {
            get => Entity.GetAttributeValue<EntityReference>(Fields.SecurityRole.CreatedByImpersonator);
            set => Entity.Attributes[Fields.SecurityRole.CreatedByImpersonator] = value;
        }
        public string Name
        {
            get => Entity.GetAttributeValue<string>(Fields.SecurityRole.Name);
            set => Entity.Attributes[Fields.SecurityRole.Name] = value;
        }

        public int ImportSequenceNumber
        {
            get => Entity.GetAttributeValue<int>(Fields.SecurityRole.ImportSequenceNumber);
            set => Entity.Attributes[Fields.SecurityRole.ImportSequenceNumber] = value;
        }
        public Guid Organization
        {
            get => Entity.GetAttributeValue<Guid>(Fields.SecurityRole.Organization);
            set => Entity.Attributes[Fields.SecurityRole.Organization] = value;
        }
        public EntityReference BusinessUnit
        {
            get => Entity.GetAttributeValue<EntityReference>(Fields.SecurityRole.BusinessUnit);
            set => Entity.Attributes[Fields.SecurityRole.BusinessUnit] = value;
        }
        public Guid Role
        {
            get => Entity.GetAttributeValue<Guid>(Fields.SecurityRole.Role);
            set => Entity.Attributes[Fields.SecurityRole.Role] = value;
        }

        public Guid UniqueId
        {
            get => Entity.GetAttributeValue<Guid>(Fields.SecurityRole.UniqueId);
            set => Entity.Attributes[Fields.SecurityRole.UniqueId] = value;
        }
        public long Versionnumber
        {
            get => Entity.GetAttributeValue<long>(Fields.SecurityRole.Versionnumber);
            set => Entity.Attributes[Fields.SecurityRole.Versionnumber] = value;
        }
        public EntityReference ModifiedBy
        {
            get => Entity.GetAttributeValue<EntityReference>(Fields.SecurityRole.ModifiedBy);
            set => Entity.Attributes[Fields.SecurityRole.ModifiedBy] = value;
        }
        public EntityReference CreatedBy
        {
            get => Entity.GetAttributeValue<EntityReference>(Fields.SecurityRole.CreatedBy);
            set => Entity.Attributes[Fields.SecurityRole.CreatedBy] = value;
        }
        public bool State
        {
            get => Entity.GetAttributeValue<bool>(Fields.SecurityRole.State);
            set => Entity.Attributes[Fields.SecurityRole.State] = value;
        }
        public DateTime RecordOverwriteTime
        {
            get => Entity.GetAttributeValue<DateTime>(Fields.SecurityRole.RecordOverwriteTime);
            set => Entity.Attributes[Fields.SecurityRole.RecordOverwriteTime] = value;
        }

        public DateTime ModifiedOn
        {
            get => Entity.GetAttributeValue<DateTime>(Fields.SecurityRole.ModifiedOn);
            set => Entity.Attributes[Fields.SecurityRole.ModifiedOn] = value;
        }
        public EntityReference ParentRole
        {
            get => Entity.GetAttributeValue<EntityReference>(Fields.SecurityRole.ParentRole);
            set => Entity.Attributes[Fields.SecurityRole.ParentRole] = value;
        }
        public DateTime CreatedOn
        {
            get => Entity.GetAttributeValue<DateTime>(Fields.SecurityRole.CreatedOn);
            set => Entity.Attributes[Fields.SecurityRole.CreatedOn] = value;
        }
        public EntityReference ParentRootRole
        {
            get => Entity.GetAttributeValue<EntityReference>(Fields.SecurityRole.ParentRootRole);
            set => Entity.Attributes[Fields.SecurityRole.ParentRootRole] = value;
        }
        public DateTime RecordCreatedOn
        {
            get => Entity.GetAttributeValue<DateTime>(Fields.SecurityRole.RecordCreatedOn);
            set => Entity.Attributes[Fields.SecurityRole.RecordCreatedOn] = value;
        }

    }
}
