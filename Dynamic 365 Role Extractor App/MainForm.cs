using CDSCore.Core;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Dynamic_365_Role_Extractor.Utilities;
using Dynamic_365_Role_Extractor.CDSEntities;
using Dynamic_365_Role_Extractor.Models;
using Dynamic_365_Role_Extractor.CDSDataAccess;
using Dynamic_365_Role_Extractor.Handlers;
using LoginControl.LoginWindow;

namespace Dynamic_365_Role_Extractor_App
{
    public partial class MainForm : Form
    {
        CDSConnection conn;
        IOrganizationService orgService;
        List<SecurityRole> roles = new List<SecurityRole>();
        List<EntityMetaData> metaDataList = new List<EntityMetaData>();        
        bool isConnecting = false;
        bool isConnected = false;
        bool includeAppend = true;
        bool includeAssign = true;
        bool isSystemAdminSelected = false;
        bool includeAllEntities = false;
        string SystemAdminRoleName = "System Administrator";
        public MainForm()
        {
            InitializeComponent();
            Console.SetOut(new ConsoleWriter(this.listView1));

            //StringBuilder sb = new StringBuilder();
            //foreach (KeyValuePair<string, string> kvp in Utility.EntityGroups)
            //    sb.AppendLine($"{kvp.Key},{kvp.Value}");

            //FileManager.WriteToCSVFile(sb, "EntityGroups.csv");

            //sb = new StringBuilder();
            //foreach (KeyValuePair<string, string> kvp in Utility.MiscPermissionMap)
            //    sb.AppendLine($"{kvp.Key},{kvp.Value}");

            //FileManager.WriteToCSVFile(sb, "MiscPermissionMap.csv");
            
            Utility.EntityGroups.Clear();
            Utility.MiscPermissionMap.Clear();
            foreach (string s in FileManager.ReadFromCSVFile("EntityGroups.csv"))
                Utility.EntityGroups.Add(s.Split(',')[0], s.Split(',')[1]);

            foreach (string s in FileManager.ReadFromCSVFile("MiscPermissionMap.csv"))
                Utility.MiscPermissionMap.Add(s.Split(',')[0], s.Split(',')[1]);
            
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {            
            if (!isConnecting)
            {
                isConnecting = true;
                Console.WriteLine("Trying to connect!");
                CrmLogin ctrl = new CrmLogin();
                ctrl.ConnectionToCrmCompleted += Ctrl_ConnectionToCrmCompleted;
                ctrl.Closed += Ctrl_Closed;
                ctrl.ShowDialog();
                //await Task.Run(() =>
                //{
                //    conn = new CDSConnection();
                //    orgService = conn.GetService();
                //    isConnected = true;
                //    this.BeginInvoke((Action)(() =>
                //            {
                //                tsConnectionStatus.Text = "Connected";
                //                Console.WriteLine("Connected to Organization Service!");
                //            }
                //    ));                    
                //    isConnecting = false;
                //});
            }
        }

        private void Ctrl_Closed(object sender, EventArgs e)
        {
            isConnecting = false;
        }

        private void Ctrl_ConnectionToCrmCompleted(object sender, EventArgs e)
        {
            if (sender is CrmLogin)
            {
                if (((CrmLogin)sender).CrmConnectionMgr?.CrmSvc?.OrganizationServiceProxy!=null)
                {
                    isConnected = true;
                    orgService = ((CrmLogin)sender).CrmConnectionMgr.CrmSvc.OrganizationServiceProxy;
                }
                ((CrmLogin)sender).Close();                
                tsConnectionStatus.Text = "Connected";
                Console.WriteLine("Connected to Organization Service!");
            }
        }
        private async void btnFetch_Click(object sender, EventArgs e)
        {
            if (!isConnected) { MessageBox.Show("Please connect to crm oganization firts!","Error"); return; }
            btnFetch.Enabled = false;
            Console.WriteLine("Fetching Roles!");
            roles.Clear();
            this.treeView1.Nodes.Clear();
            await Task.Run(() =>
            {
                SecurityRoleAccess roleAccess = new SecurityRoleAccess(orgService);
                roles = roleAccess.GetSecurityRoles().OrderBy(x => x.Name).ToList();

                IAsyncResult result = this.BeginInvoke((Action)(() =>
                {
                    foreach (var role in roles)
                    {
                        TreeNode mainNode = new TreeNode();
                        mainNode.Name = role.Id.ToString();
                        mainNode.Text = role.Name;
                        //mainNode.Checked = true;
                        this.treeView1.Nodes.Add(mainNode);
                    }
                    btnFetch.Enabled = true;
                }
                ));
                result.AsyncWaitHandle.WaitOne();
                var prevSelectedRoles= FileManager.ReadFromFile<List<string>>("PrevSelectedRoles");
                if (prevSelectedRoles?.Count > 0)
                {                                  
                    foreach (TreeNode node in this.treeView1.Nodes)
                    {
                        if (prevSelectedRoles.Where(x => x == node.Name).Count() > 0)
                            this.BeginInvoke((Action)(() => { node.Checked = true; }));
                        else
                            this.BeginInvoke((Action)(() => { node.Checked = false; }));
                    }
                }
            });
            Console.WriteLine($"{roles.Count()} Security Roles Found!");
        }
        private async void btnGenerateSheet_Click(object sender, EventArgs e)
        {
            if (!isConnected) { MessageBox.Show("Please connect to crm oganization firts!","Error"); return; }
            includeAllEntities = cbIncludeAllEntities.Checked;
            List<string> selectedEntities = new List<string>();
            includeAppend = cbAppend.Checked;
            includeAssign = cbAssign.Checked;
            btnGenerateSheet.Enabled = false;
            var tempRoles = new List<SecurityRole>(roles); 
            foreach (TreeNode node in this.treeView1.Nodes){
                if (!node.Checked)
                    tempRoles.Remove(tempRoles.Where(x => x.Id.ToString() == node.Name).FirstOrDefault());                
            }
            await Task.Run(() =>
            {
                FileManager.WriteToFile(tempRoles.Select(x=>x.Id.ToString()).ToList(), "PrevSelectedRoles");
            });
            Console.WriteLine($"Fetching Entity Meta Data!");
            await Task.Run(() =>
            {
                MetaDataHandler metaDataHandler = new MetaDataHandler(orgService);
                if (metaDataList.Count == 0)
                    metaDataList = metaDataHandler.GetEntitiesMetDataList();

                IAsyncResult result = this.BeginInvoke((Action)(() =>
                {
                    
                    ChooseEntities chooseEntities = new ChooseEntities(selectedEntities, metaDataList, metaDataHandler);
                    chooseEntities.ShowDialog();
                }
                ));
                result.AsyncWaitHandle.WaitOne();
                
                List<Permission> permissionList = new List<Permission>();

                if (includeAllEntities)
                {
                    if (tempRoles.Where(x => x.Name == SystemAdminRoleName).Count() == 0)
                        tempRoles.Add(roles.Where(x => x.Name == SystemAdminRoleName).FirstOrDefault());
                    else
                        isSystemAdminSelected = true;
                }
                foreach (var role in tempRoles)
                {                   
                        PrivilegeAccess privilegeAccess = new PrivilegeAccess(orgService);
                        privilegeAccess.GetRolePermission(role, permissionList);

                        result = this.BeginInvoke((Action)(() =>
                        {
                            Console.WriteLine($"Fetching {role.Name} Security Role Details");
                        }
                        ));
                        result.AsyncWaitHandle.WaitOne();
                }
                Console.WriteLine($"Creating File!");

                //foreach (var a in metaDataList) {
                //    Console.WriteLine(a.LogicalName +" "+ a.DisplayName +" " + a.SchemaName + " "+ a.GroupName);
                //}

                var fileName = string.Empty;
                result =  this.BeginInvoke((Action)(() =>
                {
                    var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog.DefaultExt = "xlsx";
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    saveFileDialog.ShowDialog();
                    fileName = saveFileDialog.FileName;
                }
                ));
                result.AsyncWaitHandle.WaitOne();
                if (fileName.Length > 0)
                {
                    RolesHandler rolesHandler = new RolesHandler();
                    rolesHandler.SetDisplayNameAndGroup(permissionList, metaDataList);
                    permissionList = permissionList.Where(x => selectedEntities.Contains(x.EntityName)).ToList();
                    permissionList = permissionList.OrderBy(x => x.EntityName).ThenBy(y => y.ActionName).ToList();

                    var dt = rolesHandler.CreateTableForSingleSheet(permissionList);
                    if (isSystemAdminSelected == false)
                        if (dt.Columns.Contains(SystemAdminRoleName))
                            dt.Columns.Remove(SystemAdminRoleName);
                    rolesHandler.CreateExcel(dt, fileName);
                }


                this.BeginInvoke((Action)(() =>
                {
                    btnGenerateSheet.Enabled = true;
                }
                )); 
            });
        }
        private void cbRoles_CheckedChanged(object sender, EventArgs e)
        {
            bool checked_ = cbRoles.Checked;
            foreach (TreeNode node in this.treeView1.Nodes)            
                node.Checked = checked_;            
        }
        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (sender != listView1) return;
                if (e.Control && e.KeyCode == Keys.C)
                {
                    var builder = new StringBuilder();
                    foreach (ListViewItem item in listView1.SelectedItems)
                        builder.AppendLine(item.SubItems[0].Text);

                    Clipboard.SetText(builder.ToString());
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
