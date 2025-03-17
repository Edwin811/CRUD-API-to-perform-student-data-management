using Npgsql;
using System;
using System.Data;

namespace percobaan2.Helpers
{
    public class SqlDBHelper
    {
        private NpgsqlConnection connection;
        private string __constr;

        public SqlDBHelper(string pConstr)
        {
            __constr = pConstr;
            connection = new NpgsqlConnection();
            connection.ConnectionString = __constr;
        }

        public NpgsqlCommand GetNpgsqlCommand(string query)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public void closeConnection()
        {
            connection.Close();
        }
    }
}