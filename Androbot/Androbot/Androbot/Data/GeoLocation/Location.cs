using System;

namespace Androbot.Androbot.Data.GeoLocation
{
    [Serializable]
    public class Location : EventArgs
    {
        public Network network { get; set; }
    }
}
