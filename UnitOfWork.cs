using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mc.data.infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;
        private const string DATABASE_MSSQL = "MSSQL";
        private const string DATABASE_MYSQL = "MYSQL";
        private const string DATABASE_ORACLE = "ORACLE";

        public DbContext Context { get => _dbContext; }
        public virtual string ApplicationContext { get => throw new NotImplementedException(); }
        public virtual string ConnectionName { get => throw new NotImplementedException(); }

        public UnitOfWork(IConfiguration appConfiguration)
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();

            string environment = appConfiguration.GetValue<String>("environment");

            ConfigurationDatabase database = new ConfigurationDatabase();

            appConfiguration.GetSection(environment)
                            .GetSection(this.ApplicationContext)
                            .Bind(this.ConnectionName, database);

            switch (database.type)
            {
                case DATABASE_MYSQL:
                    builder.UseMySQL(database.connectionString, mysql =>
                    {
                        mysql.CommandTimeout(60);
                    });
                    break;

                case DATABASE_MSSQL:
                    builder.UseSqlServer(database.connectionString, mssql =>
                    {
                        mssql.CommandTimeout(60);
                        mssql.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
                    break;

                case DATABASE_ORACLE:
                    builder.UseOracle(database.connectionString, oracle =>
                    {
                        oracle.CommandTimeout(600);
                    });
                    break;
            }

            _dbContext = this.OnCreatingContext(builder.Options).Result;
        }

        public virtual Task<DbContext> OnCreatingContext(DbContextOptions options)
        {
            throw new NotImplementedException();
        }

        async public Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
