using PizzaIsland.Data.Model;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaIsland.Data
{
    public class PIContext : DbContext
    {
        public static string DbName => "data.db";

        public static string ConnectionString
        {
            get
            {
                return new SQLiteConnectionStringBuilder()
                {
                    DataSource = DbName,
                    ForeignKeys = true,
                }.ConnectionString;
            }
        }

        public PIContext() :
            base(new SQLiteConnection(ConnectionString), true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<PIContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            /*modelBuilder.Entity<Product>()
                .HasRequired<ProductType>(x => x.Type)
                .WithMany(x => x.Products)
                .HasForeignKey<int>(x => x.TypeId);*/
        }

        public virtual DbSet<OrderItem> Orders { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
    }
}
