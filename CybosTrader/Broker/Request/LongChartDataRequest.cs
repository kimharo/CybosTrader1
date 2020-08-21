using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using CPSYSDIBLib;
using MongoDB.Driver.Core.Events;

namespace CybosTrader.Broker.Request
{
    public class ChartData
    {
        public long Date { get; set; }
        public long Time { get; set; }
        public long Open { get; set; }
        public long High { get; set; }
        public long Low { get; set; }
        public long Close { get; set; }
        public long Volume { get; set; }
        public long VolumePrice { get; set; }
        public long StockCount { get; set; }
        public float ForeignRatio { get; set; }
        public float AdjustRatio { get; set; }
    }

    public class LongChartDataRequest
    {
        private StockChart chart = new StockChart();
        public string Code { get; }
        public ulong Start { get; }
        public ulong End { get; }
        public char Period { get; }
        private int[] fields;

        public LongChartDataRequest(string code, ulong start, ulong end, char period='D')
        {
            Code = code;
            Start = start;
            End = end;
            Period = period;
            fields = new int[] { 0, 1, 2, 3, 4, 5, 8, 9, 12, 17, 19 };
        }

        protected void SetInputValue()
        {
            chart.SetInputValue(0, Code);
            chart.SetInputValue(1, '1');
            chart.SetInputValue(2, End);
            chart.SetInputValue(3, Start);
            chart.SetInputValue(5, fields);
            chart.SetInputValue(6, Period);
            chart.SetInputValue(9, '1');
        }

        protected List<ChartData> GetValue()
        {
            List<ChartData> datas = new List<ChartData>();
            int count = chart.GetHeaderValue(3);
            Console.WriteLine($"count: ${count}");
            for (var i = 0; i < count; i++)
            {
                long date = (long)chart.GetDataValue(0, i);
                long time = chart.GetDataValue(1, i);
                long open = chart.GetDataValue(2, i);
                long high = chart.GetDataValue(3, i);
                long low = chart.GetDataValue(4, i);
                long close = chart.GetDataValue(5, i);
                long volume = (long)chart.GetDataValue(6, i);
                long volumePrice = (long)chart.GetDataValue(7, i);
                long stockCounts = (long)chart.GetDataValue(8, i);
                float foreignRatio = chart.GetDataValue(9, i);
                float adjustRatio = chart.GetDataValue(10, i);
                Console.WriteLine($"{date} {time} {open} {high} {low} {close} {volume}");
                var data = new ChartData { 
                    Date = date,
                    Time = time,
                    Open = open,
                    High = high,
                    Low = low,
                    Close = close,
                    Volume = volume,
                    VolumePrice = volumePrice,
                    StockCount = stockCounts,
                    ForeignRatio = foreignRatio,
                    AdjustRatio = adjustRatio,

                };
                datas.Add(data);
            }
            return datas;
        }

        public List<ChartData> Request()
        {
            SetInputValue();
            List<ChartData> datas = new List<ChartData>();
            while (true)
            {
                var status = chart.BlockRequest();
                if (status == 4)
                {
                    throw new TimeoutException($"LongChartDataRequest TimeoutException: {status}");
                }
                else if (status != 0)
                {
                    throw new Exception($"LongChartDataRequest Exception: {status}");
                }

                var results = chart.GetDibStatus();
                if (results != 0)
                {
                    var msg = chart.GetDibMsg1();
                    throw new Exception($"chart request error({results}): {msg}");
                }
                var data = GetValue();
                if (data.Count > 0)
                    datas.AddRange(data);
                if (chart.Continue == 0)
                {
                    Console.WriteLine($"Continue {chart.Continue}");
                    break;
                }
                else
                    Thread.Sleep(300);

            }

            return datas;
        }
    }
}
