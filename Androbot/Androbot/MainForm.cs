using System;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using Androbot.Properties;
using Androbot.Androbot.Events;
using Androbot.Androbot.Data.Sensors;
using Androbot.Androbot.Data.GeoLocation;
using System.Runtime.InteropServices;
using StereoScopic;
using System.Drawing.Drawing2D;

// Inspiration for the maps.
// https://developers.google.com/maps/documentation/static-maps/intro#Markers

// TODO: Add kyboard event handler.
// TODO: Add the whole command set of the carel robot.
// TODO: Add library to 3Dfy the camera image.
// TODO: Add circular diagram for the sensor.
// TODO: Log4Net READ MF.
// TODO: Add LORA API layer.

namespace Androbot
{
    public partial class MainForm : Form
    {

        #region Variables

        /// <summary>
        /// The robot.
        /// </summary>
        private Androbot.Robot robot;

        /// <summary>
        /// Map type visualization.
        /// </summary>
        private string mapType = Androbot.Data.GeoLocation.Google.MapType.SATELITE;

        /// <summary>
        /// 
        /// </summary>
        private int shiftValue = 10;

        /// <summary>
        /// Input original image.
        /// </summary>
        private Bitmap inputImage;

        /// <summary>
        /// Output processed image.
        /// </summary>
        private Bitmap outputImage;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            this.pbMain.SizeMode = PictureBoxSizeMode.CenterImage;

            this.cbMapType.Items.AddRange(Androbot.Data.GeoLocation.Here.MapType.AsArray);
            if (this.cbMapType.Items.Count > 0)
            {
                this.cbMapType.SelectedIndex = 0;
            }
        }

        #endregion

