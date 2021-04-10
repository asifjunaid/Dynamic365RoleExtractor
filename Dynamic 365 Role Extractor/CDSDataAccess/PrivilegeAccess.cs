using CDSCore.Core;
using Dynamic_365_Role_Extractor.CDSEntities;
using Dynamic_365_Role_Extractor.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_365_Role_Extractor.CDSDataAccess
{
    public class PrivilegeAccess : CDSDataAccess<Privilege>
    {
        public PrivilegeAccess(IOrganizationService orgService) : base(orgService){
        }
        public void GetRolePermission(SecurityRole role, List<Permission> lst)
        {
            List<Permission> permissionList = new List<Permission>();

            QueryExpression privilegeQuery_ = null;
            privilegeQuery_ = new QueryExpression { EntityName = "roleprivileges", ColumnSet = new ColumnSet(new string[] { "privilegedepthmask" }) };
            privilegeQuery_.Criteria = new FilterExpression(LogicalOperator.And);
            privilegeQuery_.Criteria.AddCondition("roleid", ConditionOperator.Equal, role.Id);
            privilegeQuery_.LinkEntities.Add(new LinkEntity("roleprivileges", "privilege", "privilegeid",
                "privilegeid", JoinOperator.Natural)
            {
                Columns = new ColumnSet(new string[2] { "name", "privilegeid" }),
                EntityAlias = "p"
            });
            var rpList = orgService.RetrieveMultiple(privilegeQuery_);
            int level = -1;
            if (rpList.Entities.Count > 0)
            {
                foreach (var entity in rpList.Entities)
                {                    
                    Guid roleid = role.Id;
                    Guid privilegeid = (Guid)entity.GetAttributeValue<Microsoft.Xrm.Sdk.AliasedValue>("p.privilegeid").Value;

                    Permission permission = new Permission();
                    permission.RoleId = role.Id;
                    permission.PreviligeId = privilegeid;

                    var name = entity.GetAttributeValue<Microsoft.Xrm.Sdk.AliasedValue>("p.name").Value.ToString();
                    permission.ActualName = name;
                    permission.EntityName = name.Replace("prvRead", "").Replace("prvWrite", "").Replace("prvCreate", "")
                        .Replace("prvAppendTo", "").Replace("prvAppend", "").Replace("prvDelete", "")
                        .Replace("prvShare", "").Replace("prvAssign", "");

                    permission.ActionName = name.Replace(permission.EntityName, "");

                    permission.RoleName = role.Name;

                    level = entity.GetAttributeValue<int>("privilegedepthmask");

                    permission.Level = level == 8 ? "O" :
                                level == 4 ? "PCBU" :
                                level == 2 ? "BU" :
                                level == 1 ? "U" :
                                "N";

                    permissionList.Add(permission);
                }
            }            
            lst.AddRange(permissionList);
        }
    }
}
