using System;
using System.Drawing;

namespace Androbot.Androbot.Data.GeoLocation.Google
{
    public class ServiceData
    {
        private const string URI = "http://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom={2}&size={3}x{4}&maptype={5}";

        public static string CreateUri(float latitude, float longtitude, int zoom, Size size, string mapType)
        {

            //http://maps.googleapis.com/maps/api/staticmap?center=40.714728,-73.998672&zoom=12&size=400x400
            //satellite terrain  hybrid satellite

            string marker = String.Format("markers=color:blue%7Clabel:A%7C{0},{1}", latitude, longtitude);

            string lat = latitude.ToString("F7").Replace(',', '.');
            string lon = longtitude.ToString("F7").Replace(',', '.');
            string type = MapType.Validate(mapType) ? mapType : MapType.SATELITE;

            return Uri.EscapeUriString(String.Format(URI, lat, lon, zoom, size.Width, size.Height, type));
        }
    }
}
