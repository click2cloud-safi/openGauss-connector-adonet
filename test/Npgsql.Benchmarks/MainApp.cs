using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npgsql.Benchmarks
{
    class MainApp
    {
        public void ConnectionString(string con)
        {
            BenchmarkEnvironment.OpenConnectionAgain(con);
        }

        public int InsertData(string query,string con)
        {
            BenchmarkEnvironment.Insert(query,con);
            return 1;
        }
    }
}
