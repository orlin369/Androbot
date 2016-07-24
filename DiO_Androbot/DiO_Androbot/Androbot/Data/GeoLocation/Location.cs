using System;

namespace DiO_Androbot.Androbot.Data.GeoLocation
{
    [Serializable]
    public class Location : EventArgs
    {
        public Network network { get; set; }
    }
}
