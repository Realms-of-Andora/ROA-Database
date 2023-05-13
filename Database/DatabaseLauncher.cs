using System;
using RealmsOfAndora.Database.DatabaseWrappers;
using Vintagestory.API.Common;

namespace RealmsOfAndora.Database
{
    class DatabaseLauncher : ModSystem
    {
        public IVSDatabase Instance { get; set; }
        public override void Start(ICoreAPI api)
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
    }
}