        #region Main Form

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.GenerateChrtData();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DisconnectFromRobot();
        }

        #endregion

        #region Buttons

        private void btnCapture_Click(object sender, EventArgs e)
        {
            this.CaptureRobotCamera(1);
        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            this.MakeRobotSpeak(this.tbTextToSpeak.Text);
        }

        private void btnGetGeoLocation_Click(object sender, EventArgs e)
        {
            this.GetRobotLocation();
        }

        private void btnStartGeoLocation_Click(object sender, EventArgs e)
        {
            this.StartRobotGeoLocation();
        }

        private void btnStopGeoLocation_Click(object sender, EventArgs e)
        {
            this.StopRobotLocation();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void btnBackward_Click(object sender, EventArgs e)
        {

        }

        private void btnLeft_Click(object sender, EventArgs e)
        {

        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            this.MoveRobot();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {

        }

        private void btnStartSeonsors_Click(object sender, EventArgs e)
        {
            this.StartRobotSensors();
        }

        private void btnStopSensors_Click(object sender, EventArgs e)
        {
            this.StopRobotSensors();
        }

        private void btnGetSensors_Click(object sender, EventArgs e)
        {
            this.GetRobotSensors();
        }

        private void btn3Dfy_Click(object sender, EventArgs e)
        {
            this.ProcessImage();
        }

        #endregion

        #region Menu

        private void presentYourselfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MakeRobotSpeak("Hello, I am Androbot. I am born @ 2016. My creator is Orlin Dimitrov. Technologies that I use are: Python, dotNet, WinForms, Arduino, Android, HTTP, etc.");
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ConnectToRobot();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DisconnectFromRobot();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm sf = new SettingsForm())
            {
                sf.ShowDialog();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is Androbot.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Private Methods

        public Bitmap FitImage(Bitmap image, Size size)
        {
            var ratioX = (double)size.Width / image.Width;
            var ratioY = (double)size.Height / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        /// <summary>
        /// Prosecc image.
        /// </summary>
        private void ProcessImage()
        {
            // jivanov@repir.eu Julian Ivanov
            // 20140918 JI@DevGroup: Long running task should not be executed in the main thread.
            //                       Thread thread = new Thread(() => 
            //                                                  {
            //                                                      // code executend in another thread
            //                                                  });
            //                       thread.Start();

            Thread workerThread = new Thread(() =>
            {
                // Rend the image.
                this.outputImage = Anaglyph.Make3DPopIn(new Bitmap((Image)this.inputImage), this.shiftValue);
                // Show the nwe mage.
                this.pbMain.Image = this.FitImage(this.outputImage, this.pbMain.Size);
            });
            workerThread.Start();

        }

        #endregion

        #region Robot

        private void ConnectToRobot()
        {
            if (this.robot != null)
            {
                return;
            }

            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(Settings.Default.RobotIP), Settings.Default.RobotPort);
                this.robot = new Androbot.Robot(endPoint);
                this.robot.OnImageDeliver += this.robot_OnImageDeliver;
                this.robot.OnLocationDelivered += this.robot_OnLocationDeliver;
                this.robot.OnLocationStarted += this.robot_OnLocationStarted;
                this.robot.OnLocationStoped += this.robot_OnLocationStoped;
                this.robot.OnSensorsStarted += this.robot_OnSensoerStarted;
                this.robot.OnSensorsStoped += this.robot_OnSensorsStoped;
                this.robot.OnSensorsDelivered += this.robot_OnSensorsDelivered;

                this.robot.Connect(Settings.Default.RobotBTID);

                this.lblConnectionStatus.Text = String.Format("Connection: {0}:{1}", Settings.Default.RobotIP, Settings.Default.RobotPort);
            }
            catch (Exception exception)
            {
                // TODO: Log the error.
            }
        }

        private void DisconnectFromRobot()
        {
            if (this.robot == null)
            {
                return;
            }

            try
            {
                this.robot.StopMoveing();
                this.robot.StopGeoLocation();
                this.robot.StopSensors();
                this.robot.Disconnect();

                this.robot = null;
                this.lblConnectionStatus.Text = "Connection: Not";
            }
            catch (Exception exception)
            {
                // TODO: Log the error.
            }
        }

        private void MoveRobot()
        {
            if (this.robot == null)
            {
                return;
            }

            Thread worker = new Thread(new ThreadStart(
                delegate ()
                {
                    try
                    {
                        //
                        if (this.btnForward.InvokeRequired)
                        {
                            this.btnForward.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnForward.Enabled = false;
                            });
                        }
                        else
                        {
                            this.btnForward.Enabled = false;
                        }

                        // 
                        this.robot.Move(1.0D, 1.0D);

                        // 
                        if (this.btnForward.InvokeRequired)
                        {
                            this.btnForward.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnForward.Enabled = true;
                            });
                        }
                        else
                        {
                            this.btnForward.Enabled = true;
                        }

                    }
                    catch (Exception exception)
                    {
                        // TODO: Log the error.
                    }
                }));

            worker.Start();

        }

        private void MakeRobotSpeak(string text)
        {
            if (this.robot == null)
            {
                return;
            }

            Thread worker = new Thread(new ThreadStart(
                delegate ()
                {
                    try
                    {
                        //
                        if (this.btnSpeak.InvokeRequired)
                        {
                            this.btnSpeak.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnSpeak.Enabled = false;
                            });
                        }
                        else
                        {
                            this.btnSpeak.Enabled = false;
                        }

                        // 
                        this.robot.Talk(text);

                        // 
                        if (this.btnSpeak.InvokeRequired)
                        {
                            this.btnSpeak.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnSpeak.Enabled = true;
                            });
                        }
                        else
                        {
                            this.btnSpeak.Enabled = true;
                        }

                    }
                    catch (Exception exception)
                    {
                        // TODO: Log the error.
                    }
                }));

            worker.Start();

        }

        private void CaptureRobotCamera(int index)
        {
            // Exit if there is no robot instanced.
            if (this.robot == null)
            {
                return;
            }

            // Gnerate the thread to do the job.
            Thread worker = new Thread(new ThreadStart(
                delegate ()
                {
                    try
                    {
                        // Disabble the capture button.
                        if (this.btnCapture.InvokeRequired)
                        {
                            this.btnCapture.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnCapture.Enabled = false;
                            });
                        }
                        else
                        {
                            this.btnCapture.Enabled = false;
                        }

                        // Capture
                        this.robot.Capture(index);

                        // Enable the capture button.
                        if (this.btnCapture.InvokeRequired)
                        {
                            this.btnCapture.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnCapture.Enabled = true;
                            });
                        }
                        else
                        {
                            this.btnCapture.Enabled = true;
                        }
                    }
                    catch (Exception exception)
                    {
                        // TODO: Log the error.
                    }
                }));

            worker.Start();

        }

        private void StartRobotGeoLocation()
        {
            if (this.robot == null)
            {
                return;
            }

            if (this.robot.IsGeoLocationStarted)
            {
                return;
            }

            Thread worker = new Thread(new ThreadStart(
                delegate ()
                {
                    try
                    {
                        //
                        if (this.btnStartGeoLocation.InvokeRequired)
                        {
                            this.btnStartGeoLocation.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnStartGeoLocation.Enabled = false;
                            });
                        }
                        else
                        {
                            this.btnStartGeoLocation.Enabled = false;
                        }

                        // 
                        this.robot.StartGeoLocation();

                        // 
                        if (this.btnStartGeoLocation.InvokeRequired)
                        {
                            this.btnStartGeoLocation.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnStartGeoLocation.Enabled = true;
                            });
                        }
                        else
                        {
                            this.btnStartGeoLocation.Enabled = true;
                        }

                    }
                    catch (Exception exception)
                    {
                        // TODO: Log the error.
                    }
                }));

            worker.Start();
        }

        private void StopRobotLocation()
        {
            if (this.robot == null)
            {
                return;
            }

            if (!this.robot.IsGeoLocationStarted)
            {
                return;
            }

            Thread worker = new Thread(new ThreadStart(
                delegate ()
                {
                    try
                    {
                        //
                        if (this.btnGetGeoLocation.InvokeRequired)
                        {
                            this.btnGetGeoLocation.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnGetGeoLocation.Enabled = false;
                            });
                        }
                        else
                        {
                            this.btnGetGeoLocation.Enabled = false;
                        }

                        // 
                        this.robot.StopGeoLocation();

                        // 
                        if (this.btnGetGeoLocation.InvokeRequired)
                        {
                            this.btnGetGeoLocation.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnGetGeoLocation.Enabled = true;
                            });
                        }
                        else
                        {
                            this.btnGetGeoLocation.Enabled = true;
                        }

                    }
                    catch (Exception exception)
                    {
                        // TODO: Log the error.
                    }
                }));

            worker.Start();
        }

        private void GetRobotLocation()
        {
            if (this.robot == null)
            {
                return;
            }

            if (!this.robot.IsGeoLocationStarted)
            {
                return;
            }

            Thread worker = new Thread(new ThreadStart(
                delegate ()
                {
                    try
                    {
                        //
                        if (this.btnGetGeoLocation.InvokeRequired)
                        {
                            this.btnGetGeoLocation.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnGetGeoLocation.Enabled = false;
                            });
                        }
                        else
                        {
                            this.btnGetGeoLocation.Enabled = false;
                        }

                        // 
                        this.robot.GetGeoLocation();

                        // 
                        if (this.btnGetGeoLocation.InvokeRequired)
                        {
                            this.btnGetGeoLocation.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnGetGeoLocation.Enabled = true;
                            });
                        }
                        else
                        {
                            this.btnGetGeoLocation.Enabled = true;
                        }

                    }
                    catch (Exception exception)
                    {
                        // TODO: Log the error.
                    }
                }));

            worker.Start();
        }

        private void StartRobotSensors()
        {
            if (this.robot == null)
            {
                return;
            }

            if (this.robot.IsSensorsStarted)
            {
                return;
            }

            Thread worker = new Thread(new ThreadStart(
                delegate ()
                {
                    try
                    {
                        //
                        if (this.btnStartSeonsors.InvokeRequired)
                        {
                            this.btnStartSeonsors.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnStartSeonsors.Enabled = false;
                            });
                        }
                        else
                        {
                            this.btnStartSeonsors.Enabled = false;
                        }

                        // 
                        this.robot.StartSensors();

                        // 
                        if (this.btnStartSeonsors.InvokeRequired)
                        {
                            this.btnStartSeonsors.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnStartSeonsors.Enabled = true;
                            });
                        }
                        else
                        {
                            this.btnStartSeonsors.Enabled = true;
                        }

                    }
                    catch (Exception exception)
                    {
                        // TODO: Log the error.
                    }
                }));

            worker.Start();

        }

        private void StopRobotSensors()
        {
            if (this.robot == null)
            {
                return;
            }

            if (!this.robot.IsSensorsStarted)
            {
                return;
            }

            Thread worker = new Thread(new ThreadStart(
                delegate ()
                {
                    try
                    {
                        //
                        if (this.btnStopSensors.InvokeRequired)
                        {
                            this.btnStopSensors.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnStopSensors.Enabled = false;
                            });
                        }
                        else
                        {
                            this.btnStopSensors.Enabled = false;
                        }

                        // 
                        this.robot.StopSensors();

                        // 
                        if (this.btnStopSensors.InvokeRequired)
                        {
                            this.btnStopSensors.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnStopSensors.Enabled = true;
                            });
                        }
                        else
                        {
                            this.btnStopSensors.Enabled = true;
                        }

                    }
                    catch (Exception exception)
                    {
                        // TODO: Log the error.
                    }
                }));

            worker.Start();
        }

        private void GetRobotSensors()
        {
            if (this.robot == null)
            {
                return;
            }

            if (!this.robot.IsSensorsStarted)
            {
                return;
            }

            Thread worker = new Thread(new ThreadStart(
                delegate ()
                {
                    try
                    {
                        //
                        if (this.btnGetSensors.InvokeRequired)
                        {
                            this.btnGetSensors.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnGetSensors.Enabled = false;
                            });
                        }
                        else
                        {
                            this.btnGetSensors.Enabled = false;
                        }

                        // 
                        this.robot.GetSensors();

                        // 
                        if (this.btnGetSensors.InvokeRequired)
                        {
                            this.btnGetSensors.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.btnGetSensors.Enabled = true;
                            });
                        }
                        else
                        {
                            this.btnGetSensors.Enabled = true;
                        }

                    }
                    catch (Exception exception)
                    {
                        // TODO: Log the error.
                    }
                }));

            worker.Start();
        }

        private void robot_OnImageDeliver(object sender, EventArgImage e)
        {
            if (this.inputImage != null) this.inputImage.Dispose();
            this.inputImage = (Bitmap)e.Image;

            this.pbMain.Image = FitImage(this.inputImage, this.pbMain.Size);
        }

        private void robot_OnLocationDeliver(object sender, Location e)
        {
            try
            {
                // Google maps
                string uriGoogle = Androbot.Data.GeoLocation.Google.ServiceData.CreateUri(
                    e.network.latitude,
                    e.network.longitude,
                    (int)this.nudMapZoom.Value,
                    new Size(this.webBrowser.Width, this.webBrowser.Height),
                    this.mapType);

                // Here maps
                string uriHere = Androbot.Data.GeoLocation.Here.ServiceData.CreateUri(
                    e.network.latitude,
                    e.network.longitude,
                    (int)this.nudMapZoom.Value,
                    new Size(this.webBrowser.Width, this.webBrowser.Height),
                    this.mapType);

                // Navigate to URI.
                this.webBrowser.Navigate(uriHere);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void robot_OnLocationStoped(object sender, EventArgs e)
        {
            this.tsslblGeoLocSrvState.Text = "Geo Location: Stoped";
        }

        private void robot_OnLocationStarted(object sender, EventArgs e)
        {
            this.tsslblGeoLocSrvState.Text = "Geo Location: Started";
        }

        private void robot_OnSensorsStoped(object sender, EventArgs e)
        {
            this.tsslblSensorsSrvState.Text = "Sensors: Stoped";
        }

        private void robot_OnSensoerStarted(object sender, EventArgs e)
        {
            this.tsslblSensorsSrvState.Text = "Sensors: Started";
        }

        private void robot_OnSensorsDelivered(object sender, Mesurment e)
        {
            Console.WriteLine(e.accuracy);
        }

        #endregion

        private void nudMapZoom_ValueChanged(object sender, EventArgs e)
        {
            this.GetRobotLocation();
        }

        private void cbMapType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mapType = (string)this.cbMapType.SelectedItem;

            this.GetRobotLocation();
        }

        private void GenerateChrtData()
        {
            // Inspiration ...
            // http://stackoverflow.com/questions/10622674/chart-creating-dynamically-in-net-c-sharp

            this.crtMagVectors.Series.Clear();

            Series seriesX = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "X",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line
            };

            Series seriesY = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Y",
                Color = System.Drawing.Color.Red,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line
            };

            this.crtMagVectors.Series.Add(seriesX);
            this.crtMagVectors.Series.Add(seriesY);

            for (int i = 0; i < 100; i++)
            {
                seriesX.Points.AddXY(i, fX(i));
                seriesY.Points.AddXY(i, fY(i));
            }

            this.crtMagVectors.Invalidate();
        }

        private double fX(int i)
        {
            var f1 = 59894 - (8128 * i) + (262 * i * i) - (1.6 * i * i * i);
            return f1;
        }
        private double fY(int i)
        {
            var f1 = 1 - (8128 * i) + (262 * i * i) - (1.6 * i * i * i);
            return f1;
        }

        #region Track bar shift value.

        /// <summary>
        /// When the track bar value change update everything.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trbShiftValue_ValueChanged(object sender, EventArgs e)
        {
            // Update the value.
            this.shiftValue = this.trbShiftValue.Value;
            // Visualise the value.
            this.lblShiftValue.Text = String.Format("Shift: {0}", this.shiftValue);
        }

        #endregion
    }
}
