using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CPSYSDIBLib;
using MongoDB.Driver.Core.Events;

namespace CybosTrader.Broker.Request
{
    class MarketEyeRequest
    {
        CPSYSDIBLib.MarketEye eye = new CPSYSDIBLib.MarketEye();
        public List<string> Codes { get; }
        public int[] fields;

        public MarketEyeRequest(List<string> codes)
        {
            Codes = codes;
            fields = new int[]{
                0, 4, 10, 11, 12, 20, 21, 22, 23, 67,
                70, 71, 73, 74, 75, 76, 77, 78, 79, 80,
                86, 87, 88, 89, 90, 91, 92, 93, 95, 96, 101,
                102, 103, 104, 105, 107, 110, 111, 123, 124, 125,
            };
        }

        public void Request()
        {
            SetInputValue();
            var status = eye.BlockRequest();
            if (status == 4)
            {
                throw new TimeoutException($"LongChartDataRequest TimeoutException: {status}");
            }
            else if (status != 0)
            {
                throw new Exception($"LongChartDataRequest Exception: {status}");
            }
            GetValue();
        }

        private void GetValue()
        {
            var count = eye.GetHeaderValue(2);

            for(var i = 0; i < count; i++)
            {
                var code = eye.GetDataValue(0, i);
                var price = eye.GetDataValue(1, i);
                var volume = eye.GetDataValue(2, i);
                var vol_price = eye.GetDataValue(3, i);
                var market = eye.GetDataValue(4, i);
                var share_count = eye.GetDataValue(5, i);
                var fn_ratio = eye.GetDataValue(6, i);
                var y_volume = eye.GetDataValue(7, i);
                var y_price = eye.GetDataValue(8, i);
                var per = eye.GetDataValue(9, i);
                var eps = eye.GetDataValue(10, i);
                var capital = eye.GetDataValue(11, i);
                var divid = eye.GetDataValue(12, i);
                var divid_yield = eye.GetDataValue(13, i);
                var debt_ratio = eye.GetDataValue(14, i);
                var reserve_rate = eye.GetDataValue(15, i);
                var roe = eye.GetDataValue(16, i);
                var revenue_gr = eye.GetDataValue(17, i);
                var ordinary_income_gr = eye.GetDataValue(18, i);
                var net_income_gr = eye.GetDataValue(19, i);
                var revenue = eye.GetDataValue(20, i);
                var ordinary_income = eye.GetDataValue(21, i);
                var net_income = eye.GetDataValue(22, i);
                var bps = eye.GetDataValue(23, i);
                var operating_income_gr = eye.GetDataValue(24, i);
                var operating_income = eye.GetDataValue(25, i);
                var revenu_oper_income_ratio = eye.GetDataValue(26, i);
                var revenu_ord_income_ratio = eye.GetDataValue(27, i);
                var date_af = eye.GetDataValue(28, i);
                var bps_qf = eye.GetDataValue(29, i);
                var revenue_qf = eye.GetDataValue(30, i);
                var income_qf = eye.GetDataValue(31, i);
                var ordinary_income_qf = eye.GetDataValue(32, i);
                var net_income_qf = eye.GetDataValue(33, i);
                var operating_margin_qf = eye.GetDataValue(34, i);
                var roe_qf = eye.GetDataValue(35, i);
                var debit_ratio_qf = eye.GetDataValue(36, i);
                var date_qf = eye.GetDataValue(37, i);
                var sps = eye.GetDataValue(38, i);
                var cfps = eye.GetDataValue(39, i);
                var ebita = eye.GetDataValue(40, i);
                Console.WriteLine($"{code} {share_count} {eps} {bps} {sps} {cfps} {roe} {divid}");

            }
        }

        private void SetInputValue()
        {
            eye.SetInputValue(0, fields);
            eye.SetInputValue(1, Codes.ToArray());
        }
    }
}
