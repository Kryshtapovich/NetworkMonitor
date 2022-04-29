
namespace NetworkMonitor
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.StatusStrip sbMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.NotifyIcon sysTray;
        private System.Windows.Forms.ContextMenuStrip sysTrayMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuShowApp;
        private System.Windows.Forms.ToolStripMenuItem mnuExitSysTray;
        private System.Windows.Forms.Timer tmrRefreshList;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.DataGridView gvRecords;
        private System.Windows.Forms.Panel pnlDevice;
        private System.Windows.Forms.ComboBox cbDevice;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.TabPage tabGrid;
        private System.Windows.Forms.DataGridView gvGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Protocol;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemoteAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemotePort;
        private System.Windows.Forms.DataGridViewTextBoxColumn Country;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PID;
        private System.Windows.Forms.TabControl tcMain;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.sbMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.sysTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.sysTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowApp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExitSysTray = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrRefreshList = new System.Windows.Forms.Timer(this.components);
            this.pnlDevice = new System.Windows.Forms.Panel();
            this.lblDevice = new System.Windows.Forms.Label();
            this.cbDevice = new System.Windows.Forms.ComboBox();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.gvRecords = new System.Windows.Forms.DataGridView();
            this.tabGrid = new System.Windows.Forms.TabPage();
            this.gvGrid = new System.Windows.Forms.DataGridView();
            this.Protocol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemoteAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemotePort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Country = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.sbMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.sysTrayMenu.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecords)).BeginInit();
            this.tabGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvGrid)).BeginInit();
            this.tcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // sbMain
            // 
            this.sbMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.sbMain.Location = new System.Drawing.Point(0, 972);
            this.sbMain.Name = "sbMain";
            this.sbMain.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.sbMain.Size = new System.Drawing.Size(1833, 22);
            this.sbMain.TabIndex = 0;
            this.sbMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 15);
            // 
            // mnuMain
            // 
            this.mnuMain.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1833, 33);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(178, 34);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.MnuExit_Click);
            // 
            // sysTray
            // 
            this.sysTray.ContextMenuStrip = this.sysTrayMenu;
            this.sysTray.Text = "Show Main Window";
            this.sysTray.Visible = true;
            // 
            // sysTrayMenu
            // 
            this.sysTrayMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.sysTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowApp,
            this.mnuExitSysTray});
            this.sysTrayMenu.Name = "sysTrayMenu";
            this.sysTrayMenu.Size = new System.Drawing.Size(232, 68);
            // 
            // mnuShowApp
            // 
            this.mnuShowApp.Name = "mnuShowApp";
            this.mnuShowApp.Size = new System.Drawing.Size(231, 32);
            this.mnuShowApp.Text = "Show Application";
            this.mnuShowApp.Click += new System.EventHandler(this.MnuShowApp_Click);
            // 
            // mnuExitSysTray
            // 
            this.mnuExitSysTray.Name = "mnuExitSysTray";
            this.mnuExitSysTray.Size = new System.Drawing.Size(231, 32);
            this.mnuExitSysTray.Text = "Exit";
            this.mnuExitSysTray.Click += new System.EventHandler(this.MnuExit_Click);
            // 
            // tmrRefreshList
            // 
            this.tmrRefreshList.Interval = 5000;
            this.tmrRefreshList.Tick += new System.EventHandler(this.TmrRefreshList_Tick);
            // 
            // pnlDevice
            // 
            this.pnlDevice.Location = new System.Drawing.Point(0, 0);
            this.pnlDevice.Name = "pnlDevice";
            this.pnlDevice.Size = new System.Drawing.Size(200, 100);
            this.pnlDevice.TabIndex = 0;
            // 
            // lblDevice
            // 
            this.lblDevice.Location = new System.Drawing.Point(0, 0);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(100, 23);
            this.lblDevice.TabIndex = 0;
            // 
            // cbDevice
            // 
            this.cbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevice.FormattingEnabled = true;
            this.cbDevice.Location = new System.Drawing.Point(120, 11);
            this.cbDevice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDevice.Name = "cbDevice";
            this.cbDevice.Size = new System.Drawing.Size(708, 28);
            this.cbDevice.TabIndex = 1;
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.gvRecords);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(4, 79);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1817, 812);
            this.pnlContainer.TabIndex = 1;
            // 
            // gvRecords
            // 
            this.gvRecords.AllowUserToAddRows = false;
            this.gvRecords.AllowUserToDeleteRows = false;
            this.gvRecords.AllowUserToResizeRows = false;
            this.gvRecords.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvRecords.Location = new System.Drawing.Point(0, 0);
            this.gvRecords.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gvRecords.Name = "gvRecords";
            this.gvRecords.ReadOnly = true;
            this.gvRecords.RowHeadersWidth = 62;
            this.gvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gvRecords.ShowEditingIcon = false;
            this.gvRecords.Size = new System.Drawing.Size(1817, 812);
            this.gvRecords.TabIndex = 4;
            // 
            // tabGrid
            // 
            this.tabGrid.Controls.Add(this.gvGrid);
            this.tabGrid.Location = new System.Drawing.Point(4, 29);
            this.tabGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabGrid.Name = "tabGrid";
            this.tabGrid.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabGrid.Size = new System.Drawing.Size(1825, 896);
            this.tabGrid.TabIndex = 1;
            this.tabGrid.Text = "List View";
            this.tabGrid.UseVisualStyleBackColor = true;
            // 
            // gvGrid
            // 
            this.gvGrid.AllowUserToAddRows = false;
            this.gvGrid.AllowUserToDeleteRows = false;
            this.gvGrid.AllowUserToResizeRows = false;
            this.gvGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Protocol,
            this.LocalPort,
            this.LocalAddress,
            this.RemoteAddress,
            this.RemotePort,
            this.Country,
            this.City,
            this.Status,
            this.ProcessName,
            this.PID});
            this.gvGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvGrid.Location = new System.Drawing.Point(4, 5);
            this.gvGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gvGrid.Name = "gvGrid";
            this.gvGrid.ReadOnly = true;
            this.gvGrid.RowHeadersWidth = 62;
            this.gvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gvGrid.ShowEditingIcon = false;
            this.gvGrid.Size = new System.Drawing.Size(1817, 886);
            this.gvGrid.TabIndex = 3;
            // 
            // Protocol
            // 
            this.Protocol.HeaderText = "Protocol";
            this.Protocol.MinimumWidth = 8;
            this.Protocol.Name = "Protocol";
            this.Protocol.ReadOnly = true;
            this.Protocol.Width = 117;
            // 
            // LocalPort
            // 
            this.LocalPort.HeaderText = "Local Port";
            this.LocalPort.MinimumWidth = 8;
            this.LocalPort.Name = "LocalPort";
            this.LocalPort.ReadOnly = true;
            this.LocalPort.Width = 116;
            // 
            // LocalAddress
            // 
            this.LocalAddress.HeaderText = "Local Address";
            this.LocalAddress.MinimumWidth = 8;
            this.LocalAddress.Name = "LocalAddress";
            this.LocalAddress.ReadOnly = true;
            this.LocalAddress.Width = 117;
            // 
            // RemoteAddress
            // 
            this.RemoteAddress.HeaderText = "Remote Address";
            this.RemoteAddress.MinimumWidth = 8;
            this.RemoteAddress.Name = "RemoteAddress";
            this.RemoteAddress.ReadOnly = true;
            this.RemoteAddress.Width = 116;
            // 
            // RemotePort
            // 
            this.RemotePort.HeaderText = "Remote Port";
            this.RemotePort.MinimumWidth = 8;
            this.RemotePort.Name = "RemotePort";
            this.RemotePort.ReadOnly = true;
            this.RemotePort.Width = 117;
            // 
            // Country
            // 
            this.Country.HeaderText = "Country";
            this.Country.MinimumWidth = 8;
            this.Country.Name = "Country";
            this.Country.ReadOnly = true;
            this.Country.Width = 116;
            // 
            // City
            // 
            this.City.HeaderText = "City";
            this.City.MinimumWidth = 8;
            this.City.Name = "City";
            this.City.ReadOnly = true;
            this.City.Width = 117;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 8;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 116;
            // 
            // ProcessName
            // 
            this.ProcessName.HeaderText = "Process Name";
            this.ProcessName.MinimumWidth = 8;
            this.ProcessName.Name = "ProcessName";
            this.ProcessName.ReadOnly = true;
            this.ProcessName.Width = 117;
            // 
            // PID
            // 
            this.PID.HeaderText = "PID";
            this.PID.MinimumWidth = 8;
            this.PID.Name = "PID";
            this.PID.ReadOnly = true;
            this.PID.Width = 116;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabGrid);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 33);
            this.tcMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1833, 939);
            this.tcMain.TabIndex = 2;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1833, 994);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.sbMain);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NetworkMonitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.sbMain.ResumeLayout(false);
            this.sbMain.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.sysTrayMenu.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvRecords)).EndInit();
            this.tabGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvGrid)).EndInit();
            this.tcMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
