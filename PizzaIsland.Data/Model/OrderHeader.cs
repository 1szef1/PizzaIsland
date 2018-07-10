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
    [Table("OrderHeader")]
    public class OrderHeader : INotifyPropertyChanged
    {
        [Key]
        [Column("Id", TypeName = "INTEGER")]
        public int Id { get; set; }

        [Column("Date", TypeName = "DATETIME")]
        [Required]
        public DateTime Date { get; set; }

        [Column("Comments", TypeName = "VARCHAR")]
        public string Comments { get; set; }

        [Column("Email", TypeName = "VARCHAR")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("EmailSent", TypeName = "BIT")]
        public bool EmailSent { get; set; }

        private EntitySet<OrderItem> orderItems = new EntitySet<OrderItem>();

        [ForeignKey("OrderHeaderId")]
        public virtual ICollection<OrderItem> OrderItems
        {
            get { return orderItems; }
            set { orderItems.Assign(value); }
        }

        [NotMapped]
        public decimal OrderValue =>  OrderItems.Sum(x => x.Price * x.Count);

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
