using System;

namespace Androbot.Androbot.Data.GeoLocation
{
    [Serializable]
    public class Network
    {
        public float bearing;
        public float altitude;
        public long time;
        public float longitude;
        public string provider;
        public float latitude;
        public float speed;
        public float accuracy;
    }
}
