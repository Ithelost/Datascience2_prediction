using DataScience2_2019_prediction.Scripts;
using System;
using System.Collections.Generic;

namespace DataScience2_2019_prediction
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader();
            List<Forecasting> forcs = reader.ParseData();

            int seasonTime = 12;
            double stapwaarde = 0.1;
            double stapwaardeNew = stapwaarde / 10;

            int totalData = 48;
            int predictMonth = 3;

            Formules func = new Formules(seasonTime);
            Smoother smoother = new Smoother();

            Main main = new Main(forcs, func, smoother, seasonTime);
            main.Run(); // run once

            //Update smoothnes of Alpha, Delta, Gamma
            //main.UpdateSmoothness(stapwaarde);
            //main.UpdateSmoothness(stapwaarde, true); 
            //main.UpdateSmoothness(stapwaardeNew, false);

            // prediction
            main.Prediction(totalData, predictMonth);

            Console.ReadLine();
        }
    }
}
