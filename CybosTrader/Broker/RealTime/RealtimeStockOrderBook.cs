using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CPSYSDIBLib;
using DSCBO1Lib;

namespace CybosTrader.Broker.RealTime
{
    class RealtimeStockOrderBook
    {
        public string Code { get; }
        private StockJpbid real = new StockJpbid();
        public RealtimeStockOrderBook(string code)
        {
            Code = code;
            real.SetInputValue(0, code);
            real.Received += OnOrderBookReceived;
        }

        public void Subscribe()
        {
            real.Subscribe();
        }

        public void UnSubscribe()
        {
            real.Unsubscribe();
        }

        private void OnOrderBookReceived()
        {
            var datetime = DateTime.Now.ToString("yyyyMMddHHMMssfff");
            long time = real.GetHeaderValue(1);
            long volume = real.GetHeaderValue(2);

            long ask1 = real.GetHeaderValue(3);
            long bid1 = real.GetHeaderValue(4);
            long ask1vol = real.GetHeaderValue(5);
            long bid1vol = real.GetHeaderValue(6);

            long ask2 = real.GetHeaderValue(7);
            long bid2 = real.GetHeaderValue(8);
            long ask2vol = real.GetHeaderValue(9);
            long bid2vol = real.GetHeaderValue(10);

            long ask3 = real.GetHeaderValue(11);
            long bid3 = real.GetHeaderValue(12);
            long ask3vol = real.GetHeaderValue(13);
            long bid3vol = real.GetHeaderValue(14);

            long ask4 = real.GetHeaderValue(15);
            long bid4 = real.GetHeaderValue(16);
            long ask4vol = real.GetHeaderValue(17);
            long bid4vol = real.GetHeaderValue(18);

            long ask5 = real.GetHeaderValue(19);
            long bid5 = real.GetHeaderValue(20);
            long ask5vol = real.GetHeaderValue(21);
            long bid5vol = real.GetHeaderValue(22);

            long ask6 = real.GetHeaderValue(27);
            long bid6 = real.GetHeaderValue(28);
            long ask6vol = real.GetHeaderValue(29);
            long bid6vol = real.GetHeaderValue(30);

            long ask7 = real.GetHeaderValue(31);
            long bid7 = real.GetHeaderValue(32);
            long ask7vol = real.GetHeaderValue(33);
            long bid7vol = real.GetHeaderValue(34);

            long ask8 = real.GetHeaderValue(35);
            long bid8 = real.GetHeaderValue(36);
            long ask8vol = real.GetHeaderValue(37);
            long bid8vol = real.GetHeaderValue(38);

            long ask9 = real.GetHeaderValue(39);
            long bid9 = real.GetHeaderValue(40);
            long ask9vol = real.GetHeaderValue(41);
            long bid9vol = real.GetHeaderValue(42);

            long ask10 = real.GetHeaderValue(43);
            long bid10 = real.GetHeaderValue(44);
            long ask10vol = real.GetHeaderValue(45);
            long bid10vol = real.GetHeaderValue(46);

            long totalask = real.GetHeaderValue(23);
            long totalbid = real.GetHeaderValue(24);

            Console.WriteLine($"{datetime} {time} {volume} ask1: {ask1} {ask1vol} bid1: {bid1} {bid1vol}");
        }
    }
}
