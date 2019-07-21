using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience2_2019_prediction.Scripts
{
    class Smoother
    {
        // optimale waarde (overgenomen van excel)
        private double alpha = 0.3071953425;
        private double delta = 0;
        private double gamma = 0.2285449336;

        //private double alpha = 0;
        //private double delta = 0;
        //private double gamma = 0;

        public Smoother() { }

        public double Alpha { get => alpha; set => alpha = value; }
        public double Delta { get => delta; set => delta = value; }
        public double Gamma { get => gamma; set => gamma = value; }
    }
}
