using System;
using realmsofandora.databasewrappers;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

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
        public override void StartServerSide(ICoreServerAPI api)
        {
            api.Server.Logger.EntryAdded += OnServerLogEntry;
        }

        public override bool ShouldLoad(EnumAppSide side)
        {
            return side == EnumAppSide.Server;
        }

        private void OnServerLogEntry(EnumLogType logType, string message, params object[] args)
        {
            if (logType == EnumLogType.VerboseDebug) return;
            System.Diagnostics.Debug.WriteLine("[Server " + logType + "] " + message, args);
        }
    }
}
