using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace realmsofandora.databasewrappers
{
    internal class PostgresqlWrapper : IVSDatabase
    {
        private readonly string connString = "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer";
        private readonly NpgsqlConnection conn;
        public bool HasError { get; set; } = false;
        public string ErrorMessage { get; set; }

        public PostgresqlWrapper(DatabaseConfig databaseConfig)
        {
            this.conn = new NpgsqlConnection(String.Format(this.connString,
                databaseConfig.Server, databaseConfig.Username, databaseConfig.Database, databaseConfig.Port, databaseConfig.Password));
            conn.Open();
        }
        public int QueryInsert(string sql, Dictionary<string,object> parameters)
        {
            int rowsAffected = -1;
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, this.conn))
            {
                cmd.Parameters.Add(new NpgsqlParameter(parameters.Keys.ToString(), DbType.Object));
                cmd.Parameters[0].Value = parameters.Values;
                rowsAffected = cmd.ExecuteNonQuery();
            }
            return rowsAffected;
        }
        public bool QueryExists(string sql, Dictionary<string, object> parameters)
        {
            return false;
        }
    }
}
