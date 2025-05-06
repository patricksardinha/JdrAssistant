using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace JdrAssistant
{
    public static class Database
    {
        private const string DbPath = "data_jdr_assistant.db";

        public static void Init()
        {
            using var connection = new SqliteConnection($"Data Source={DbPath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Campaigns (
                Id TEXT PRIMARY KEY,
                Name TEXT
            );
        ";
            cmd.ExecuteNonQuery();
        }

        public static void AddCampaign(string id, string name)
        {
            using var connection = new SqliteConnection($"Data Source={DbPath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Campaigns (Id, Name) VALUES ($id, $name)";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.Parameters.AddWithValue("$name", name);
            cmd.ExecuteNonQuery();
        }

        public static List<Campaign> GetCampaigns()
        {
            var list = new List<Campaign>();

            using var connection = new SqliteConnection($"Data Source={DbPath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM Campaigns";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Campaign
                {
                    Id = reader.GetString(0),
                    Name = reader.GetString(1)
                });
            }

            return list;
        }
    }
}
