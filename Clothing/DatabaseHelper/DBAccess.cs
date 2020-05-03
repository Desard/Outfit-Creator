using Clothing.DatabaseHelper;
using Clothing.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothing
{
    public class DBAccess
    {
        public static ObservableCollection<Outfit> LoadOutfits()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DBOutfit>("select * from Outfit", new DynamicParameters());
                ObservableCollection<Outfit> outfits = new ObservableCollection<Outfit>();

                foreach (DBOutfit dbOutfit in output.ToList())
                {
                    outfits.Add(dbOutfit.ConvertToOutfit());
                }

                return outfits;
            }
        }

        public static int SaveOutfit(Outfit outfit)
        {
            Int64 rowID = 0;

            DBOutfit dbOutfit = new DBOutfit(outfit);

            using (SQLiteConnection connection = new SQLiteConnection())
            {
                connection.ConnectionString = LoadConnectionString();
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                        "insert into Outfit(TitleName, TitleImage, Chest, Pants, Necklace, Earrings, Rings, Shoes, Head, Wrist) values(@TitleName, @TitleImage, @Chest, @Pants, @Necklace, @Earrings, @Rings, @Shoes, @Head, @Wrist)";
                    command.Parameters.Add("titleName", DbType.String).Value = dbOutfit.TitleName;
                    command.Parameters.Add("titleImage", DbType.Binary).Value = dbOutfit.TitleImage;
                    command.Parameters.Add("head", DbType.Binary).Value = dbOutfit.Head;
                    command.Parameters.Add("chest", DbType.Binary).Value = dbOutfit.Chest;
                    command.Parameters.Add("pants", DbType.Binary).Value = dbOutfit.Pants;
                    command.Parameters.Add("necklace", DbType.Binary).Value = dbOutfit.Necklace;
                    command.Parameters.Add("earrings", DbType.Binary).Value = dbOutfit.Earrings;
                    command.Parameters.Add("rings", DbType.Binary).Value = dbOutfit.Rings;
                    command.Parameters.Add("shoes", DbType.Binary).Value = dbOutfit.Shoes;
                    command.Parameters.Add("wrist", DbType.Binary).Value = dbOutfit.Wrist;
                    command.ExecuteNonQuery();

                    command.CommandText = "select last_insert_rowid()";
                    rowID = (Int64)command.ExecuteScalar();
                }
                connection.Close();
            }

            return (int)rowID;
        }

        public static void DeleteOutfit(Outfit outfit)
        {

            //DBOutfit dbOutfit = new DBOutfit(outfit);

            //using (SQLiteConnection connection = new SQLiteConnection())
            //{
            //    connection.ConnectionString = LoadConnectionString();
            //    connection.Open();
            //    using (SQLiteCommand command = new SQLiteCommand(connection))
            //    {
            //        command.CommandText = "delete from Outfit where Id=:id";
            //        command.ExecuteNonQuery();
            //    }
            //    connection.Close();
            //}
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from Outfit where Id=" + outfit.Id);
            }
        }

        public static void UpdateOutfit(Outfit outfit)
        {
            DBOutfit dbOutfit = new DBOutfit(outfit);

            using (SQLiteConnection connection = new SQLiteConnection())
            {
                connection.ConnectionString = LoadConnectionString();
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                        "update Outfit set TitleName = :titleName, TitleImage = :titleImage, Head = :head, Chest = :chest, Pants = :pants, Necklace = :necklace, Earrings = :earrings, Rings = :rings, Earrings = :earrings, Shoes = :shoes, Earrings = :earrings, Wrist = :wrist where Id=:id";
                    command.Parameters.Add("titleName", DbType.String).Value = dbOutfit.TitleName;
                    command.Parameters.Add("titleImage", DbType.Binary).Value = dbOutfit.TitleImage;
                    command.Parameters.Add("head", DbType.Binary).Value = dbOutfit.Head;
                    command.Parameters.Add("chest", DbType.Binary).Value = dbOutfit.Chest;
                    command.Parameters.Add("pants", DbType.Binary).Value = dbOutfit.Pants;
                    command.Parameters.Add("necklace", DbType.Binary).Value = dbOutfit.Necklace;
                    command.Parameters.Add("earrings", DbType.Binary).Value = dbOutfit.Earrings;
                    command.Parameters.Add("rings", DbType.Binary).Value = dbOutfit.Rings;
                    command.Parameters.Add("shoes", DbType.Binary).Value = dbOutfit.Shoes;
                    command.Parameters.Add("wrist", DbType.Binary).Value = dbOutfit.Wrist;
                    command.Parameters.Add("id", DbType.Int32).Value = dbOutfit.Id;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
