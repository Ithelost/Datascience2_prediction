using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience2_2019_prediction.Scripts
{
    class Main
    {
        List<Forecasting> forcs = new List<Forecasting>();
        Formules formula;
        Smoother smoother = new Smoother();

        double bestSSE = double.MaxValue;
        double curSSE = double.MaxValue;

        int seasonTime;

        public Main(List<Forecasting> _forcs, Formules _for, Smoother _smoother, int _seasonTime)
        {
            forcs = _forcs;
            formula = _for;
            smoother = _smoother;
            seasonTime = _seasonTime;
        }

        public void Run()
        {
            for (int i = 12; i < forcs.Count; i++)
            {
                forcs[i].Level = formula.CalcLevel(forcs[i - 1].Level, forcs[i - 1].TrendLine, smoother.Alpha, forcs[i].Demand, forcs[i - seasonTime].Seasonal_Adj);
                forcs[i].TrendLine = formula.CalcTrend(forcs[i - 1].Level, forcs[i - 1].TrendLine, smoother.Gamma, smoother.Alpha, forcs[i].Demand, forcs[i - seasonTime].Seasonal_Adj);
                forcs[i].Seasonal_Adj = formula.CalcSeasonal(forcs[i - 1].Level, forcs[i - 1].TrendLine, smoother.Delta, smoother.Alpha, forcs[i].Demand, forcs[i - seasonTime].Seasonal_Adj);
                forcs[i].Onestep_for = formula.CalcOneStepForcast(forcs[i - 1].Level, forcs[i - 1].TrendLine, forcs[i - seasonTime].Seasonal_Adj);
                forcs[i].For_err = formula.CalcForcastError(forcs[i].Demand, forcs[i].Onestep_for);
                forcs[i].Squered_err = formula.CalcSequenceError(forcs[i].For_err);
            }

            curSSE = formula.CalcStandardError(forcs);
            double standardError = formula.CalcStandardError(forcs);
        }

        public void Prediction(int n, double m)
        {
            int iteration = 0;
            Console.WriteLine("Updating prediction");
            for (int k = 48; k < forcs.Count; k++)
            {
                Forecasting timeToPredict = forcs[k];
                m = timeToPredict.Time - n;
                int t = (int)(n - seasonTime + 1 + ((m - 1) % seasonTime));
                double SA = forcs[t].Seasonal_Adj;
                double demand = formula.CalcPrediction(timeToPredict.Level, m, timeToPredict.TrendLine, SA);
                timeToPredict.Demand = demand;
                Console.WriteLine($"Demand = {demand} at time = {timeToPredict.Time}");
                Run();
            }
        }

        public void UpdateSmoothness(double stapwaarde)
        {
            double bestAlpha = 0;
            double bestDelta = 0;
            double bestGamma = 0;
            double stapwaardeNew = stapwaarde / 10;

            // first run
            for (smoother.Alpha = 0; smoother.Alpha <= 1; smoother.Alpha = smoother.Alpha + stapwaarde)
            {
                for (smoother.Delta = 0; smoother.Delta <= 1; smoother.Delta = smoother.Delta + stapwaarde)
                {
                    for (smoother.Gamma = 0; smoother.Gamma <= 1; smoother.Gamma = smoother.Gamma + stapwaarde)
                    {
                        Run();
                        if (curSSE < bestSSE)
                        {
                            bestSSE = curSSE;
                            bestAlpha = smoother.Alpha;
                            bestDelta = smoother.Delta;
                            bestGamma = smoother.Gamma;
                        }
                    }
                }
            }

            //second run with new stapwaarde
            for (smoother.Alpha = bestAlpha - stapwaardeNew; smoother.Alpha <= bestAlpha + stapwaardeNew; smoother.Alpha = smoother.Alpha + stapwaarde)
            {
                for (smoother.Delta = bestDelta - stapwaardeNew; smoother.Delta <= bestDelta + stapwaardeNew; smoother.Delta = smoother.Delta + stapwaarde)
                {
                    for (smoother.Gamma = bestGamma - stapwaardeNew; smoother.Gamma <= bestGamma + stapwaardeNew; smoother.Gamma = smoother.Gamma + stapwaarde)
                    {
                        Run();
                        if (curSSE < bestSSE)
                        {
                            bestSSE = curSSE;
                            bestAlpha = smoother.Alpha;
                            bestDelta = smoother.Delta;
                            bestGamma = smoother.Gamma;
                        }
                    }
                }
            }

            // save the best stapwaarde
            smoother.Alpha = bestAlpha;
            smoother.Delta = bestDelta;
            smoother.Gamma = bestGamma;
            Console.WriteLine("Updating Alpha, Delta, Gamma");
            Console.WriteLine($"best Alpha: {bestAlpha} best Delta = {bestDelta} best Gamma = {bestGamma} best SSE = {bestSSE}");
        }

        public void UpdateSmoothness(double stapwaarde, bool firstRun)
        {
            double bestAlpha = smoother.Alpha;
            double bestDelta = smoother.Delta;
            double bestGamma = smoother.Gamma;

            double minValueOfAlpha = 0;
            double minValueOfDelta = 0;
            double minValueOfGamma = 0;
            double maxValueOfAlpha = 1;
            double maxValueOfDelta = 1;
            double maxValueOfGamma = 1;

            if (!firstRun)
            {
                minValueOfAlpha = smoother.Alpha - stapwaarde;
                minValueOfDelta = smoother.Delta - stapwaarde;
                minValueOfGamma = smoother.Gamma - stapwaarde;
                maxValueOfAlpha = smoother.Alpha + stapwaarde;
                maxValueOfDelta = smoother.Delta + stapwaarde;
                maxValueOfGamma = smoother.Gamma + stapwaarde;
            }

            for (smoother.Alpha = minValueOfAlpha; smoother.Alpha <= maxValueOfAlpha; smoother.Alpha = smoother.Alpha + stapwaarde)
            {
                for (smoother.Delta = minValueOfDelta; smoother.Delta <= maxValueOfDelta; smoother.Delta = smoother.Delta + stapwaarde)
                {
                    for (smoother.Gamma = minValueOfGamma; smoother.Gamma <= maxValueOfGamma; smoother.Gamma = smoother.Gamma + stapwaarde)
                    {
                        Run();
                        if (curSSE < bestSSE)
                        {
                            bestSSE = curSSE;
                            bestAlpha = smoother.Alpha;
                            bestDelta = smoother.Delta;
                            bestGamma = smoother.Gamma;
                        }
                    }
                }
            }

            smoother.Alpha = bestAlpha;
            smoother.Delta = bestDelta;
            smoother.Gamma = bestGamma;
            Console.WriteLine("Updating Alpha, Delta, Gamma");
            Console.WriteLine($"best Alpha: {bestAlpha} best Delta = {bestDelta} best Gamma = {bestGamma} best SSE = {bestSSE}");
        }
    }
}
