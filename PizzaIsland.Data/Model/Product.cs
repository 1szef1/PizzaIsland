using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaIsland.Data.Model
{
    //[System.Data.Linq.Mapping.Table(Name = "Component")]
    [System.ComponentModel.DataAnnotations.Schema.Table("Product")]
    public class Product
    {
        //[Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id", TypeName = "INTEGER")]
        public int Id { get; set; }

        //[Column(Name = "Name", DbType = "VARCHAR", CanBeNull = false)]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        //[Column(Name = "TypeId", DbType = "INTEGER NOT NULL", CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.Schema.Column("TypeId", TypeName = "INTEGER")]
        public int TypeId { get; set; }

        private EntityRef<ProductType> type = new EntityRef<ProductType>();

        [ForeignKey("TypeId")]
        [Required]
        //[System.Data.Linq.Mapping.Association(Name = "FK_Product_ProductType", IsForeignKey = true, Storage = "type", ThisKey = "TypeId")]
        public virtual ProductType Type
        {
            get { return type.Entity; }
            set { type.Entity = value; }
        }

        public decimal? Price { get; set; }

        public string DisplayName
        {
            get
            {
                return (Price != null ? $"{Name} ({Price.Value.ToString("c")})" : Name);
            }
        }

        public string TypeName
        {
            get
            {
                return Type?.DisplayName;
            }
        }

        public ProductEnum TypeNr
        {
            get
            {
                return (ProductEnum)(Type?.TypeNr ?? 0);
            }
        }
    }
}
