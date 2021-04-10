namespace Dynamic_365_Role_Extractor_App
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnFetch = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnGenerateSheet = new System.Windows.Forms.Button();
            this.cbAppend = new System.Windows.Forms.CheckBox();
            this.cbAssign = new System.Windows.Forms.CheckBox();
            this.cbRoles = new System.Windows.Forms.CheckBox();
            this.cbIncludeAllEntities = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnFetch
            // 
            this.btnFetch.Location = new System.Drawing.Point(107, 12);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(75, 23);
            this.btnFetch.TabIndex = 1;
            this.btnFetch.Text = "Fetch Roles";
            this.btnFetch.UseVisualStyleBackColor = true;
            this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(12, 58);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(231, 363);
            this.treeView1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsConnectionStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsConnectionStatus
            // 
            this.tsConnectionStatus.Name = "tsConnectionStatus";
            this.tsConnectionStatus.Size = new System.Drawing.Size(79, 17);
            this.tsConnectionStatus.Text = "Disconnected";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(249, 58);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(539, 363);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Message";
            this.columnHeader1.Width = 600;
            // 
            // btnGenerateSheet
            // 
            this.btnGenerateSheet.Location = new System.Drawing.Point(203, 12);
            this.btnGenerateSheet.Name = "btnGenerateSheet";
            this.btnGenerateSheet.Size = new System.Drawing.Size(100, 23);
            this.btnGenerateSheet.TabIndex = 5;
            this.btnGenerateSheet.Text = "Generate Sheets";
            this.btnGenerateSheet.UseVisualStyleBackColor = true;
            this.btnGenerateSheet.Click += new System.EventHandler(this.btnGenerateSheet_Click);
            // 
            // cbAppend
            // 
            this.cbAppend.AutoSize = true;
            this.cbAppend.Checked = true;
            this.cbAppend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAppend.Location = new System.Drawing.Point(249, 41);
            this.cbAppend.Name = "cbAppend";
            this.cbAppend.Size = new System.Drawing.Size(165, 17);
            this.cbAppend.TabIndex = 7;
            this.cbAppend.Text = "Include Append / Append To";
            this.cbAppend.UseVisualStyleBackColor = true;
            // 
            // cbAssign
            // 
            this.cbAssign.AutoSize = true;
            this.cbAssign.Checked = true;
            this.cbAssign.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAssign.Location = new System.Drawing.Point(420, 41);
            this.cbAssign.Name = "cbAssign";
            this.cbAssign.Size = new System.Drawing.Size(134, 17);
            this.cbAssign.TabIndex = 8;
            this.cbAssign.Text = "Include Assign / Share";
            this.cbAssign.UseVisualStyleBackColor = true;
            // 
            // cbRoles
            // 
            this.cbRoles.AutoSize = true;
            this.cbRoles.Location = new System.Drawing.Point(12, 41);
            this.cbRoles.Name = "cbRoles";
            this.cbRoles.Size = new System.Drawing.Size(70, 17);
            this.cbRoles.TabIndex = 9;
            this.cbRoles.Text = "Select All";
            this.cbRoles.UseVisualStyleBackColor = true;
            this.cbRoles.CheckedChanged += new System.EventHandler(this.cbRoles_CheckedChanged);
            // 
            // cbIncludeAllEntities
            // 
            this.cbIncludeAllEntities.AutoSize = true;
            this.cbIncludeAllEntities.Location = new System.Drawing.Point(676, 41);
            this.cbIncludeAllEntities.Name = "cbIncludeAllEntities";
            this.cbIncludeAllEntities.Size = new System.Drawing.Size(112, 17);
            this.cbIncludeAllEntities.TabIndex = 10;
            this.cbIncludeAllEntities.Text = "Include All Entities";
            this.cbIncludeAllEntities.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbIncludeAllEntities);
            this.Controls.Add(this.cbRoles);
            this.Controls.Add(this.cbAssign);
            this.Controls.Add(this.cbAppend);
            this.Controls.Add(this.btnGenerateSheet);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.btnFetch);
            this.Controls.Add(this.btnConnect);
            this.Name = "MainForm";
            this.Text = "Dynamic 365 Role Extractor";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnFetch;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsConnectionStatus;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnGenerateSheet;
        private System.Windows.Forms.CheckBox cbAppend;
        private System.Windows.Forms.CheckBox cbAssign;
        private System.Windows.Forms.CheckBox cbRoles;
        private System.Windows.Forms.CheckBox cbIncludeAllEntities;
    }
}

