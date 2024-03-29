﻿using System.Data.SqlClient;

namespace WebApplication1.Models.Common
{
    public class DBHelper
    {
        private static string SERVER = "DESKTOP-AEPN1UJ";
        private static string DATABASE = "Copycat";

        private string CONNECTIONSTRING = $"Data Source={SERVER};Initial Catalog={DATABASE};Integrated Security = true";

        private SqlConnection? connection { get; set; }

        public SqlConnection GetConnection()
        {
            if (connection != null)
            {
                return connection;
            }
            else
            {
                return new SqlConnection(CONNECTIONSTRING);
            }
        }

    }
}
