using SQLite.CodeFirst;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace MailSender.Model
{
    internal class MSContext : DbContext
    {
        public static string DbName => "mail_conv.db";

        public static string ConnectionString
        {
            get
            {
                return new SQLiteConnectionStringBuilder()
                {
                    DataSource = DbName,
                }.ConnectionString;
            }
        }

        public MSContext() :
            base(new SQLiteConnection(ConnectionString), true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteDropCreateDatabaseWhenModelChanges<MSContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SenderInfo> SenderInfos { get; set; }
    }
}
