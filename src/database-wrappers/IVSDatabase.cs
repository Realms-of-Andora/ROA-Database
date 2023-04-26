using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realmsofandora.databasewrappers
{
    internal interface IVSDatabase
    {
        bool QueryExists(string sql, Dictionary<string, object> parameters);
        int QueryInsert(string sql, Dictionary<string, object> parameters);
    }
}
