using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CPTRADELib;
using CPUTILLib;
using DSCBO1Lib;

namespace CybosTrader.Broker
{
    public class StockInfo
    {
        public string _id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int MarketType { get; set; }
        public int MemeMin { get; set; }
        public string IndustryCode { get; set; }
        public int Supervision { get; set; }
        public int ControlKind { get; set; }
        public int Status { get; set; }
        public int FiscalMonth { get; set; }
        public int Section { get; set; }
        public int ListedDate { get; set; }
        public string Date { get; set; }
    }

    public class CybosClient
    {
        CpCybos client = new CpCybos();
        CpCodeMgr codeMgr = new CpCodeMgr();
        CpStockCode stockCode = new CpStockCode();
        CpTdUtil tdUtil = new CpTdUtil();
        CpConclusion conclusion = new CpConclusion();
        public bool IsConnect { get; protected set; }
        Dictionary<string, StockInfo> codes = new Dictionary<string, StockInfo>();

        public CybosClient()
        {
            if (client.IsConnect == 1)
            {
                IsConnect = true;
                Console.WriteLine("Cybos Connect Success");
            }
            else
            {
                Console.WriteLine("Cybos Connect Failed");
            }
            client.OnDisconnect += OnDisconnectEventHandler;
        }

        private void OnDisconnectEventHandler()
        {
            IsConnect = false;
            Console.WriteLine("Cybos Disconnected");
        }

        public Dictionary<string, StockInfo> LoadStockCodes(bool cache=true, bool nowarn=true)
        {
            if (cache && codes.Count > 0)
                return codes;

            var date = DateTime.Now.ToString("yyyyMMdd");
            for(short i = 0; i < stockCode.GetCount(); i++)
            {
                var code = stockCode.GetData(0, i).ToString().Trim();
                var name = stockCode.GetData(1, i).ToString().Trim();
                var fullCode = stockCode.GetData(2, i).ToString().Trim();
                var memeMin = codeMgr.GetStockMemeMin(code);
                var industryCode = codeMgr.GetStockIndustryCode(code);
                var marketType = codeMgr.GetStockMarketKind(code);
                var controlKind = codeMgr.GetStockControlKind(code);
                var supervisionKind = codeMgr.GetStockSupervisionKind(code);
                var status = codeMgr.GetStockStatusKind(code);
                var fiscalMongth = codeMgr.GetStockFiscalMonth(code);
                var section = codeMgr.GetStockSectionKind(code);
                var listedDate = codeMgr.GetStockListedDate(code);
                var stockInfo = new StockInfo
                {
                    _id = code,
                    Code = code,
                    Name = name,
                    MarketType = (int)marketType,
                    MemeMin = memeMin,
                    IndustryCode = industryCode,
                    Supervision = (int)supervisionKind,
                    ControlKind = (int)controlKind,
                    Status = (int)status,
                    FiscalMonth = fiscalMongth,
                    Section = (int)section,
                    ListedDate = listedDate,
                    Date = date,
                };
                codes.Add(code, stockInfo);
            }

            var f = codes.First().Value;
            Console.WriteLine($"{f.Code} {f.Date} {f.MarketType} {f.MemeMin} {f.Status} {f.Supervision} {f.ControlKind}");
            return codes;
        }
    }
}
