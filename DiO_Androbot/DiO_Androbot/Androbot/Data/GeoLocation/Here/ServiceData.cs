using System;
using System.Drawing;

namespace DiO_Androbot.Androbot.Data.GeoLocation.Here
{
    public class ServiceData
    {
        private const string URI = "https://maps.here.com/?{0}&{1}&x=ep";

        public static string CreateUri(float latitude, float longtitude, int zoom, Size size, string mapType)
        {
            //https://maps.here.com/directions/mix/Simmeringer-Hauptstra%C3%9Fe-242,-1110-Vienna,-Austria:48.15191,16.44927/?start=48.18848%252C16.39942&hu&destination=48.12423%252C16.55854%252CVienna&map=48.12289,16.56163,16,satellite&msg=Schwechat
            //https://maps.here.com/?map=43.0737,25.61624,18,satellite&x=ep
            //https://developer.here.com/api-explorer/maps-js/markers/markers-on-the-map
            //https://maps.here.com/
            //?traffic =43.07737,25.6183,15,satellite
            //&map=43.07737,25.6183,15,satellite
            //&x=ep7

            string lat = latitude.ToString("F7").Replace(',', '.');
            string lon = longtitude.ToString("F7").Replace(',', '.');
            string type = MapType.Validate(mapType) ? mapType : MapType.SATELITE;
            string traffic = String.Format("traffic={0},{1},{2},{3}", lat, lon, zoom, type);
            string map = String.Format("map={0},{1},{2},{3}", lat, lon, zoom, type);

            return Uri.EscapeUriString(String.Format(URI, traffic, map));
        }
    }
}
