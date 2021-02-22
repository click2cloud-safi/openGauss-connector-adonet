using System;

namespace Npgsql.Benchmarks
{

    class BenchmarkEnvironment
    {
        static string DefaultConnectionString1 = "Server=192.168.1.48;User ID=opengauss;Password=openg23;Database=opengauss";
        static string DefaultConnection = DefaultConnectionString1;

        internal static string ConnectionString => Environment.GetEnvironmentVariable("opengauss") ?? DefaultConnection;
        //internal static string ConnectionString =>DefaultConnectionString1;
        /// <summary>
        /// Unless the NPGSQL_TEST_DB environment variable is defined, this is used as the connection string for the
        /// test database.
        /// </summary>
        //const string DefaultConnectionString = "Server=192.168.1.48;User ID=opengauss;Password=opengauss#123;Database=opengauss";
        //const string DefaultConnectionString = connection;

        internal static NpgsqlConnection GetConnection() => new NpgsqlConnection(ConnectionString);

        //new
        internal static NpgsqlConnection GetConnectionAgain(string constring) => new NpgsqlConnection(constring);

        internal static NpgsqlConnection OpenConnection()
        {
            var conn = GetConnection();
            conn.Open();
            return conn;
        }

        //new
        internal static NpgsqlConnection OpenConnectionAgain(string DefaultConnectionString1)
        {
            var conn = GetConnectionAgain(DefaultConnectionString1);
            conn.Open();
            return conn;
        }

        public static int Insert(string query, string con)
        {
            var conn = new NpgsqlConnection(con);
            var cmd = new NpgsqlCommand(query, conn);
            var rowAffected = cmd.ExecuteNonQuery();
            return rowAffected;
        }


    }
}
