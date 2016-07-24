using Androbot.Androbot.Data.GeoLocation;
using Androbot.Androbot.Data.Sensors;
using Androbot.Androbot.Events;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace Androbot.Androbot
{

    /// <summary>
    /// Androbot control class.
    /// </summary>
    public class Robot
    {

        #region Variables

        /// <summary>
        /// End point of the host.
        /// </summary>
        private IPEndPoint endPoint;

        #endregion

        #region Properties

        /// <summary>
        /// End point to the host.
        /// </summary>
        public IPEndPoint EndPoint
        {
            get
            {
                return this.endPoint;
            }
        }

        /// <summary>
        /// Is the robot is connected.
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Is geo location service stgarted.
        /// </summary>
        public bool IsGeoLocationStarted { get; private set; }

        /// <summary>
        /// Is sensors service stgarted.
        /// </summary>
        public bool IsSensorsStarted { get; private set; }

        /// <summary>
        /// Robot bluetooth MAC address.
        /// </summary>
        public string BTID { get; private set; }

        #endregion

        #region Events

        public EventHandler<EventArgImage> OnImageDeliver;
        

        public EventHandler<Location> OnLocationDelivered;

        public EventHandler<EventArgs> OnLocationStarted;

        public EventHandler<EventArgs> OnLocationStoped;


        public EventHandler<Mesurment> OnSensorsDelivered;

        public EventHandler<EventArgs> OnSensorsStarted;

        public EventHandler<EventArgs> OnSensorsStoped;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endPoint"></param>
        public Robot(IPEndPoint endPoint)
        {
            this.endPoint = endPoint;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create URI command string.
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="value">Value of the command.</param>
        /// <returns>URI string.</returns>
        private string CreateUri(string command, string value)
        {
            return Uri.EscapeUriString(String.Format("http://{0}:{1}/?command={2}&values={3}", this.endPoint.Address.ToString(), endPoint.Port, command, value)).ToString();
        }

        /// <summary>
        /// Make request to the server of the robot.
        /// </summary>
        /// <param name="uri">URI string.</param>
        /// <returns>Result of the server.</returns>
        private string MakeRequest(string uri)
        {
            string responseFromServer = "";

            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(uri);

            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                using (StreamReader reader = new StreamReader(stream))
                {
                    // Read the content.
                    responseFromServer = reader.ReadToEnd();
                }
            }

            // Retuen the data.
            return responseFromServer;
        }

        #endregion

        #region Connect / Disconnect

        /// <summary>
        /// Connect to the robot.
        /// </summary>
        /// <param name="textBTID">Bluetooth ID of the robot.</param>
        public void Connect(string textBTID)
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(this.CreateUri("connect", textBTID));
            
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            
            // Get the response.
            WebResponse response = request.GetResponse();

            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            
            // Display the content.
            Console.WriteLine(responseFromServer);

            // Clean up the streams and the response.
            reader.Close();

            // Close the response.
            response.Close();

            // Get the code.
            HttpStatusCode code = ((HttpWebResponse)response).StatusCode;

            // Check the state and set the propertie.
            if (code != HttpStatusCode.OK)
            {
                this.BTID = "";
                throw new InvalidOperationException(String.Format("HTTP status; {0}", code.ToString()));
            }
            else
            {
                this.BTID = textBTID;
            }
        }

        /// <summary>
        /// Disconnect from the robot.
        /// </summary>
        public void Disconnect()
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(this.CreateUri("disconnect", "null"));

            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            WebResponse response = request.GetResponse();

            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            // Display the content.
            Console.WriteLine(responseFromServer);

            // Clean up the streams and the response.
            reader.Close();

            response.Close();

            // Get the code.
            HttpStatusCode code = ((HttpWebResponse)response).StatusCode;

            if (code != HttpStatusCode.OK)
            {
                throw new InvalidOperationException(String.Format("HTTP status; {0}", code.ToString()));
            }
        }

        #endregion

        #region Camera

        /// <summary>
        /// Capture a picture from the camera. 
        /// </summary>
        /// <param name="camera">Camera index.</param>
        /// <returns>Image from the camera.</returns>
        public void Capture(int camera)
        {
            if (this.OnImageDeliver == null)
            {
                return;
            }

            WebRequest request = WebRequest.Create(this.CreateUri("capture", "1"));

            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                this.OnImageDeliver(this, new EventArgImage(Image.FromStream(stream)));
            }
        }

        #endregion

        #region Motion

        /// <summary>
        /// Stop the robot.
        /// </summary>
        public void StopMoveing()
        {
            string responseFromServer = this.MakeRequest(this.CreateUri("robot_stop", "null"));
        }

        /// <summary>
        /// Move the robot.
        /// </summary>
        /// <param name="distance">Distance [mm]</param>
        /// <remarks>
        /// http://127.0.0.1:8080/?command=robot_move&value=360.5
        /// </remarks>
        public void Move(double distance, double speed)
        {
            string sDistance = distance.ToString("F3").Replace(',', '.');
            string sSpeed = speed.ToString("F3").Replace(',', '.');

            string value = String.Format("{0}|{1}", sDistance, sSpeed);

            string responseFromServer = this.MakeRequest(this.CreateUri("robot_move", value));
        }

        /// <summary>
        /// Rotate the robot.
        /// </summary>
        /// <param name="degree">Degree [deg]</param>
        /// <remarks>
        /// http://127.0.0.1:8080/?command=robot_rotate&value=360.5
        /// </remarks>
        public void Rotate(double degree, double speed)
        {
            string sDegree = degree.ToString("F3").Replace(',', '.');
            string sSpeed = speed.ToString("F3").Replace(',', '.');

            string value = String.Format("{0}|{1}", sDegree, sSpeed);

            string responseFromServer = this.MakeRequest(this.CreateUri("robot_rotate", value));
        }

        /// <summary>
        /// Move the robot in diferrential.
        /// </summary>
        /// <param name="lDistance"></param>
        /// <param name="lSpeed"></param>
        /// <param name="rDistance"></param>
        /// <param name="rSpeed"></param>
        /// <remarks>
        /// http://127.0.0.1:8080/?command=robot_curve&value=360.5
        /// </remarks>
        public void Curve(double lDistance, double lSpeed, double rDistance, double rSpeed)
        {
            string slDistance = lDistance.ToString("F3").Replace(',', '.');
            string srDistance = rDistance.ToString("F3").Replace(',', '.');
            string slSpeed = lSpeed.ToString("F3").Replace(',', '.');
            string srSpeed = rSpeed.ToString("F3").Replace(',', '.');

            string value = String.Format("{0}|{1}|{2}|{3}", slDistance, slSpeed, srDistance, srSpeed);

            string responseFromServer = this.MakeRequest(this.CreateUri("robot_curve", value));
        }

        #endregion

        #region Speach

        /// <summary>
        /// Move the robot in diferrential.
        /// </summary>
        /// <param name="text">Text to speech.</param>
        /// <remarks>
        /// http://127.0.0.1:8080/?command=telk&value=text
        /// </remarks>
        public void Talk(string text)
        {
            string responseFromServer = this.MakeRequest(this.CreateUri("talk", text));
        }

        #endregion

        #region Location

        /// <summary>
        /// Start the geo location service.
        /// </summary>
        public void StartGeoLocation()
        {
            string responseFromServer = this.MakeRequest(this.CreateUri("location", "start"));

            this.IsGeoLocationStarted = true;

            this.OnLocationStarted?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Stop the geo location service.
        /// </summary>
        public void StopGeoLocation()
        {
            string responseFromServer = this.MakeRequest(this.CreateUri("location", "stop"));
            
            this.IsGeoLocationStarted = false;

            this.OnLocationStoped?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Get the geo location.
        /// </summary>
        public void GetGeoLocation()
        {
            string responseFromServer = this.MakeRequest(this.CreateUri("location", "get"));

            this.OnLocationDelivered?.Invoke(this, JsonConvert.DeserializeObject<Location>(@responseFromServer));
        }

        #endregion

        #region Sensors

        /// <summary>
        /// Start sensors service.
        /// </summary>
        public void StartSensors()
        {
            string responseFromServer = this.MakeRequest(this.CreateUri("sensors", "start"));

            this.IsSensorsStarted = true;

            this.OnSensorsStarted?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Stop sensors service.
        /// </summary>
        public void StopSensors()
        {
            string responseFromServer = this.MakeRequest(this.CreateUri("sensors", "stop"));
            
            this.IsSensorsStarted = false;

            this.OnSensorsStoped?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Get sensors data.
        /// </summary>
        public void GetSensors()
        {
            string responseFromServer = this.MakeRequest(this.CreateUri("sensors", "get"));
            
            this.OnSensorsDelivered?.Invoke(this, JsonConvert.DeserializeObject<Mesurment>(@responseFromServer));
        }

        #endregion

    }
}
