using System.Collections.Generic;

namespace RealmsOfAndora.Database
{
    public interface IVSDatabase
    {
        bool IsConnected { get; }
        List<T> Query<T>(string sql, Dictionary<string, string> parameters);
        bool QueryExists(string sql, Dictionary<string, string> parameters);
        int QueryInsert(string sql, Dictionary<string, string> parameters);
    }
}