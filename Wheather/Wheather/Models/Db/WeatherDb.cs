namespace Wheather.Models.Db
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class WeatherDb : DbContext
    {
        public WeatherDb()
            : base("name=WeatherDb")
        {
            Database.SetInitializer<WeatherDb>(new WeatherDbInitilizer());
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Action> Actions { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Weather> Weathers { get; set; }
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
}