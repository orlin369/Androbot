namespace DiO_Androbot
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.presentYourselfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.lblConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblGeoLocSrvState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblSensorsSrvState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabRobotFunctionalities = new System.Windows.Forms.TabControl();
            this.tpCameraCapture = new System.Windows.Forms.TabPage();
            this.tlpRobotFunctionalities = new System.Windows.Forms.TableLayoutPanel();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.pCaptureComponents = new System.Windows.Forms.Panel();
            this.btn3Dfy = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.tpGeoLocation = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pGeoLocationComponents = new System.Windows.Forms.Panel();
            this.cbMapType = new System.Windows.Forms.ComboBox();
            this.nudMapZoom = new System.Windows.Forms.NumericUpDown();
            this.btnGetGeoLocation = new System.Windows.Forms.Button();
            this.btnStopGeoLocation = new System.Windows.Forms.Button();
            this.btnStartGeoLocation = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tpSpeach = new System.Windows.Forms.TabPage();
            this.tlpRobotSpeach = new System.Windows.Forms.TableLayoutPanel();
            this.tbTextToSpeak = new System.Windows.Forms.TextBox();
            this.pSpeachComponents = new System.Windows.Forms.Panel();
            this.btnSpeak = new System.Windows.Forms.Button();
            this.tpMotion = new System.Windows.Forms.TabPage();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.tpSensors = new System.Windows.Forms.TabPage();
            this.tlpRobotSensors = new System.Windows.Forms.TableLayoutPanel();
            this.pSensors = new System.Windows.Forms.Panel();
            this.crtMagVectors = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pSensorComponenst = new System.Windows.Forms.Panel();
            this.btnGetSensors = new System.Windows.Forms.Button();
            this.btnStopSensors = new System.Windows.Forms.Button();
            this.btnStartSeonsors = new System.Windows.Forms.Button();
            this.mainMenuStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.tabRobotFunctionalities.SuspendLayout();
            this.tpCameraCapture.SuspendLayout();
            this.tlpRobotFunctionalities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.pCaptureComponents.SuspendLayout();
            this.tpGeoLocation.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pGeoLocationComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapZoom)).BeginInit();
            this.tpSpeach.SuspendLayout();
            this.tlpRobotSpeach.SuspendLayout();
            this.pSpeachComponents.SuspendLayout();
            this.tpMotion.SuspendLayout();
            this.tpSensors.SuspendLayout();
            this.tlpRobotSensors.SuspendLayout();
            this.pSensors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crtMagVectors)).BeginInit();
            this.pSensorComponenst.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.mainMenuStrip.Size = new System.Drawing.Size(1260, 28);
            this.mainMenuStrip.TabIndex = 9;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(120, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.presentYourselfToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // presentYourselfToolStripMenuItem
            // 
            this.presentYourselfToolStripMenuItem.Name = "presentYourselfToolStripMenuItem";
            this.presentYourselfToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.presentYourselfToolStripMenuItem.Text = "Present yourself";
            this.presentYourselfToolStripMenuItem.Click += new System.EventHandler(this.presentYourselfToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblConnectionStatus,
            this.tsslblGeoLocSrvState,
            this.tsslblSensorsSrvState});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 553);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.mainStatusStrip.Size = new System.Drawing.Size(1260, 25);
            this.mainStatusStrip.TabIndex = 10;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(112, 20);
            this.lblConnectionStatus.Text = "Connected: Not";
            // 
            // tsslblGeoLocSrvState
            // 
            this.tsslblGeoLocSrvState.Name = "tsslblGeoLocSrvState";
            this.tsslblGeoLocSrvState.Size = new System.Drawing.Size(152, 20);
            this.tsslblGeoLocSrvState.Text = "Geo Location: Stoped";
            // 
            // tsslblSensorsSrvState
            // 
            this.tsslblSensorsSrvState.Name = "tsslblSensorsSrvState";
            this.tsslblSensorsSrvState.Size = new System.Drawing.Size(114, 20);
            this.tsslblSensorsSrvState.Text = "Sensors: Stoped";
            // 
            // tabRobotFunctionalities
            // 
            this.tabRobotFunctionalities.Controls.Add(this.tpCameraCapture);
            this.tabRobotFunctionalities.Controls.Add(this.tpGeoLocation);
            this.tabRobotFunctionalities.Controls.Add(this.tpSpeach);
            this.tabRobotFunctionalities.Controls.Add(this.tpMotion);
            this.tabRobotFunctionalities.Controls.Add(this.tpSensors);
            this.tabRobotFunctionalities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabRobotFunctionalities.Location = new System.Drawing.Point(0, 28);
            this.tabRobotFunctionalities.Name = "tabRobotFunctionalities";
            this.tabRobotFunctionalities.SelectedIndex = 0;
            this.tabRobotFunctionalities.Size = new System.Drawing.Size(1260, 525);
            this.tabRobotFunctionalities.TabIndex = 15;
            // 
            // tpCameraCapture
            // 
            this.tpCameraCapture.Controls.Add(this.tlpRobotFunctionalities);
            this.tpCameraCapture.Location = new System.Drawing.Point(4, 29);
            this.tpCameraCapture.Name = "tpCameraCapture";
            this.tpCameraCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tpCameraCapture.Size = new System.Drawing.Size(1252, 492);
            this.tpCameraCapture.TabIndex = 0;
            this.tpCameraCapture.Text = "Camera";
            this.tpCameraCapture.UseVisualStyleBackColor = true;
            // 
            // tlpRobotFunctionalities
            // 
            this.tlpRobotFunctionalities.ColumnCount = 1;
            this.tlpRobotFunctionalities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRobotFunctionalities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRobotFunctionalities.Controls.Add(this.pbMain, 0, 0);
            this.tlpRobotFunctionalities.Controls.Add(this.pCaptureComponents, 0, 1);
            this.tlpRobotFunctionalities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRobotFunctionalities.Location = new System.Drawing.Point(3, 3);
            this.tlpRobotFunctionalities.Name = "tlpRobotFunctionalities";
            this.tlpRobotFunctionalities.RowCount = 2;
            this.tlpRobotFunctionalities.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.3887F));
            this.tlpRobotFunctionalities.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6113F));
            this.tlpRobotFunctionalities.Size = new System.Drawing.Size(1246, 486);
            this.tlpRobotFunctionalities.TabIndex = 0;
            // 
            // pbMain
            // 
            this.pbMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMain.Location = new System.Drawing.Point(6, 5);
            this.pbMain.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(1234, 395);
            this.pbMain.TabIndex = 1;
            this.pbMain.TabStop = false;
            // 
            // pCaptureComponents
            // 
            this.pCaptureComponents.Controls.Add(this.btn3Dfy);
            this.pCaptureComponents.Controls.Add(this.btnCapture);
            this.pCaptureComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCaptureComponents.Location = new System.Drawing.Point(3, 408);
            this.pCaptureComponents.Name = "pCaptureComponents";
            this.pCaptureComponents.Size = new System.Drawing.Size(1240, 75);
            this.pCaptureComponents.TabIndex = 2;
            // 
            // btn3Dfy
            // 
            this.btn3Dfy.Location = new System.Drawing.Point(201, 5);
            this.btn3Dfy.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btn3Dfy.Name = "btn3Dfy";
            this.btn3Dfy.Size = new System.Drawing.Size(183, 35);
            this.btn3Dfy.TabIndex = 3;
            this.btn3Dfy.Text = "3D";
            this.btn3Dfy.UseVisualStyleBackColor = true;
            this.btn3Dfy.Click += new System.EventHandler(this.btn3Dfy_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(6, 5);
            this.btnCapture.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(183, 35);
            this.btnCapture.TabIndex = 2;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // tpGeoLocation
            // 
            this.tpGeoLocation.Controls.Add(this.tableLayoutPanel1);
            this.tpGeoLocation.Location = new System.Drawing.Point(4, 29);
            this.tpGeoLocation.Name = "tpGeoLocation";
            this.tpGeoLocation.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeoLocation.Size = new System.Drawing.Size(1252, 492);
            this.tpGeoLocation.TabIndex = 1;
            this.tpGeoLocation.Text = "Geo Location";
            this.tpGeoLocation.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pGeoLocationComponents, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.webBrowser, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.05648F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.94352F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1246, 486);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pGeoLocationComponents
            // 
            this.pGeoLocationComponents.Controls.Add(this.cbMapType);
            this.pGeoLocationComponents.Controls.Add(this.nudMapZoom);
            this.pGeoLocationComponents.Controls.Add(this.btnGetGeoLocation);
            this.pGeoLocationComponents.Controls.Add(this.btnStopGeoLocation);
            this.pGeoLocationComponents.Controls.Add(this.btnStartGeoLocation);
            this.pGeoLocationComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGeoLocationComponents.Location = new System.Drawing.Point(3, 406);
            this.pGeoLocationComponents.Name = "pGeoLocationComponents";
            this.pGeoLocationComponents.Size = new System.Drawing.Size(1240, 77);
            this.pGeoLocationComponents.TabIndex = 0;
            // 
            // cbMapType
            // 
            this.cbMapType.FormattingEnabled = true;
            this.cbMapType.Location = new System.Drawing.Point(714, 9);
            this.cbMapType.Name = "cbMapType";
            this.cbMapType.Size = new System.Drawing.Size(121, 28);
            this.cbMapType.TabIndex = 19;
            this.cbMapType.SelectedIndexChanged += new System.EventHandler(this.cbMapType_SelectedIndexChanged);
            // 
            // nudMapZoom
            // 
            this.nudMapZoom.Location = new System.Drawing.Point(588, 10);
            this.nudMapZoom.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudMapZoom.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMapZoom.Name = "nudMapZoom";
            this.nudMapZoom.Size = new System.Drawing.Size(120, 27);
            this.nudMapZoom.TabIndex = 18;
            this.nudMapZoom.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.nudMapZoom.ValueChanged += new System.EventHandler(this.nudMapZoom_ValueChanged);
            // 
            // btnGetGeoLocation
            // 
            this.btnGetGeoLocation.Location = new System.Drawing.Point(396, 5);
            this.btnGetGeoLocation.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnGetGeoLocation.Name = "btnGetGeoLocation";
            this.btnGetGeoLocation.Size = new System.Drawing.Size(183, 35);
            this.btnGetGeoLocation.TabIndex = 17;
            this.btnGetGeoLocation.Text = "Get Geo Location";
            this.btnGetGeoLocation.UseVisualStyleBackColor = true;
            this.btnGetGeoLocation.Click += new System.EventHandler(this.btnGetGeoLocation_Click);
            // 
            // btnStopGeoLocation
            // 
            this.btnStopGeoLocation.Location = new System.Drawing.Point(201, 5);
            this.btnStopGeoLocation.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnStopGeoLocation.Name = "btnStopGeoLocation";
            this.btnStopGeoLocation.Size = new System.Drawing.Size(183, 35);
            this.btnStopGeoLocation.TabIndex = 16;
            this.btnStopGeoLocation.Text = "Stop Geo Location";
            this.btnStopGeoLocation.UseVisualStyleBackColor = true;
            this.btnStopGeoLocation.Click += new System.EventHandler(this.btnStopGeoLocation_Click);
            // 
            // btnStartGeoLocation
            // 
            this.btnStartGeoLocation.Location = new System.Drawing.Point(6, 5);
            this.btnStartGeoLocation.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnStartGeoLocation.Name = "btnStartGeoLocation";
            this.btnStartGeoLocation.Size = new System.Drawing.Size(183, 35);
            this.btnStartGeoLocation.TabIndex = 15;
            this.btnStartGeoLocation.Text = "Start Geo Location";
            this.btnStartGeoLocation.UseVisualStyleBackColor = true;
            this.btnStartGeoLocation.Click += new System.EventHandler(this.btnStartGeoLocation_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(3, 3);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(1240, 397);
            this.webBrowser.TabIndex = 1;
            // 
            // tpSpeach
            // 
            this.tpSpeach.Controls.Add(this.tlpRobotSpeach);
            this.tpSpeach.Location = new System.Drawing.Point(4, 29);
            this.tpSpeach.Name = "tpSpeach";
            this.tpSpeach.Padding = new System.Windows.Forms.Padding(3);
            this.tpSpeach.Size = new System.Drawing.Size(1252, 492);
            this.tpSpeach.TabIndex = 2;
            this.tpSpeach.Text = "Speach";
            this.tpSpeach.UseVisualStyleBackColor = true;
            // 
            // tlpRobotSpeach
            // 
            this.tlpRobotSpeach.ColumnCount = 1;
            this.tlpRobotSpeach.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRobotSpeach.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRobotSpeach.Controls.Add(this.tbTextToSpeak, 0, 0);
            this.tlpRobotSpeach.Controls.Add(this.pSpeachComponents, 0, 1);
            this.tlpRobotSpeach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRobotSpeach.Location = new System.Drawing.Point(3, 3);
            this.tlpRobotSpeach.Name = "tlpRobotSpeach";
            this.tlpRobotSpeach.RowCount = 2;
            this.tlpRobotSpeach.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.3887F));
            this.tlpRobotSpeach.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6113F));
            this.tlpRobotSpeach.Size = new System.Drawing.Size(1246, 486);
            this.tlpRobotSpeach.TabIndex = 0;
            // 
            // tbTextToSpeak
            // 
            this.tbTextToSpeak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTextToSpeak.Location = new System.Drawing.Point(6, 5);
            this.tbTextToSpeak.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tbTextToSpeak.Multiline = true;
            this.tbTextToSpeak.Name = "tbTextToSpeak";
            this.tbTextToSpeak.Size = new System.Drawing.Size(1234, 395);
            this.tbTextToSpeak.TabIndex = 8;
            this.tbTextToSpeak.Text = "Hello, I am Androbot!";
            this.tbTextToSpeak.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pSpeachComponents
            // 
            this.pSpeachComponents.Controls.Add(this.btnSpeak);
            this.pSpeachComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSpeachComponents.Location = new System.Drawing.Point(3, 408);
            this.pSpeachComponents.Name = "pSpeachComponents";
            this.pSpeachComponents.Size = new System.Drawing.Size(1240, 75);
            this.pSpeachComponents.TabIndex = 9;
            // 
            // btnSpeak
            // 
            this.btnSpeak.Location = new System.Drawing.Point(6, 5);
            this.btnSpeak.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnSpeak.Name = "btnSpeak";
            this.btnSpeak.Size = new System.Drawing.Size(183, 35);
            this.btnSpeak.TabIndex = 9;
            this.btnSpeak.Text = "Speak";
            this.btnSpeak.UseVisualStyleBackColor = true;
            this.btnSpeak.Click += new System.EventHandler(this.btnSpeak_Click);
            // 
            // tpMotion
            // 
            this.tpMotion.Controls.Add(this.btnRight);
            this.tpMotion.Controls.Add(this.btnStop);
            this.tpMotion.Controls.Add(this.btnBackward);
            this.tpMotion.Controls.Add(this.btnLeft);
            this.tpMotion.Controls.Add(this.btnForward);
            this.tpMotion.Location = new System.Drawing.Point(4, 29);
            this.tpMotion.Name = "tpMotion";
            this.tpMotion.Padding = new System.Windows.Forms.Padding(3);
            this.tpMotion.Size = new System.Drawing.Size(1252, 492);
            this.tpMotion.TabIndex = 3;
            this.tpMotion.Text = "Motion";
            this.tpMotion.UseVisualStyleBackColor = true;
            // 
            // btnRight
            // 
            this.btnRight.Image = global::DiO_Androbot.Images.ArrowRight;
            this.btnRight.Location = new System.Drawing.Point(218, 105);
            this.btnRight.Margin = new System.Windows.Forms.Padding(4);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(97, 90);
            this.btnRight.TabIndex = 23;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::DiO_Androbot.Images.Stop;
            this.btnStop.Location = new System.Drawing.Point(112, 105);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(97, 90);
            this.btnStop.TabIndex = 22;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.Image = global::DiO_Androbot.Images.ArrowDown;
            this.btnBackward.Location = new System.Drawing.Point(112, 202);
            this.btnBackward.Margin = new System.Windows.Forms.Padding(4);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(97, 90);
            this.btnBackward.TabIndex = 21;
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Image = global::DiO_Androbot.Images.ArrowLeft;
            this.btnLeft.Location = new System.Drawing.Point(7, 105);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(4);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(97, 90);
            this.btnLeft.TabIndex = 20;
            this.btnLeft.UseVisualStyleBackColor = true;
            // 
            // btnForward
            // 
            this.btnForward.Image = global::DiO_Androbot.Images.ArrowUp;
            this.btnForward.Location = new System.Drawing.Point(112, 7);
            this.btnForward.Margin = new System.Windows.Forms.Padding(4);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(97, 90);
            this.btnForward.TabIndex = 19;
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // tpSensors
            // 
            this.tpSensors.Controls.Add(this.tlpRobotSensors);
            this.tpSensors.Location = new System.Drawing.Point(4, 29);
            this.tpSensors.Name = "tpSensors";
            this.tpSensors.Padding = new System.Windows.Forms.Padding(3);
            this.tpSensors.Size = new System.Drawing.Size(1252, 492);
            this.tpSensors.TabIndex = 4;
            this.tpSensors.Text = "Sensors";
            this.tpSensors.UseVisualStyleBackColor = true;
            // 
            // tlpRobotSensors
            // 
            this.tlpRobotSensors.ColumnCount = 1;
            this.tlpRobotSensors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRobotSensors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRobotSensors.Controls.Add(this.pSensors, 0, 0);
            this.tlpRobotSensors.Controls.Add(this.pSensorComponenst, 0, 1);
            this.tlpRobotSensors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRobotSensors.Location = new System.Drawing.Point(3, 3);
            this.tlpRobotSensors.Name = "tlpRobotSensors";
            this.tlpRobotSensors.RowCount = 2;
            this.tlpRobotSensors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tlpRobotSensors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpRobotSensors.Size = new System.Drawing.Size(1246, 486);
            this.tlpRobotSensors.TabIndex = 1;
            // 
            // pSensors
            // 
            this.pSensors.Controls.Add(this.crtMagVectors);
            this.pSensors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSensors.Location = new System.Drawing.Point(3, 3);
            this.pSensors.Name = "pSensors";
            this.pSensors.Size = new System.Drawing.Size(1240, 398);
            this.pSensors.TabIndex = 0;
            // 
            // crtMagVectors
            // 
            chartArea1.Name = "ChartArea1";
            this.crtMagVectors.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.crtMagVectors.Legends.Add(legend1);
            this.crtMagVectors.Location = new System.Drawing.Point(6, 3);
            this.crtMagVectors.Name = "crtMagVectors";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.crtMagVectors.Series.Add(series1);
            this.crtMagVectors.Size = new System.Drawing.Size(677, 329);
            this.crtMagVectors.TabIndex = 0;
            this.crtMagVectors.Text = "chart1";
            // 
            // pSensorComponenst
            // 
            this.pSensorComponenst.Controls.Add(this.btnGetSensors);
            this.pSensorComponenst.Controls.Add(this.btnStopSensors);
            this.pSensorComponenst.Controls.Add(this.btnStartSeonsors);
            this.pSensorComponenst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSensorComponenst.Location = new System.Drawing.Point(3, 407);
            this.pSensorComponenst.Name = "pSensorComponenst";
            this.pSensorComponenst.Size = new System.Drawing.Size(1240, 76);
            this.pSensorComponenst.TabIndex = 1;
            // 
            // btnGetSensors
            // 
            this.btnGetSensors.Location = new System.Drawing.Point(396, 5);
            this.btnGetSensors.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnGetSensors.Name = "btnGetSensors";
            this.btnGetSensors.Size = new System.Drawing.Size(183, 35);
            this.btnGetSensors.TabIndex = 20;
            this.btnGetSensors.Text = "Get Sensors";
            this.btnGetSensors.UseVisualStyleBackColor = true;
            this.btnGetSensors.Click += new System.EventHandler(this.btnGetSensors_Click);
            // 
            // btnStopSensors
            // 
            this.btnStopSensors.Location = new System.Drawing.Point(201, 5);
            this.btnStopSensors.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnStopSensors.Name = "btnStopSensors";
            this.btnStopSensors.Size = new System.Drawing.Size(183, 35);
            this.btnStopSensors.TabIndex = 19;
            this.btnStopSensors.Text = "Stop Sensors";
            this.btnStopSensors.UseVisualStyleBackColor = true;
            this.btnStopSensors.Click += new System.EventHandler(this.btnStopSensors_Click);
            // 
            // btnStartSeonsors
            // 
            this.btnStartSeonsors.Location = new System.Drawing.Point(6, 5);
            this.btnStartSeonsors.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnStartSeonsors.Name = "btnStartSeonsors";
            this.btnStartSeonsors.Size = new System.Drawing.Size(183, 35);
            this.btnStartSeonsors.TabIndex = 18;
            this.btnStartSeonsors.Text = "Start Sensors";
            this.btnStartSeonsors.UseVisualStyleBackColor = true;
            this.btnStartSeonsors.Click += new System.EventHandler(this.btnStartSeonsors_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 578);
            this.Controls.Add(this.tabRobotFunctionalities);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "MainForm";
            this.Text = "Androbot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.tabRobotFunctionalities.ResumeLayout(false);
            this.tpCameraCapture.ResumeLayout(false);
            this.tlpRobotFunctionalities.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.pCaptureComponents.ResumeLayout(false);
            this.tpGeoLocation.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pGeoLocationComponents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudMapZoom)).EndInit();
            this.tpSpeach.ResumeLayout(false);
            this.tlpRobotSpeach.ResumeLayout(false);
            this.tlpRobotSpeach.PerformLayout();
            this.pSpeachComponents.ResumeLayout(false);
            this.tpMotion.ResumeLayout(false);
            this.tpSensors.ResumeLayout(false);
            this.tlpRobotSensors.ResumeLayout(false);
            this.pSensors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.crtMagVectors)).EndInit();
            this.pSensorComponenst.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presentYourselfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblConnectionStatus;
        private System.Windows.Forms.TabControl tabRobotFunctionalities;
        private System.Windows.Forms.TabPage tpCameraCapture;
        private System.Windows.Forms.TableLayoutPanel tlpRobotFunctionalities;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Panel pCaptureComponents;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.TabPage tpGeoLocation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pGeoLocationComponents;
        private System.Windows.Forms.Button btnGetGeoLocation;
        private System.Windows.Forms.Button btnStopGeoLocation;
        private System.Windows.Forms.Button btnStartGeoLocation;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TabPage tpSpeach;
        private System.Windows.Forms.TableLayoutPanel tlpRobotSpeach;
        private System.Windows.Forms.TextBox tbTextToSpeak;
        private System.Windows.Forms.Panel pSpeachComponents;
        private System.Windows.Forms.Button btnSpeak;
        private System.Windows.Forms.TabPage tpMotion;
        private System.Windows.Forms.Button btn3Dfy;
        private System.Windows.Forms.ToolStripStatusLabel tsslblGeoLocSrvState;
        private System.Windows.Forms.NumericUpDown nudMapZoom;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnBackward;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.ComboBox cbMapType;
        private System.Windows.Forms.TabPage tpSensors;
        private System.Windows.Forms.TableLayoutPanel tlpRobotSensors;
        private System.Windows.Forms.Panel pSensors;
        private System.Windows.Forms.Panel pSensorComponenst;
        private System.Windows.Forms.Button btnGetSensors;
        private System.Windows.Forms.Button btnStopSensors;
        private System.Windows.Forms.Button btnStartSeonsors;
        private System.Windows.Forms.ToolStripStatusLabel tsslblSensorsSrvState;
        private System.Windows.Forms.DataVisualization.Charting.Chart crtMagVectors;
    }
}

