using NetworkMonitor.Models;
using NetworkMonitor.Services;
using System;
using System.Windows.Forms;

namespace NetworkMonitor
{
    public partial class FrmMain : Form
    {
        public Settings settings = SettingsHelper.GetSettings();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            tmrRefreshList.Enabled = true;
            if (VisualizationHelper.CheckData(ref settings))
            {
                gvGrid.Columns.Clear();
                gvGrid.DataSource = VisualizationHelper.GridSource;
            }
        }

        private void MnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MnuShowApp_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingsHelper.SaveSettings(settings);
            Application.Exit();
        }

        private void TmrRefreshList_Tick(object sender, EventArgs e)
        {
            if (VisualizationHelper.CheckData(ref settings))
            {
                gvGrid.DataSource = VisualizationHelper.GridSource;
            }
        }
    }
}
