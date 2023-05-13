using System;
using RealmsOfAndora.Database.DatabaseWrappers;
using RealmsOfAndora.Database;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using realmsofandora.Database;

namespace realmsofandora
{
    public class ExternalDatabase : ModSystem
    {
        //TODO: This should only be settable within Start Server Side or in a future reconnect function.
        static IVSDatabase Instance;
        public override void StartServerSide(ICoreServerAPI api)
        {
            DatabaseConfig.LoadConfig(api);
            DatabaseConfig databaseConfig = DatabaseConfig.Current;

            if (databaseConfig.DatabaseType.Equals("postgresql"))
            {
                Instance = new PostgresqlWrapper(databaseConfig);
            }
            else
            {
                throw new NotImplementedException("Invalid database type was entered.");
            }
            base.Start(api);
        }
        public override bool ShouldLoad(EnumAppSide side)
        {
            return side == EnumAppSide.Server;
        }
        public static IVSDatabase GetDatabase()
        {
            if (Instance == null)
            {
                throw new DatabaseNotInitializedException();
            }
            return Instance;
        }
    }
}