using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CDSCore.Core
{
    public class CDSConnection
    {
        public CDSConnection()
        {
        }
        public IOrganizationService GetService()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            string connectionString = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
            CrmServiceClient conn = new CrmServiceClient(connectionString);
            return (IOrganizationService)conn.OrganizationWebProxyClient != null ?
                (IOrganizationService)conn.OrganizationWebProxyClient :
                (IOrganizationService)conn.OrganizationServiceProxy!=null?
                (IOrganizationService)conn.OrganizationServiceProxy: throw new Exception("Unable to connect to server");
        }
        private System.Security.SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException("missing pwd");

            var securePassword = new System.Security.SecureString();
            foreach (char c in password)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();
            return securePassword;
        }


    }
}
