
namespace DiO_Androbot.Androbot.Data.GeoLocation.Here
{
    public class MapType
    {
        public const string SATELITE = "satellite";
        public const string NORMAL = "normal";
        public const string TERRAIN = "terrain";

        private static string[] asArray = new string[3] { SATELITE, NORMAL, TERRAIN };

        public static string[] AsArray
        {
            get
            {
                return asArray;
            }
        }

        public static bool Validate(string mapType)
        {
            foreach (string type in asArray)
            {
                if (type == mapType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
