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
