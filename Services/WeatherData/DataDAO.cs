using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApplication.Models;

/*
    @Authors:
        Alex J Zoller
        &logging by Aiden
 */

namespace WeatherApplication
{
    public class DataDAO : IDataDAO
    {
        public string connectionString = "datasource=remotemysql.com;username=hHOQMOQDTD;password=f3HJJN4I3c;database=hHOQMOQDTD;port=3306";

        public List<WeatherData> getAllData()
        {
            Logger.Debug("Entering getAllData@DataDAO");
            List<WeatherData> allData = new List<WeatherData>();
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = con.CreateCommand();
            string sqlStatement = "SELECT * FROM hHOQMOQDTD.currentweather";
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sqlStatement;
            MySqlDataReader rs = command.ExecuteReader();
            if (rs.Read())
            {
                allData.Add( new WeatherData { Location = (string)rs[0], Pressure = (double)rs[1], Tempature = (double)rs[2], Humidity = (double)rs[3], Time = (int)rs[4] });
            }
            con.Close();
            return allData;

        }

        public List<string> getAllLocations()
        {
            Logger.Debug("Entering getAllLocations@DataDAO");
            List<string> locations = new List<string>();
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = con.CreateCommand();
            string sqlStatement = "SELECT Location FROM currentweather";
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sqlStatement;
            MySqlDataReader rs = command.ExecuteReader();
            if (rs.Read())
            {
                string location = (string)rs[0];
                locations.Add(location);
            }
            con.Close();
            return locations;

        }

        public WeatherData getCurrentData()
        {
            Logger.Debug("Entering getCurrentData@DataDAO");
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = con.CreateCommand();
            string sqlStatement = "SELECT * FROM hHOQMOQDTD.currentweather";
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sqlStatement;
            MySqlDataReader rs = command.ExecuteReader();
            if (rs.Read())
            {
                return new WeatherData { Location = (string)rs[0], Pressure = (double)rs[1], Tempature = (double)rs[2], Humidity = (double)rs[3], Time = (int)rs[4] };
            }
            con.Close();
            return new WeatherData();
        }

        public WeatherData getDataByLocation(string location)
        {
            Logger.Debug("Entering getDataByLocations@DataDAO");
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = con.CreateCommand();
            string sqlStatement = "SELECT * FROM hHOQMOQDTD.currentweather WHERE Location = @Location";
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sqlStatement;
            command.Parameters.AddWithValue("@Location", location);
            MySqlDataReader rs = command.ExecuteReader();
            if (rs.Read())
            {
                return new WeatherData { Location = (string)rs[0], Pressure = (double)rs[1], Tempature = (double)rs[2], Humidity = (double)rs[3], Time = (int)rs[4] };
            }
            con.Close();
            return new WeatherData();
        }
    }
}
