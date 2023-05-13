using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Npgsql;

namespace RealmsOfAndora.Database.DatabaseWrappers
{
    internal class PostgresqlWrapper : IVSDatabase
    {
        private readonly string connString = "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer";
        private readonly NpgsqlConnection conn;
        public bool IsConnected { get; set; } = false;
        public string ErrorMessage { get; set; }

        public PostgresqlWrapper(DatabaseConfig databaseConfig)
        {
            conn = new NpgsqlConnection(String.Format(this.connString,
                databaseConfig.Server, databaseConfig.Username, databaseConfig.Database, databaseConfig.Port, databaseConfig.Password));
            conn.Open();
            IsConnected = true;
        }
        public int QueryInsert(string sql, Dictionary<string, string> parameters) => conn.Execute(sql, new DynamicParameters(parameters));
        public bool QueryExists(string sql, Dictionary<string, string> parameters) => QueryInsert(sql, parameters) > 0;
        public List<T> Query<T>(string sql, Dictionary<string, string> parameters) => conn.Query<T>(sql, new DynamicParameters(parameters)).ToList();
    }
}
