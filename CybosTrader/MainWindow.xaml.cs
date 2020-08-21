using CPFOREDIBLIB;
using CybosTrader.Broker;
using CybosTrader.Broker.RealTime;
using CybosTrader.Broker.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CybosTrader
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        CybosClient client = null;
        RealtimeStockOrderBook orderbook = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //client.LoadStockCodes();
            LongChartDataRequest request = new LongChartDataRequest("A005930", 20200812, 20200813, period:'m');
            request.Request();
            orderbook.Subscribe();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            client = new CybosClient();
            orderbook = new RealtimeStockOrderBook("A005930");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (orderbook != null)
                orderbook.UnSubscribe();
        }
    }
}
