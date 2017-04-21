using System;
using System.Windows;
namespace WPFLog4Net
{
    /// <summary>
    /// Interaction logic for Demo.xaml
    /// </summary>
    public partial class Demo : Window
    {
        ILogger logger = new Logger(typeof(Demo));
        public Demo()
        {
            InitializeComponent();

            var actionInfo = new ActionLoggerInfo()
            {
                Module = "Test Module",
                DateTime = DateTime.Now,
                Status = "Test Status",
                Message = "Test Message",
                UserId = 1,
                IPAddress = "127.0.0.1"
            };
       
            logger.LogError(actionInfo);
        }
    }
}
