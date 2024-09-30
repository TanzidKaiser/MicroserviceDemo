﻿using MongoRepo.Context;

namespace Catalog.API.Context
{
    public class CatalogDbContext : ApplicationDbContext
    {

        static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
        static string connectionString = configuration.GetConnectionString("Calatog.Api");
        static string databaseName = configuration.GetValue<string>("DatabaseName");

        public CatalogDbContext() : base(connectionString, databaseName)
        {
        }
    }
}
