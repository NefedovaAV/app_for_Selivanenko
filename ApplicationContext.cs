using System;
using System.Collections.Generic;
using System.Data.Entity;

using MySql.Data.MySqlClient;

namespace wfa_Selivanenko
{
   class ApplicationContext : DbContext
    {
       
        private static string connectString = "Data Source=C:\\Users\\Anastasia\\/*OneDrive\\Документы\\Dump20240524*/;server=localhost;user=root;password=qquCNWi8jMfK);database=vniigaz;";

        MySqlConnection connection = new MySqlConnection(connectString);
        public void openConnection() {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeConnection()
        {
            if (connection.State==System.Data.ConnectionState.Open)
                connection.Close();
        }
        
        public MySqlConnection getConnection()
        {
            return connection;
        }
       
    }
}
