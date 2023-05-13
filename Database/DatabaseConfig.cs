using Vintagestory.API.Common;

namespace RealmsOfAndora.Database
{
    internal class DatabaseConfig
    {
        public string DatabaseType { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public DatabaseConfig() { }

        public static DatabaseConfig Current { get; set; }

        private static DatabaseConfig GetDefault()
        {
            DatabaseConfig databaseConfig = new DatabaseConfig
            {
                DatabaseType = "postgresql",
                Server = "localhost",
                Port = 5432,
                Username = "vintage_story",
                Password = "password123",
                Database = "vintage_story"
            };

            return databaseConfig;
        }

        public static void LoadConfig(ICoreAPI api)
        {
            try
            {
                var Config = api.LoadModConfig<DatabaseConfig>("externaldatabase/dbconfig.json");
                if (Config != null)
                {
                    api.Logger.Notification("Mod Config successfully loaded.");
                    DatabaseConfig.Current = Config;
                }
                else
                {
                    api.Logger.Notification("No Mod Config specified. Falling back to default settings");
                    DatabaseConfig.Current = DatabaseConfig.GetDefault();
                }
            }
            catch
            {
                DatabaseConfig.Current = DatabaseConfig.GetDefault();
                api.Logger.Error("Failed to load custom mod configuration. Falling back to default settings!");
            }
            finally
            {
                api.StoreModConfig(DatabaseConfig.Current, "externaldatabase/dbconfig.json");
            }
        }
    }
}
