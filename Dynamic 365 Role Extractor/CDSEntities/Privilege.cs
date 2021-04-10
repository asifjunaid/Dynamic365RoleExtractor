using CDSCore.Core;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_365_Role_Extractor.CDSEntities
{
    public static partial class Fields
    {
        public static class Privilege
        {
            public const string EntityLogicalName = "privilege";
            public const string CanBeBasic = "canbebasic";
            public const string CanBeDeep = "canbedeep";
            public const string PrivilegeId = "privilegeid";
            public const string AccessRight = "accessright";
            public const string VersionNumber = "versionnumber";
            public const string CanBeLocal = "canbelocal";
            public const string CanBeGlobal = "canbeglobal";
            public const string CanBeEntityReference = "canbeentityreference";
            public const string IsDisabledWhenIntegrated = "isdisabledwhenintegrated";
            public const string CanBeParentEntityReference = "canbeparententityreference";
            public const string Name = "name";

        }
    }
    public class Privilege : TypedEntity
    {
        public Privilege() : base()
        {
            LogicalName = Fields.Privilege.EntityLogicalName;
        }

        #region Default Columns
        public override List<string> Columns => new List<string>() {
            Fields.Privilege.CanBeBasic,
              Fields.Privilege.CanBeDeep,
              Fields.Privilege.PrivilegeId,
              Fields.Privilege.AccessRight,
              Fields.Privilege.VersionNumber,
              Fields.Privilege.CanBeLocal,
              Fields.Privilege.CanBeGlobal,
              Fields.Privilege.CanBeEntityReference,
              Fields.Privilege.IsDisabledWhenIntegrated,
              Fields.Privilege.CanBeParentEntityReference,
              Fields.Privilege.Name,

        };
        #endregion Default Columns

        public bool CanBeBasic
        {
            get => Entity.GetAttributeValue<bool>(Fields.Privilege.CanBeBasic);
            set => Entity.Attributes[Fields.Privilege.CanBeBasic] = value;
        }
        public bool CanBeDeep
        {
            get => Entity.GetAttributeValue<bool>(Fields.Privilege.CanBeDeep);
            set => Entity.Attributes[Fields.Privilege.CanBeDeep] = value;
        }
        public Guid PrivilegeId
        {
            get => Entity.GetAttributeValue<Guid>(Fields.Privilege.PrivilegeId);
            set => Entity.Attributes[Fields.Privilege.PrivilegeId] = value;
        }
        public int AccessRight
        {
            get => Entity.GetAttributeValue<int>(Fields.Privilege.AccessRight);
            set => Entity.Attributes[Fields.Privilege.AccessRight] = value;
        }
        public long VersionNumber
        {
            get => Entity.GetAttributeValue<long>(Fields.Privilege.VersionNumber);
            set => Entity.Attributes[Fields.Privilege.VersionNumber] = value;
        }
        public bool CanBeLocal
        {
            get => Entity.GetAttributeValue<bool>(Fields.Privilege.CanBeLocal);
            set => Entity.Attributes[Fields.Privilege.CanBeLocal] = value;
        }
        public bool CanBeGlobal
        {
            get => Entity.GetAttributeValue<bool>(Fields.Privilege.CanBeGlobal);
            set => Entity.Attributes[Fields.Privilege.CanBeGlobal] = value;
        }
        public bool CanBeEntityReference
        {
            get => Entity.GetAttributeValue<bool>(Fields.Privilege.CanBeEntityReference);
            set => Entity.Attributes[Fields.Privilege.CanBeEntityReference] = value;
        }
        public bool IsDisabledWhenIntegrated
        {
            get => Entity.GetAttributeValue<bool>(Fields.Privilege.IsDisabledWhenIntegrated);
            set => Entity.Attributes[Fields.Privilege.IsDisabledWhenIntegrated] = value;
        }
        public bool CanBeParentEntityReference
        {
            get => Entity.GetAttributeValue<bool>(Fields.Privilege.CanBeParentEntityReference);
            set => Entity.Attributes[Fields.Privilege.CanBeParentEntityReference] = value;
        }
        public string Name
        {
            get => Entity.GetAttributeValue<string>(Fields.Privilege.Name);
            set => Entity.Attributes[Fields.Privilege.Name] = value;
        }

    }
}
