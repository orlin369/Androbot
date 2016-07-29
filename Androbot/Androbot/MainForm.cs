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
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

using Androbot.Properties;
using Androbot.Androbot.Events;
using Androbot.Androbot.Data.Sensors;
using Androbot.Androbot.Data.GeoLocation;

using StereoScopic;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;

// Inspiration for the maps.
// https://developers.google.com/maps/documentation/static-maps/intro#Markers

// TODO: Add kyboard event handler.
// TODO: Add the whole command set of the carel robot.
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
        /// Output processed Anaglyph image.
        /// </summary>
        private Bitmap anaglyphImage;

        /// <summary>
        /// Output processed Anaglyph image.
        /// </summary>
        private Bitmap waterImage;


        private MotionMode motionMode = MotionMode.Positional;

        /// <summary>
        /// Image path
        /// </summary>
        private string imagePath = @"Images";

        /// <summary>
        /// Mask image to isolate the down side of the image.
        /// </summary>
        private Emgu.CV.Image<Gray, byte> mask;

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

            this.mask = new Emgu.CV.Image<Gray, byte>(480, 640, new Gray(0));
            Gray color = new Gray(255);
            for (int x = 0; x < 480; x++)
            {
                for (int y = 500; y < 640; y++)
                {
                    mask[y, x] = color;
                }
            }
            //ImageViewer.Show(mask, "Test Window");
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
            this.MoveRobot((Button)sender, 0, 0);
        }

        private void btnForward_MouseDown(object sender, MouseEventArgs e)
        {
            this.MoveRobot((Button)sender, 50, 50);
        }

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            this.RotateRobot((Button)sender, 50, -50);
        }

        private void btnBackward_MouseDown(object sender, MouseEventArgs e)
        {
            this.MoveRobot((Button)sender, -50, -50);
        }

        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            this.RotateRobot((Button)sender, -50, 50);
        }

        private void btnForward_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.motionMode == MotionMode.Speed)
            {
                this.MoveRobot((Button)sender, 0, 0);
            }
        }

        private void btnRight_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.motionMode == MotionMode.Speed)
            {
                this.RotateRobot((Button)sender, 0, 0);
            }
        }

        private void btnBackward_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.motionMode == MotionMode.Speed)
            {
                this.MoveRobot((Button)sender, 0, 0);
            }
        }

        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.motionMode == MotionMode.Speed)
            {
                this.RotateRobot((Button)sender, 0, 0);
            }
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
            this.Process3D();
        }

        private void btnProcessWater_Click(object sender, EventArgs e)
        {
            this.ProcessWater();
        }

        private void save2DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveImage2D();
        }

        private void save3DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveImage3D();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveWaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveWaterImage();
        }

        private void btnProcessRocks_Click(object sender, EventArgs e)
        {
            this.ProcessRocks();
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

        /// <summary>
        /// Fit image from one size to input size.
        /// </summary>
        /// <param name="image">Input image.</param>
        /// <param name="size">New size.</param>
        /// <returns>Resized image.</returns>
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
        /// Process image to make it Anaglyph 3D.
        /// </summary>
        private void Process3D()
        {
            if (this.inputImage == null)
            {
                return;
            }

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
                this.anaglyphImage = Anaglyph.Make3DPopIn(new Bitmap((Image)this.inputImage), this.shiftValue);
                // Show the nwe mage.
                this.pbMain.Image = this.FitImage(this.anaglyphImage, this.pbMain.Size);
            });
            workerThread.Start();
        }

        /// <summary>
        /// Process water in the image.
        /// </summary>
        private void ProcessWater()
        {
            if (this.inputImage == null)
            {
                //return;
            }

            Thread workerThread = new Thread(() =>
            {
                string path = @"C:\Users\POLYGONTeam\Documents\GitHub\Androbot\Androbot\Androbot\bin\Debug\Images\2D_20160728173548.PNG";

                //Emgu.CV.Image<Bgr, byte> inpImg = new Emgu.CV.Image<Bgr, byte>(this.inputImage);
                Emgu.CV.Image<Bgr, byte> inpImg = new Emgu.CV.Image<Bgr, byte>(path);

                Emgu.CV.Image<Gray, byte> water = inpImg.InRange(new Bgr(100, 100, 0), new Bgr(255, 255, 255));
                water = water.Add(mask);
                //water._Dilate(1);

                // Create the blobs.
                Emgu.CV.Cvb.CvBlobs blobs = new Emgu.CV.Cvb.CvBlobs();
                // Create blob detector.
                Emgu.CV.Cvb.CvBlobDetector dtk = new Emgu.CV.Cvb.CvBlobDetector();
                // Detect blobs.
                uint state = dtk.Detect(water, blobs);

                foreach (Emgu.CV.Cvb.CvBlob blob in blobs.Values)
                {
                    //Console.WriteLine("Center: X:{0:F3} Y:{1:F3}", blob.Centroid.X, blob.Centroid.Y);
                    //Console.WriteLine("{0}", blob.Area);
                    if (blob.Area >= 4500 && blob.Area < 34465)
                    {
                        //Console.WriteLine("{0}", blob.Area);
                        inpImg.Draw(new CircleF(blob.Centroid, 5), new Bgr(Color.Red), 2);
                        inpImg.Draw(blob.BoundingBox, new Bgr(Color.Blue), 2);
                    }
                }


                if (this.waterImage != null) this.waterImage.Dispose();
                // Dump the image.
                this.waterImage = inpImg.ToBitmap();
                // Show the nwe mage.
                this.pbMain.Image = this.FitImage(this.waterImage, this.pbMain.Size);
            });
            workerThread.Start();
        }

        /// <summary>
        /// Process rocks in the image.
        /// </summary>
        private void ProcessRocks()
        {
            if (this.inputImage == null)
            {
                //return;
            }

            Thread workerThread = new Thread(() =>
            {
                string path = @"C:\Users\POLYGONTeam\Documents\GitHub\Androbot\Androbot\Androbot\bin\Debug\Images\2D_20160728170358.PNG";

                //Emgu.CV.Image<Bgr, byte> inpImg = new Emgu.CV.Image<Bgr, byte>(this.inputImage);
                Emgu.CV.Image<Bgr, byte> inpImg = new Emgu.CV.Image<Bgr, byte>(path);

                Emgu.CV.Image<Gray, byte> water = inpImg.InRange(new Bgr(0, 100, 0), new Bgr(255, 255, 255));
                water = water.Add(mask);
                //water._Dilate(1);

                // Create the blobs.
                Emgu.CV.Cvb.CvBlobs blobs = new Emgu.CV.Cvb.CvBlobs();
                // Create blob detector.
                Emgu.CV.Cvb.CvBlobDetector dtk = new Emgu.CV.Cvb.CvBlobDetector();
                // Detect blobs.
                uint state = dtk.Detect(water, blobs);

                foreach (Emgu.CV.Cvb.CvBlob blob in blobs.Values)
                {
                    //Console.WriteLine("Center: X:{0:F3} Y:{1:F3}", blob.Centroid.X, blob.Centroid.Y);
                    //Console.WriteLine("{0}", blob.Area);
                    if (blob.Area >= 4500 && blob.Area < 34465)
                    {
                        //Console.WriteLine("{0}", blob.Area);
                        inpImg.Draw(new CircleF(blob.Centroid, 5), new Bgr(Color.Red), 2);
                        inpImg.Draw(blob.BoundingBox, new Bgr(Color.Blue), 2);
                    }
                }
                
                if (this.waterImage != null) this.waterImage.Dispose();
                // Dump the image.
                this.waterImage = inpImg.ToBitmap();
                // Show the nwe mage.
                this.pbMain.Image = this.FitImage(this.waterImage, this.pbMain.Size);
            });

            workerThread.Start();
        }

        private void ProcessFaces()
        {
            if (this.inputImage == null)
            {
                //return;
            }

            Thread workerThread = new Thread(() =>
            {
                try
                {
                    string path = @"C:\Users\POLYGONTeam\Documents\GitHub\Androbot\Androbot\Androbot\bin\Debug\Images\2D_20160728170254.PNG";
                    string haarCascadeSettings = @"C:\Users\POLYGONTeam\Documents\GitHub\Androbot\Androbot\Androbot\bin\Debug\haarcascade_frontalface_alt_tree.xml";

                    //Emgu.CV.Image<Bgr, byte> inpImg = new Emgu.CV.Image<Bgr, byte>(this.inputImage);

                    Emgu.CV.Image<Bgr, byte> inpImg = new Emgu.CV.Image<Bgr, byte>(path);
                    if (!File.Exists(haarCascadeSettings))
                    {
                        return;
                    }

                    CascadeClassifier cascadeClassifier = new CascadeClassifier(haarCascadeSettings); // Application.StartupPath + 

                    var grayframe = inpImg.Convert<Gray, byte>();

                    var faces = cascadeClassifier.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here

                    foreach (var face in faces)
                    {
                        inpImg.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them
                    }

                    if (this.waterImage != null) this.waterImage.Dispose();
                    // Dump the image.
                    this.waterImage = inpImg.ToBitmap();
                    // Show the nwe mage.
                    this.pbMain.Image = this.FitImage(this.waterImage, this.pbMain.Size);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            });

            workerThread.Start();
        }

        /// <summary>
        /// Save the processed 3D image.
        /// </summary>
        private void SaveImage3D()
        {
            if(this.anaglyphImage == null)
            {
                return;
            }

            Thread workerThread = new Thread(() =>
            {
                // Generate the image name.
                string tmpPath = String.Format(@"3D_{0}_{1}.PNG", this.shiftValue, DateTime.Now.ToString("yyyyMMddHHmmss"));
                tmpPath = Path.Combine(this.imagePath, tmpPath);
                // Save the image.
                this.anaglyphImage.Save(tmpPath);
            });

            workerThread.Start();
        }

        /// <summary>
        /// Save 2D image.
        /// </summary>
        private void SaveImage2D()
        {
            if (this.inputImage == null)
            {
                return;
            }

            Thread workerThread = new Thread(() =>
            {
                // Generate the image name.
                string tmpPath = String.Format(@"2D_{0}.PNG", DateTime.Now.ToString("yyyyMMddHHmmss"));
                tmpPath = Path.Combine(this.imagePath, tmpPath);
                // Save the image.
                this.inputImage.Save(tmpPath);
            });

            workerThread.Start();
        }

        /// <summary>
        /// Save processed water image.
        /// </summary>
        private void SaveWaterImage()
        {
            if (this.waterImage == null)
            {
                return;
            }

            Thread workerThread = new Thread(() =>
            {
                // Generate the image name.
                string tmpPath = String.Format(@"2D_WATER_{0}.PNG", DateTime.Now.ToString("yyyyMMddHHmmss"));
                tmpPath = Path.Combine(this.imagePath, tmpPath);
                // Save the image.
                this.waterImage.Save(tmpPath);
            });

            workerThread.Start();
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

        #endregion

        #region Robot

        private void ConnectToRobot()
        {
            if (this.robot != null)
            {
                return;
            }

            // Gnerate the thread to do the job.
            Thread worker = new Thread(new ThreadStart(
                delegate ()
                {
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
                }));

            worker.Start();


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

        private void MoveRobot(Button button, int left, int right)
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
                        if (button.InvokeRequired)
                        {
                            button.BeginInvoke((MethodInvoker)delegate ()
                            {
                                button.Enabled = false;
                            });
                        }
                        else
                        {
                            button.Enabled = false;
                        }

                        // 
                        this.robot.Move(left, right);

                        // 
                        if (button.InvokeRequired)
                        {
                            button.BeginInvoke((MethodInvoker)delegate ()
                            {
                                button.Enabled = true;
                            });
                        }
                        else
                        {
                            button.Enabled = true;
                        }

                    }
                    catch (Exception exception)
                    {
                        // TODO: Log the error.
                    }
                }));

            worker.Start();
        }

        private void RotateRobot(Button button, int left, int right)
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
                        if (button.InvokeRequired)
                        {
                            button.BeginInvoke((MethodInvoker)delegate ()
                            {
                                button.Enabled = false;
                            });
                        }
                        else
                        {
                            button.Enabled = false;
                        }

                        // 
                        this.robot.Rotate(left, right);

                        // 
                        if (button.InvokeRequired)
                        {
                            button.BeginInvoke((MethodInvoker)delegate ()
                            {
                                button.Enabled = true;
                            });
                        }
                        else
                        {
                            button.Enabled = true;
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

            // If the camera is turned.
            this.inputImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            
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

        #region Mode Radio Buttons

        private void rbtnPositional_CheckedChanged(object sender, EventArgs e)
        {
            this.motionMode = MotionMode.Positional;
        }

        private void rbtnSpeed_CheckedChanged(object sender, EventArgs e)
        {
            this.motionMode = MotionMode.Speed;
        }


        #endregion

        private void btnProcessFaces_Click(object sender, EventArgs e)
        {
            this.ProcessFaces();
        }
    }
}
