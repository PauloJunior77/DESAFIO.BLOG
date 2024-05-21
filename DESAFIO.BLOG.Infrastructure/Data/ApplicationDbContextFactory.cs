using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IO;
using DESAFIO.BLOG.Domain.Repositories;
using DESAFIO.BLOG.Infrastructure.Data;
using DESAFIO.BLOG.Infrastructure.Repositories;
using System.Security.Claims;

namespace DESAFIO.BLOG.Infrastructure.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var appSettingsPath = Path.Combine(currentDirectory, "appsettings.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(appSettingsPath)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
