using CDSCore.Core;
using Dynamic_365_Role_Extractor.CDSEntities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_365_Role_Extractor.CDSDataAccess
{
    public class SecurityRoleAccess : CDSDataAccess<SecurityRole>
    {
        public SecurityRoleAccess(IOrganizationService orgService) : base(orgService){
        }
        public List<SecurityRole> GetSecurityRoles() =>  RetrieveMultiple(new ConditionExpression(Fields.SecurityRole.ParentRole, ConditionOperator.Null));            
    }
}
