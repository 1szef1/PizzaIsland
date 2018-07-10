using MailSender.Classes;
using MailSender.Model;
using PizzaIsland.Classes;
using PizzaIsland.Extensions;
using System;
using System.Windows;
using System.Windows.Input;
using MSB = Xceed.Wpf.Toolkit.MessageBox;

namespace PizzaIsland.Windows
{
    public partial class SenderBox : Window
    {
        private SenderInfo boxInfo;
        public SenderInfo BoxInfo => boxInfo;
        public string Password { get; set; }

        public SenderBox()
        {
            InitializeComponent();
            this.Loaded += SenderBox_Loaded;
        }

        private void SenderBox_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                busyIndicator.IsBusy = true;

                if (Configuration.Exist())
                    boxInfo = Configuration.Get();

                if (boxInfo == null)
                    boxInfo = new SenderInfo();

                this.DataContext = this;
            }
            finally
            {
                busyIndicator.IsBusy = false;
            }
        }

        #region Commands

        public static readonly RoutedUICommand SaveCommand = new RoutedUICommand("Save", "SaveCommand", typeof(SenderBox));
        public static readonly RoutedUICommand TestCommand = new RoutedUICommand("Test", "TestCommand", typeof(SenderBox));

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Configuration.Configure(boxInfo, pwdBox.Password);
                this.Close();
            }
            catch (Exception ex)
            {
                MSB.Show(ex.Message, "Ustawienia skrzynki nadawczej", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ReadtToSave();
        }

        private void Test_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                btnSave.IsEnabled = btnTest.IsEnabled = false;

                var dr = MSB.Show("Zostanie wysłana wiadomość testowa. Kontynuować?", "Wiadomość testowa",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (dr != MessageBoxResult.Yes) return;

                boxInfo.SetPassword(pwdBox.Password);
                new Resender().SendTestMessage(boxInfo, boxInfo.FromEmail);

                MSB.Show("Wiadomość wysłana.", "Test skrzynki nadawczej", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MSB.Show(ex.Message, "Test skrzynki nadawczej", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                btnSave.IsEnabled = btnTest.IsEnabled = true;
            }
        }

        private void Test_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ReadtToSave();
        }

        private bool ReadtToSave()
        {
            return boxInfo != null
                && boxInfo.FromEmail.IsValidEmail()
                && !string.IsNullOrEmpty(boxInfo.FromEmail)
                && !string.IsNullOrEmpty(boxInfo.User)
                && !string.IsNullOrEmpty(pwdBox.Password)
                && !string.IsNullOrEmpty(boxInfo.Smtp)
                && boxInfo.Port > 0;
        }

        #endregion
    }
}
