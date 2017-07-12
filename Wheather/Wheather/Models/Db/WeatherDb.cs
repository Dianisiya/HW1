namespace Wheather.Models.Db
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class WeatherDb : DbContext
    {
        // Your context has been configured to use a 'WeatherDb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Wheather.Models.Db.WeatherDb' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'WeatherDb' 
        // connection string in the application configuration file.
        public WeatherDb()
            : base("name=WeatherDb")
        {
            Database.SetInitializer<WeatherDb>(new WeatherDbInitilizer());
        }

        public DbSet<Action> Actions { get; set; }

        public DbSet<City> Cities { get; set; }
        
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    public class WeatherDbInitilizer : CreateDatabaseIfNotExists<WeatherDb>
    {
        protected override void Seed(WeatherDb context)
        {
            context.Cities.AddRange(new[]
                                        {
                                            new City { Name = "Kiev" } 
                                            , new City{Name = "Lviv"}
                                            , new City{Name = "Kharkiv" } 
                                            , new City { Name = "Dnipropetrovsk" }
                                            , new City { Name = "Odessa" }
                                        });

            base.Seed(context);
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}