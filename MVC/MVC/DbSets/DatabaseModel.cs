namespace MVC.DbSets
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class DatabaseModel : DbContext
    {
        // Your context has been configured to use a 'DatabaseModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MVC.DatabaseModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DatabaseModel' 
        // connection string in the application configuration file.
        public DatabaseModel()
            : base("name=DatabaseModel")
        {
            //создать базу, если ее нету
            Database.SetInitializer(new Initializer());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Book> Books { get; set; }

        public static DatabaseModel Create()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DatabaseModel>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseModel, Migrations.Configuration>());

            var doMigration = false;
            var context = new DatabaseModel();
            try
            {
                doMigration = !context.Database.CompatibleWithModel(true);
            }
            catch (/*NotSupportedException*/Exception)
            {
                //if there are no metadata for migration
                doMigration = true;
            }

            if (doMigration)
            {
                var configuration = new Migrations.Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }

            return context;
        }
    }
}