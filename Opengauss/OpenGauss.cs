using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
 
namespace Opengauss
{
    public class OpenGauss 
    {
       

        internal static NpgsqlConnection GetConnection(string constring) => new NpgsqlConnection(constring);        

        //Connection method
        public string OpenConnection(string DefaultConnectionString1)
        {
            var conn = GetConnection(DefaultConnectionString1);
            var mesg = "";

            try
            {
                conn = GetConnection(DefaultConnectionString1);
                conn.Open();

            }
            catch (Exception ex)
            {
                mesg = ex.Message;
            }
            return mesg;
        }

        //int returns method
        public string ExecuteQuery(string query, string con)
        {
            using (var conn = new NpgsqlConnection(con))
            {

                var cmd = new NpgsqlCommand(query, conn);
                conn.Open();
                
                var rowAffected = 0;
                var resultMessage = "";
                try
                {                   
                    rowAffected = cmd.ExecuteNonQuery();
                    resultMessage = rowAffected.ToString();

                }
                catch (Exception ex)
                {

                    resultMessage = ex.Message;
                }
                conn.Close();
                return resultMessage;
            }
        } 

        //12-02-2021 Added for SP   
        public string ExecuteSPQuery(string spName, string con)
        {      

            using (var conn = new NpgsqlConnection(con))
            {

                var cmd = new NpgsqlCommand(spName, conn);
                conn.Open();
                var rowAffected = 0;
                var resultMessage = "";
                try
                {
                    rowAffected = cmd.ExecuteNonQuery();
                    resultMessage = rowAffected.ToString();

                }
                catch (Exception ex)
                {

                    resultMessage = ex.Message;
                }
                conn.Close();
                return resultMessage;
            }
        }

        //select the data
        public DataTable ExecuteSelect(string query, string con)
        {
            using (var conn = new NpgsqlConnection(con))
            {

                var dt = new DataTable();
                var cmd = new NpgsqlCommand(query, conn);
                conn.Open();
                try
                {
                    var objAdp = new NpgsqlDataAdapter(cmd);
                    objAdp.Fill(dt);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
                conn.Close();
                return dt;
            }
        } 

        //Adding SP on 02-11-2021

        public DataTable ExecuteSelectSP(string spname, string con)
        {
            using (var conn = new NpgsqlConnection(con))
            {

                var dt = new DataTable();
                var cmd = new NpgsqlCommand(spname, conn);
                conn.Open();
                try
                {
                    
                    var objAdp = new NpgsqlDataAdapter(cmd);                   
                    objAdp.Fill(dt);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
                conn.Close();
                return dt;              
            }
        }

        //Transaction and RollBack
        public string ExecuteTransactionQuery(string query, string query2, string con)
        {
            using (var conn = new NpgsqlConnection(con))
            {
                var cmd = new NpgsqlCommand(query, conn);
                var cmd2 = new NpgsqlCommand(query2, conn);
                conn.Open();
                NpgsqlTransaction transaction;
                transaction = conn.BeginTransaction();
                var rowAffected = 0;
                var rowAffected2 = 0;
                var finalRowAffect = 0;
                var resultMessage = "";
                try
                {
                    rowAffected = cmd.ExecuteNonQuery();
                    rowAffected2 = cmd2.ExecuteNonQuery();
                    finalRowAffect = rowAffected + rowAffected2;
                    resultMessage = finalRowAffect.ToString();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    resultMessage = ex.Message;
                }
                conn.Close();
                //return finalRowAffect;
                return resultMessage;               
            }
        }
    }
}
