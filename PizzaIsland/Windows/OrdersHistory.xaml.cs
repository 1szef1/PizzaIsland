using PizzaIsland.Data.Services;
using System.Windows;

namespace PizzaIsland.Windows
{
    public partial class OrdersHistory : Window
    {
        private string email;
        private OrderService service;

        public OrdersHistory(string email)
        {
            InitializeComponent();
            this.email = email;
            
            this.Title += " - " + email;
            this.Loaded += OrdersHistory_Loaded;
        }
        private void OrdersHistory_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;

            service = new OrderService();
            dgOrders.ItemsSource = service.GetOrdersByEmail(email);
        }
    }
}
