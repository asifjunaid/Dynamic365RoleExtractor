using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDSCore.Core
{
    public static class PluginContextMessageName
    {
        public const string Create = "Create";
        public const string Update = "Update";
        public const string Assign = "Assign";
        public const string Retrieve = "Retrieve";
        public const string RetrieveMultiple = "RetrieveMultiple";
    }
    public static class PluginContextStage
    {
        public const int PreValidation = 10;
        public const int PreOperation = 20;
        public const int PostOperation = 40;
    }
}
