using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaIsland.Data.Model
{
    [Table("ProductType")]
    public class ProductType
    {
        [Key]
        [Column("Id", TypeName = "INTEGER")]
        public int Id { get; set; }

        [Column("TypeNr", TypeName = "INTEGER")]
        public int TypeNr { get; set; }

        [Column("Name", TypeName = "VARCHAR")]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Column("Price", TypeName = "DECIMAL")]
        public decimal? Price { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public string DisplayName
        {
            get
            {
                return (Price != null ? $"{Name} ({Price.Value.ToString("c")})" : Name);
            }
        }
    }
}
