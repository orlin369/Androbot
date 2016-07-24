using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Androbot.Androbot.Data.GeoLocation.Google
{
    public class MapType
    {
        public const string SATELITE = "satellite";
        public const string TERRENIAN = "terrain";
        public const string HYBRID = "hybrid";
        public const string ROADMAP = "roadmap";

        private static string[] asArray = new string[4] { ROADMAP, SATELITE, TERRENIAN, HYBRID };

        public static string[] AsArray
        {
            get
            {
                return asArray;
            }
        }

        public static bool Validate(string mapType)
        {
            foreach(string type in asArray)
            {
                if(type == mapType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
