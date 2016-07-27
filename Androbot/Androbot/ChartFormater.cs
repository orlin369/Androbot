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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace Androbot
{
    public class ChartFormater
    {
        private List<float> dataContent;

        private Series dataSeries;

        public int ChartLength { get; set; }

        public ChartFormater(Series dataSeries)
        {
            this.dataSeries = dataSeries;
            this.ChartLength = 100;
            this.dataContent = new List<float>();
        }

        public Series AddValue(float value)
        {
            this.dataContent.Add(value);
            if(this.dataContent.Count > this.ChartLength)
            {
                this.dataContent.RemoveAt(0);
            }

            this.dataSeries.Points.Clear();

            for(int index = 0; index < this.ChartLength; index++)
            {
                this.dataSeries.Points.AddXY(index, this.dataContent[index]);
            }

            return this.dataSeries;
        }

    }
}