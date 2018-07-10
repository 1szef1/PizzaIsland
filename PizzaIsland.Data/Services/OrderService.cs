using PizzaIsland.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PizzaIsland.Data.Services
{
    public class OrderService : IDisposable
    {
        private PIContext context;

        public OrderService()
        {
            context = new PIContext();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IList<Product> GetProductsByType(ProductEnum type)
        {
            /*return context.Products
                .Include(x => x.Type)
                .Where(x => x.Type.TypeNr == (int)type)
                .ToList();*/
            return context.Products
                .Where(x => x.Type.TypeNr == (int)type)
                .ToList();
        }

        public void AddOrder(OrderHeader order)
        {
            context.OrderHeaders.Add(order);
            context.SaveChanges();
        }

        public IList<OrderHeader> GetOrdersByEmail(string email)
        {
            return context.OrderHeaders.Where(x => x.Email == email).ToList();
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
