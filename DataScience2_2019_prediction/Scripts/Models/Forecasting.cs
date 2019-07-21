using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience2_2019_prediction.Scripts
{
    class Forecasting
    {
        private double time;
        private double demand;
        private double level;
        private double trendLine;
        private double seasonal_Adj;
        private double onestep_for;
        private double for_err;
        private double squered_err;

        public Forecasting() { }

        public double Time { get => time; set => time = value; }
        public double Demand { get => demand; set => demand = value; }
        public double Level { get => level; set => level = value; }
        public double TrendLine { get => trendLine; set => trendLine = value; }
        public double Seasonal_Adj { get => seasonal_Adj; set => seasonal_Adj = value; }
        public double Onestep_for { get => onestep_for; set => onestep_for = value; }
        public double For_err { get => for_err; set => for_err = value; }
        public double Squered_err { get => squered_err; set => squered_err = value; }


    }
}
