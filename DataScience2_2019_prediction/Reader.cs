using DataScience2_2019_prediction.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataScience2_2019_prediction
{
    class Reader
    {
        public List<Forecasting> ParseData()
        {
            List<List<double>> data = Read();

            List<Forecasting> timeSeries = new List<Forecasting>();
            for (int i = 0; i < data.Count; i++)
            {
                Forecasting time = new Forecasting();
                if (i < 12)
                {
                    time.Time = data[i][0];
                    time.Demand = data[i][1];
                    time.Level = data[i][2];
                    time.TrendLine = data[i][3];
                    time.Seasonal_Adj = data[i][4];
                    time.Onestep_for = data[i][5];
                    time.For_err = data[i][6];
                    time.Squered_err = data[i][7];
                }
                else if (i >= 12 && i < 48)
                {
                    time.Time = data[i][0];
                    time.Demand = data[i][1];
                }
                else
                {
                    time.Time = data[i][0];
                }
                timeSeries.Add(time);
            }
            return timeSeries;
        }

        private List<List<double>> Read()
        {
            StreamReader reader = new StreamReader(@"D:\DocumentenOpD\Documenten\Unity\Datascience2_prediction\DataScience2_2019_prediction\Resources\data.csv");
            List<List<double>> data = new List<List<double>>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(';');
                List<double> row = new List<double>();
                for (int i = 0; i < values.Length; i++)
                {
                    row.Add(Convert.ToDouble(values[i]));
                }
                data.Add(row);
            }
            reader.Close();

            return data;
        }

    }
}

