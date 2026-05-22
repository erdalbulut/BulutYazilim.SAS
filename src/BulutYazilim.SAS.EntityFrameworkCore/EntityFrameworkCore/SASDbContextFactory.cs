using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BulutYazilim.SAS.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class SASDbContextFactory : IDesignTimeDbContextFactory<SASDbContext>
{
    public SASDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        SASEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<SASDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new SASDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BulutYazilim.SAS.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
