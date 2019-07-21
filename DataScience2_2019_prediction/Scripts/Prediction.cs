using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience2_2019_prediction.Scripts
{
    class Prediction
    {
        public double CalcPrediction(double level, int m, double trend, double seasonal)
        {
            return (level + m * trend) * seasonal;
        }
    }
}
