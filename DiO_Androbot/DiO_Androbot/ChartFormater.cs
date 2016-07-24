using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace DiO_Androbot
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