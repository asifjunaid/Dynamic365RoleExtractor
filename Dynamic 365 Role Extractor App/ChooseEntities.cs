using Dynamic_365_Role_Extractor.Handlers;
using Dynamic_365_Role_Extractor.Models;
using Dynamic_365_Role_Extractor.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dynamic_365_Role_Extractor_App
{
    public partial class ChooseEntities : Form
    {
        List<EntityMetaData> lst;
        List<string> selectedEntities;

        public ChooseEntities(List<string>  selectedEntities , List<EntityMetaData> lst, MetaDataHandler metaDataHandler)
        {
            this.selectedEntities = selectedEntities;
            this.selectedEntities.Clear();
            this.lst = lst;
            InitializeComponent();
            
            List<string> groupList = Utility.EntityGroups.Values.Distinct().ToList();
            
            foreach (var group in groupList)
            {
                var titles = Utility.EntityGroups.Where(x => x.Value == group).Select(y => y.Key).ToList();
                TreeNode parent = new TreeNode();
                parent.Name = group;
                parent.Text = group;
                //parent.Checked = true;
                foreach (var e in titles)
                {
                    TreeNode mainNode = new TreeNode();
                    mainNode.Name = e;
                    mainNode.Text = e;
                    //mainNode.Checked = true;
                    parent.Nodes.Add(mainNode);                    
                }
                this.treeView1.Nodes.Add(parent);
            }

            TreeNode customNode = new TreeNode();
            customNode.Name = "Custom Entities";
            customNode.Text = "Custom Entities";
            //customNode.Checked = true;
            var tempList = lst.Where(x=>x.GroupName=="Custom Entities" && x.DisplayName!=null && x.DisplayName.Length>0).OrderBy(x => x.DisplayName).ToList();
            foreach (var e in tempList)
            {
                TreeNode mainNode = new TreeNode();
                mainNode.Name = e.DisplayName;
                mainNode.Text = e.DisplayName;
                //mainNode.Checked = true;
                customNode.Nodes.Add(mainNode);
            }
            this.treeView1.Nodes.Add(customNode);

            var prevSelectedEntities = FileManager.ReadFromFile<List<string>>("PrevSelectedEntities");
            if (prevSelectedEntities?.Count > 0)
            {
                foreach (TreeNode node in this.treeView1.Nodes)
                {
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        if (prevSelectedEntities.Where(x => x == childNode.Text).Count() > 0)
                            childNode.Checked = true; 
                        else
                            childNode.Checked = false; 
                    }
                }
            }


        }
        private void ChooseEntities_Load(object sender, EventArgs e)
        {

        }
        private void cbEntities_CheckedChanged(object sender, EventArgs e)
        {
            bool checked_ = cbEntities.Checked;
            foreach (TreeNode node in this.treeView1.Nodes)
            {
                node.Checked = checked_;
                foreach (TreeNode childNode in node.Nodes)
                    childNode.Checked = checked_;

            }
        }

        private void btnOK_Click(object sender, EventArgs e){
            foreach (TreeNode node in this.treeView1.Nodes){
                foreach (TreeNode childNode in node.Nodes)
                    if (childNode.Checked) selectedEntities.Add(childNode.Text);
            }
            FileManager.WriteToFile(selectedEntities, "PrevSelectedEntities");
            this.Close();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes?.Count > 0) {
                bool checked_ = e.Node.Checked;
                foreach (TreeNode childNode in e.Node.Nodes)
                    childNode.Checked = checked_;
            }
        }

        
    }
}
