using MailSender.Interfaces;
using PizzaIsland.Classes;
using PizzaIsland.Data;
using PizzaIsland.Data.Model;
using PizzaIsland.Data.Services;
using PizzaIsland.Data.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using MSB = Xceed.Wpf.Toolkit.MessageBox;

namespace PizzaIsland.Windows
{
    public partial class OrderCreator : Window, INotifyPropertyChanged
    {
        #region Fields 

        private OrderService service;
        private OrderHeader order;
        private Product selectedProduct;

        #endregion

        #region Bindings properties

        public OrderHeader Order => order;
        private decimal orderValue;
        public decimal OrderValue
        {
            get
            {
                return orderValue;
            }
            set
            {
                orderValue = value;
                OnPropertyRaised(nameof(OrderValue));
            }
        }

        #endregion

        #region Constructor and init

        public OrderCreator()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += OrderCreator_Loaded;
        }

        private void OrderCreator_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(PIContext.DbName))
                Initializer.CreateDatabaseWithData();

            LoadData();
            InitOrder();
        }

        private void InitOrder()
        {
            order = new OrderHeader();
            dataGrid.ItemsSource = order.OrderItems.ToList();
        }

        #endregion

        #region Data

        private void LoadData()
        {
            service = new OrderService();

            var p = service.GetProductsByType(ProductEnum.Pizza);
            if (p.Count > 0)
            {
                expPizzy.Header = p[0].TypeName;
                lbPizzy.ItemsSource = p;
            }

            var pa = service.GetProductsByType(ProductEnum.PizzaAddOn);
            if (pa.Count > 0)
            {
                expPizzaAddOns.Header = pa[0].TypeName;
                lbPizzaAddOns.ItemsSource = pa;
            }

            var md = service.GetProductsByType(ProductEnum.MainDish);
            if (md.Count > 0)
            {
                expMainDishes.Header = md[0].TypeName;
                lbMainDishes.ItemsSource = md;
            }

            var mda = service.GetProductsByType(ProductEnum.MainDishAddOn);
            if (mda.Count > 0)
            {
                expMainDishAddOns.Header = mda[0].TypeName;
                lbMainDishAddOns.ItemsSource = mda;
            }

            var s = service.GetProductsByType(ProductEnum.Soup);
            if (s.Count > 0)
            {
                expSoups.Header = s[0].TypeName;
                lbSoups.ItemsSource = s;
            }

            var d = service.GetProductsByType(ProductEnum.Drink);
            if (d.Count > 0)
            {
                expDrinks.Header = d[0].TypeName;
                lbDrinks.ItemsSource = d;
            }
        }

        #endregion

        #region Product moving

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProduct = (sender as ListBox)?.SelectedItem as Product;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var p = selectedProduct;
            if (p == null) return;

            if (order.OrderItems.Any(x => x.ProductId == p.Id)) 
                return;

            var oi = new OrderItem()
            {
                Product = p,
                ProductId = p.Id,
                Count = 1,
                Price = p.Price.HasValue ? p.Price.Value : p.Type?.Price ?? 0
            };

            order.OrderItems.Add(oi);
            RefreshData();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var oh = dataGrid.SelectedItem as OrderItem;
            if (oh == null) return;

            order.OrderItems.Remove(oh);
            RefreshData();
        }

        private void RefreshData()
        {
            if (dataGrid.IsBeingEdited)
                dataGrid.EndEdit();

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = new ObservableCollection<OrderItem>(order.OrderItems);

            AddOnsEnabled();

            RecountOrderValue();
        }

        private void AddOnsEnabled()
        {
            lbPizzaAddOns.IsEnabled = order.OrderItems.Any(x => x.Product.TypeNr == ProductEnum.Pizza);
            lbMainDishAddOns.IsEnabled = order.OrderItems.Any(x => x.Product.TypeNr == ProductEnum.MainDish);
        }

        private void RecountOrderValue()
        {
            OrderValue = order.OrderItems.Sum(x => x.Price * x.Count);
        }

        private void CountUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var currValue = sender as IntegerUpDown;
            if (currValue == null || !currValue.Value.HasValue) return;

            var selected = dataGrid.SelectedItem as OrderItem;
            if (selected == null) return;

            var val1 = order.OrderItems.Where(x => x.ProductId != selected.ProductId).Sum(x => x.Price * x.Count);
            var val2 = currValue.Value.Value * selected.Price;
            OrderValue = val1 + val2;
        }

        #endregion

        #region Order actions

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (order.OrderItems.Count == 0)
            {
                MSB.Show("Brak produktów.", "Składanie zamówienia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                MSB.Show("Nieustawiony email.", "Składanie zamówienia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dr = MSB.Show("Złożyć zamówienie?", "Składanie zamówienia", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dr != MessageBoxResult.Yes) return;

            order.Date = DateTime.Now;
            order.Email = tbEmail.Text;
            order.Comments = tbComments.Text;

            try
            {
                service.AddOrder(order);
                MSB.Show("Zamównie przyjęte.", "Składanie zamówienia", MessageBoxButton.OK, MessageBoxImage.Information);

                new Resender().SendOrderMessage(order);
                MSB.Show("Email wysłany.", "Składanie zamówienia", MessageBoxButton.OK, MessageBoxImage.Information);
                order.EmailSent = true;
                service.SaveChanges();

                this.Close();
            }
            catch (Exception ex)
            {
                MSB.Show(ex.Message, "Składanie zamówienia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            string email = tbEmail.Text;

            if (string.IsNullOrEmpty(email))
            {
                MSB.Show("Nieustawiony email.", "Historia zamówień", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dr = MSB.Show("Zostanie wysłany email na wskazany adres z tokenem, który należy uzupełnić?", "Historia zamówień", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dr != MessageBoxResult.Yes) return;

            bool historyLock = true;

            try
            {
                Random r = new Random();
                string token = r.Next(99999).ToString().PadLeft(5, '0');

                new Resender().SendToken(email, token);
                MSB.Show("Email wysłany.", "Historia zamówień", MessageBoxButton.OK, MessageBoxImage.Information);

                while (true)
                {
                    InputWindow input = new InputWindow("Wprowadź token");
                    var inputResult = input.ShowDialog();
                    if (inputResult == false)
                    {
                        historyLock = true;
                        break;
                    }
                    if (input.Value == token)
                    {
                        historyLock = false;
                        break;
                    }
                    else
                        MSB.Show("Błędny token. Spróbuj jeszcze raz.", "Historia zamówień", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MSB.Show(ex.Message, "Historia zamówień", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!historyLock)
            {
                OrdersHistory history = new OrdersHistory(email);
                history.Show();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
