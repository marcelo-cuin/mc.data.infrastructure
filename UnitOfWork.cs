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
        public virtual string ConnectionStringName { get => throw new NotImplementedException(); }

        public UnitOfWork(IConfiguration configuration)
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();

            string environment = configuration.GetValue<String>("environment");

            Configuration appConfig = new Configuration();
            configuration.GetSection(environment).GetSection(this.ConnectionStringName).Bind(appConfig);

            string database = appConfig.Database.type;
            string connectionString = appConfig.Database.connectionString;

            switch (database)
            {
                case DATABASE_MYSQL:
                    builder.UseMySql(connectionString, mysql =>
                    {
                        mysql.CommandTimeout(60);
                    });
                    break;

                case DATABASE_MSSQL:
                    builder.UseSqlServer(connectionString, mssql =>
                    {
                        mssql.CommandTimeout(60);
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
