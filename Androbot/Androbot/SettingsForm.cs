/*
 MIT License

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;
using System.Net;
using System.Windows.Forms;
using Androbot.Properties;

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
