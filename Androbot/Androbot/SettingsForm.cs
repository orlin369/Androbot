using Androbot.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Androbot
{
    public partial class SettingsForm : Form
    {

        #region Constructor

        public SettingsForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Settings Form

        private void SetingsForm_Load(object sender, EventArgs e)
        {
            this.LoadSettings();
        }

        private void SetingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveSettings();
        }

        #endregion

        #region Private Methods

        private void SaveSettings()
        {
            string textAddress = this.tbAddress.Text;
            IPAddress address;
            bool isValidAddress = IPAddress.TryParse(textAddress, out address);
            if (!isValidAddress)
            {
                MessageBox.Show("Invalid IP address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string textPort = this.tbPort.Text;
            int port;
            bool isValidPort = int.TryParse(textPort, out port);
            if (!isValidPort)
            {
                MessageBox.Show("Invalid port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string textBTID = this.tbBTID.Text.ToUpper();
            //textBTID = textBTID.Replace(":", "").ToUpper();

            // 
            Settings.Default.RobotIP = address.ToString();
            Settings.Default.RobotPort = port;
            Settings.Default.RobotBTID = textBTID;

            Settings.Default.Save();
        }

        private void LoadSettings()
        {
            this.tbAddress.Text = Settings.Default.RobotIP;
            this.tbPort.Text = Settings.Default.RobotPort.ToString();
            this.tbBTID.Text = Settings.Default.RobotBTID;
        }

        #endregion

    }
}
