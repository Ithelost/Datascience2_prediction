using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience2_2019_prediction.Scripts
{
    class Formules
    {
        private int season = 0;

        public Formules(int _season)
        {
            season = _season;
        }

        public double CalcLevel(double prevLevel, double prevTrend, double alpha, double demand, double SeasonalityMinSeason)
        {
            return prevLevel + prevTrend + alpha * (demand - (prevLevel + prevTrend) * SeasonalityMinSeason) / SeasonalityMinSeason;
        }

        // same thing diffrent formula

        //public double CalcLevel(double prevLevel, double prevTrend, double alpha, double demand, double SeasonalityMinSeason)
        //{
        //    return alpha * (demand / SeasonalityMinSeason) + (1 - alpha) * (prevLevel + prevTrend);
        //}

        public double CalcTrend(double prevLevel, double prevTrend, double gamma, double alpha, double demand, double SeasonalityMinSeason)
        {
            return prevTrend + gamma * alpha * (demand - (prevLevel + prevTrend) * SeasonalityMinSeason) / SeasonalityMinSeason;
        }

        public double CalcSeasonal(double prevLevel, double prevTrend, double delta, double alpha, double demand, double SeasonalityMinSeason)
        {
            return SeasonalityMinSeason + (delta * alpha) *  (demand - (prevLevel + prevTrend) * SeasonalityMinSeason) / (prevLevel + prevTrend);
        }

        public double CalcOneStepForcast(double prevLevel, double prevTrend, double SeasonalityMinSeason)
        {
            return (prevLevel + prevTrend) * SeasonalityMinSeason;
        }

        public double CalcForcastError(double demand, double oneStep)
        {
            return demand - oneStep;
        }

        public double CalcSequenceError(double forecastError)
        {
            return (forecastError * forecastError);
        }

        public double CalcSSE(List<Forecasting> forc)
        {
            double var = 0;
            for (int i = season; i < forc.Count - season; i++)
            {
                var += forc[i].Squered_err;
            }
            return var;
        }

        public double CalcStandardError(List<Forecasting> forc)
        {
            return Math.Sqrt(CalcSSE(forc) / ((forc.Count - ( 2 * season)) - 3));
        }

        public double CalcPrediction(double level, double m, double trend, double seasonal)
        {
            return (level + (m * trend)) * seasonal;
        }
    }
}
