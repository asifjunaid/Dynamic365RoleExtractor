using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_365_Role_Extractor.Models
{   
    public class Permission
    {
        public string RoleName { get; set; }
        public string EntityName { get; set; }
        public string ActionName { get; set; }
        public string ActualName { get; set; }
        public string Level { get; set; }
        public string GroupName { get; set; }
        public Guid RoleId { get; set; }
        public Guid PreviligeId { get; set; }
        public bool IsMiscPermission { get; set; }
    }
}
