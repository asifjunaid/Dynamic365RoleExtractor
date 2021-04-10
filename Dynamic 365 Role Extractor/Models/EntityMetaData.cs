using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_365_Role_Extractor.Models
{
    public class EntityMetaData
    {
        public string DisplayName { get; set; }
        public string LogicalName { get; set; }
        public string SchemaName { get; set; }
        public int FieldsCount { get; set; }
        public int? ObjectTypeCode { get; set; }
        public string GroupName { get; set; }
    }
}
