using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaIsland.Data.Model
{
    [Table("OrderItem")]
    public class OrderItem : INotifyPropertyChanged
    {
        [Key]
        [Column("Id", TypeName = "INTEGER")]
        public int Id { get; set; }

        [Column("OrderHeaderId", TypeName = "INTEGER")]
        public int OrderHeaderId { get; set; }

        private EntityRef<OrderHeader> orderHeader = new EntityRef<OrderHeader>();

        [ForeignKey("OrderHeaderId")]
        [Required]
        public OrderHeader OrderHeader
        {
            get { return orderHeader.Entity; }
            set { orderHeader.Entity = value; }
        }

        [Column("ProductId", TypeName = "INTEGER")]
        public int ProductId { get; set; }

        private EntityRef<Product> product = new EntityRef<Product>();

        [ForeignKey("ProductId")]
        [Required]
        public virtual Product Product
        {
            get { return product.Entity; }
            set { product.Entity = value; }
        }

        private int count;
        [Column("Count", TypeName = "INTEGER")]
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                OnPropertyRaised(nameof(Count));
            }
        }

        private decimal price;
        [Column("Price", TypeName = "DECIMAL")]
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                OnPropertyRaised(nameof(Price));
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
