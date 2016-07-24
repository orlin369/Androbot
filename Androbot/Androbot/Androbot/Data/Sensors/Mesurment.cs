using System;

namespace Androbot.Androbot.Data.Sensors
{
    [Serializable]
    public class Mesurment : EventArgs
    {

        #region Properties

        public double accuracy{ get; set; } // 3,
        public double pitch { get; set; } // -0.47323511242866517,
        public double roll { get; set; } // -0.031366908922791481,
        public double azimuth { get; set; } // -0.26701245009899138,
        public double xmag { get; set; } // 1.75,
        public double ymag { get; set; } // -8.9375,
        public double zmag { get; set; } // -41.0625,
        public double zforce { get; set; } // 8.4718560000000007,
        public double yforce { get; set; } // 4.2495484000000001,
        public double xforce { get; set; } // 0.23154590999999999
        public long time{ get; set; } // 1297160391.2820001,

        #endregion

        #region Constructor

        /// <summary>
        /// Empty constructor for serialization.
        /// </summary>
        public Mesurment()
        {

        }

        #endregion

    }
}
