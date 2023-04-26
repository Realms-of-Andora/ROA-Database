using System.Collections.Generic;

interface IVSDatabase
{
    List<T> Query<T>(string sql, Dictionary<string, object> parameters);
    bool QueryExists(string sql, Dictionary<string, object> parameters);
    int QueryInsert(string sql, Dictionary<string, object> parameters);
}
