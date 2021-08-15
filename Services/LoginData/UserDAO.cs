using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApplication.Models;

namespace WeatherApplication.Services.LoginData
{
    public class UserDAO : IUserDAO
    {
        public string connectionString = "datasource=remotemysql.com;username=hHOQMOQDTD;password=f3HJJN4I3c;database=hHOQMOQDTD;port=3306";
        public bool login(string username, string password)
        {
            Logger.Debug("Entering login@UserDAO username = " + username + ", password = " + password);
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = con.CreateCommand();
            string sqlStatement = "SELECT * FROM hHOQMOQDTD.User WHERE Username = @Username AND Password = @Password";
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sqlStatement;
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            MySqlDataReader rs = command.ExecuteReader();

            bool found = rs.HasRows;
            System.Diagnostics.Debug.WriteLine(found);
            con.Close();
            return found;

        }

        public bool registerNewUser(string Email, string username, string password)
        {
            Logger.Debug("Entering registerNewUser@UserDAO");
            bool success = false;
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            string sqlStatement = "INSERT INTO hHOQMOQDTD.User (Email, Username, Password) SELECT ?Email, ?Username, ?Password FROM DUAL WHERE NOT EXISTS (SELECT * FROM User WHERE Email= ?Email OR Username = ?Username LIMIT 1)";
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sqlStatement;
            command.Parameters.Add("?Email", MySqlDbType.Text).Value = Email;
            command.Parameters.Add("?Username", MySqlDbType.Text).Value = username;
            command.Parameters.Add("?Password", MySqlDbType.Text).Value = password;
            success = (command.ExecuteNonQuery() == 1);
            con.Close();
            return success;
        }
    }
}
