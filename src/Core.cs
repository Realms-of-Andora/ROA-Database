using System;
using realmsofandora.databasewrappers;
using Vintagestory.API.Common;

namespace realmsofandora
{
    class ExternalDatabase : ModSystem
    {
        public IVSDatabase Instance;
        public override void Start(ICoreAPI api)
        {
            DatabaseConfig.LoadConfig(api);
            DatabaseConfig databaseConfig = DatabaseConfig.Current;

            if (databaseConfig.DatabaseType.Equals("postgresql"))
            {
                this.Instance = new PostgresqlWrapper(databaseConfig);
            } else
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
