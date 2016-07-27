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
