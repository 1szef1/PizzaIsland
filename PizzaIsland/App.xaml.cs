using MailSender.Classes;
using PizzaIsland.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace PizzaIsland
{
    public partial class App : Application
    {
        public static string Name => "Pizza Island";

        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Configuration.Exist())
            {
                SenderBox sb = new SenderBox();
                sb.Show();
            }
            else if (e.Args.Count() > 0 && e.Args[0].Equals("config"))
            {
                SenderBox sb = new SenderBox();
                sb.Closed += SenderBox_Closed;
                sb.Show();
            }
            else
            {
                OrderCreator mw = new OrderCreator();
                mw.Show();
            }
        }

        private void SenderBox_Closed(object sender, EventArgs e)
        {
            OrderCreator mw = new OrderCreator();
            mw.Show();
        }
    }
}
