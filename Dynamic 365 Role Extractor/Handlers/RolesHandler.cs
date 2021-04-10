using CDSCore.Core;
using Dynamic_365_Role_Extractor.Models;
using Dynamic_365_Role_Extractor.Utilities;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_365_Role_Extractor.Handlers
{
    public class RolesHandler
    {        
        
        public RolesHandler(){            
            
        }
        public void SetDisplayNameAndGroup(List<Permission> permissionList,List<EntityMetaData> metaDataList)
        {
            foreach (var p in permissionList)
            {
                var entityMetaData = metaDataList.Where(x => x.LogicalName.ToLower() == p.EntityName.ToLower()
                || x.DisplayName?.ToLower().Replace(" ", "") == p.EntityName.ToLower().Replace(" ", "")).FirstOrDefault();
                if (entityMetaData != null)
                {
                    p.EntityName = entityMetaData.DisplayName;
                    p.GroupName = entityMetaData.GroupName;
                    p.IsMiscPermission = false;
                }
                else
                {
                    if (Utility.MiscPermissionMap.ContainsKey(p.EntityName) ||
                    Utility.MiscPermissionMap.ContainsKey(p.ActualName))
                    {
                        p.EntityName = Utility.MiscPermissionMap.ContainsKey(p.EntityName) ?
                                        Utility.MiscPermissionMap[p.EntityName] :
                                        Utility.MiscPermissionMap[p.ActualName];
                        if (Utility.EntityGroups.ContainsKey(p.EntityName) ||
                            Utility.EntityGroups.ContainsKey(p.ActualName))
                            p.GroupName = Utility.EntityGroups.ContainsKey(p.EntityName) ?
                                    Utility.EntityGroups[p.EntityName] :
                                    Utility.EntityGroups[p.ActualName];
                        if (p.GroupName.IndexOf("Privileges") > -1)
                            p.IsMiscPermission = true;
                    }
                }
            }

        }
        public DataTable CreateTableForSingleSheet(List<Permission> permissionList,bool includeAssign = true)
        {
            var roles = permissionList.Select((x) => x.RoleName).Distinct().ToList();
            roles.Sort();
            DataTable dt = new DataTable();

            dt.Columns.Add("Entity Name");
            dt.Columns.Add("Action");
            foreach (var role in roles)
                dt.Columns.Add(role);

            DataRow header = dt.NewRow();
            header[0] = "<b>Entity Name";
            header[1] = "<b>Action";
            int h = 2;
            foreach (var role in roles)
                header[h++] = "<b>" + role;
            dt.Rows.Add(header);


            
            List<string> groupList = Utility.EntityGroups.Values.Distinct().ToList();
            groupList.Add("Custom Entities");

            foreach (var group in groupList)
            {
                DataRow r = dt.NewRow();
                r[0] = "";
                dt.Rows.Add(r);

                r = dt.NewRow();
                r[0] = "<b><m>" + group;
                dt.Rows.Add(r);


                var entities = permissionList.Where(x => x.GroupName == group && x.IsMiscPermission == false).Select((x) => x.EntityName).Distinct();
                foreach (var e in entities)
                {

                    dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Create", "prvCreate"));
                    dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Read", "prvRead"));
                    dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Write", "prvWrite"));
                    dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Delete", "prvDelete"));
                    if (includeAssign)
                    {
                        dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Assign", "prvAssign"));
                        dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Share", "prvShare"));
                    }
                    dt.Rows.Add(dt.NewRow());
                }
                entities = permissionList.Where(x => x.GroupName == group && x.IsMiscPermission == true).Select((x) => x.EntityName).Distinct();
                foreach (var e in entities)
                    dt.Rows.Add(CreateMiscEntry(permissionList, dt.NewRow(), e, roles));
            }
            return dt;
        }
        public DataSet CreateTableByGroup(List<Permission> permissionList, bool includeAssign = true)
        {
            DataSet ds = new DataSet();
            List<string> groupList = Utility.EntityGroups.Values.Distinct().ToList();
            groupList.Add("Custom Entities");

            foreach (var group in groupList)
            {
                var roles = permissionList.Where(y=>y.GroupName == group).Select((x) => x.RoleName).Distinct().ToList();
                roles.Sort();
                DataTable dt = new DataTable(group);

                dt.Columns.Add("Entity Name");
                dt.Columns.Add("Action");
                foreach (var role in roles)
                    dt.Columns.Add(role);

                DataRow header = dt.NewRow();
                header[0] = "<b>Entity Name";
                header[1] = "<b>Action";
                int h = 2;
                foreach (var role in roles)
                    header[h++] = "<b>" + role;
                dt.Rows.Add(header);


                DataRow r = dt.NewRow();
                r[0] = "";
                dt.Rows.Add(r);

                r = dt.NewRow();
                r[0] = "<b><m>" + group;
                dt.Rows.Add(r);


                var entities = permissionList.Where(x => x.GroupName == group && x.IsMiscPermission == false).Select((x) => x.EntityName).Distinct();
                foreach (var e in entities)
                {

                    dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Create", "prvCreate"));
                    dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Read", "prvRead"));
                    dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Write", "prvWrite"));
                    dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Delete", "prvDelete"));
                    if (includeAssign)
                    {
                        dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Assign", "prvAssign"));
                        dt.Rows.Add(CreateEntry(permissionList, dt.NewRow(), e, roles, "Share", "prvShare"));
                    }
                    dt.Rows.Add(dt.NewRow());
                }
                entities = permissionList.Where(x => x.GroupName == group && x.IsMiscPermission == true).Select((x) => x.EntityName).Distinct();
                foreach (var e in entities)
                    dt.Rows.Add(CreateMiscEntry(permissionList, dt.NewRow(), e, roles));

                ds.Tables.Add(dt);
            }
            return ds;
        }
        public void CreateExcel(DataTable dt,string fileName)
        {
            SpreadSheetCreator spreadSheetCreator = new SpreadSheetCreator();            
            spreadSheetCreator.CreateSpreadsheetWorkbook(fileName, dt);
            
        }
        
        
        public void CreateEntry(List<Permission> permissionList,
            StringBuilder sb,
            string entityName,
            IEnumerable<string> roles,
            string action,
            string action2)
        {
            sb.Append(entityName).Append(",");
            sb.Append(action).Append(",");
            foreach (var roleName in roles)
            {

                var entityPermission = permissionList.Where(x => x.EntityName == entityName
                                                        && x.RoleName == roleName
                                                        && x.ActionName == action2).FirstOrDefault();

                if (entityPermission != null)
                {
                    sb.Append(entityPermission.Level).Append(",");
                }
                else
                {
                    sb.Append("N,");
                }
            }
            sb.AppendLine();
        }
        public DataRow CreateEntry(List<Permission> permissionList,
            DataRow r,
            string entityName,
            IEnumerable<string> roles,
            string action,
            string action2)
        {
            r[0] = entityName;
            r[1] = action;
            var counter = 2;
            foreach (var roleName in roles)
            {

                var entityPermission = permissionList.Where(x => x.EntityName == entityName
                                                        && x.RoleName == roleName
                                                        && x.ActionName == action2).FirstOrDefault();

                if (entityPermission != null)
                {
                    r[counter] = entityPermission.Level;
                }
                else
                {
                    r[counter] = "N";
                }
                counter++;
            }
            return r;
        }
        public void CreateMiscEntry(List<Permission> permissionList,
            StringBuilder sb,
            string entityName,
            IEnumerable<string> roles
            )
        {
            sb.Append(entityName).Append(",");
            sb.Append("").Append(",");

            foreach (var roleName in roles)
            {

                var entityPermission = permissionList.Where(x => x.EntityName == entityName
                                                        && x.RoleName == roleName
                                                        && x.IsMiscPermission == true).FirstOrDefault();

                if (entityPermission != null)
                {
                    sb.Append(entityPermission.Level).Append(",");
                }
                else
                {
                    sb.Append("N,");
                }
            }
            sb.AppendLine();
        }
        public DataRow CreateMiscEntry(List<Permission> permissionList,
            DataRow r,
            string entityName,
            IEnumerable<string> roles
            )
        {
            r[0] = entityName;
            r[1] = "";

            var counter = 2;
            foreach (var roleName in roles)
            {

                var entityPermission = permissionList.Where(x => x.EntityName == entityName
                                                        && x.RoleName == roleName
                                                        && x.IsMiscPermission == true).FirstOrDefault();

                if (entityPermission != null)
                {
                    r[counter] = entityPermission.Level;
                }
                else
                {
                    r[counter] = "N";
                }
                counter++;
            }
            return r;
        }
    }
}
