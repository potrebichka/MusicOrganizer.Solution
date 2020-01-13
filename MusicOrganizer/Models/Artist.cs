using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace MusicOrganizer.Models
{
    public class Artist
    {
        public string Name {get;set;}
        public int Id {get; set; }
        
        private List<Record> records= new List<Record>{};
        public Artist (string name)
        {
            Name = name;
        }

        public Artist (string name, int id)
        {
            Name = name;
            Id = id;
        }

         public override bool Equals(System.Object otherArtist)
        {
            if (!(otherArtist is Artist))
            {
                return false;
            }
            else
            {
                Artist newArtist = (Artist) otherArtist;
                bool idEquility = (this.Id == newArtist.Id);
                bool nameEquility = (this.Name == newArtist.Name);
                return (nameEquility && idEquility);
            }
        }

        public static List<Artist> GetAll()
        {
            List<Artist> allArtists = new List<Artist>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM artists;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int artistId = rdr.GetInt32(0);
                string artistName = rdr.GetString(1);
                Artist newArtist = new Artist(artistName, artistId);
                allArtists.Add(newArtist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allArtists;
        }
       

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM artists;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {   
                conn.Dispose();
            }
        }

        public static Artist Find(int id)
        {
           MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM `artists` WHERE idartists= @thisID;";

             MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr =cmd.ExecuteReader() as MySqlDataReader;
            int artistId = 0;
            string artistName = "";
            while (rdr.Read())
            {
                artistId = rdr.GetInt32(0);
                artistName = rdr.GetString(1);
            }
            Artist foundArtist = new Artist (artistName, artistId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundArtist;
        }

        public void AddRecord(Record record)
        {
            records.Add(record);
        }
        public List<Record> GetListOfRecords()
        {
            return records;
        }
        public static int FindArtist(string name)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM `artists` WHERE name= @thisname;";

            MySqlParameter thisName = new MySqlParameter();
            thisName.ParameterName = "@thisName";
            thisName.Value = name;
            cmd.Parameters.Add(thisName);

             var rdr = cmd.ExecuteReader() as MySqlDataReader;
             int artistId = 0;
             string artistName = "";
             while (rdr.Read())
             {
                 artistId = rdr.GetInt32(0);
                 artistName = rdr.GetString(1);
             }   
            Artist foundArtist= new Artist( artistName, artistId );

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundArtist.Id;
        }



      
        public static Artist GetArtist(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM artists WHERE idartists = @thisId;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int artistId = 0;
            string artistName = "";
            while (rdr.Read())
            {
                artistId = rdr.GetInt32(0);
                artistName = rdr.GetString(1);
            }
            Artist foundArtist = new Artist(artistName, artistId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundArtist;

        }
        public static List <Artist> GetArtistPartial(string searchName)
        {
            List<Artist> listOfMatchedArtists = new List <Artist> {};

            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * from `artists` WHERE name LIKE @thisName;";
            MySqlParameter thisName = new MySqlParameter();
            thisName.ParameterName = "@thisName";
            thisName.Value = "%" + searchName + "%";
            cmd.Parameters.Add(thisName);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int artistId = 0;
            string artistName = "";
            while (rdr.Read())
            {
                artistId = rdr.GetInt32(0);
                artistName = rdr.GetString(1);
                Artist newArtist= new Artist(artistName, artistId);
                listOfMatchedArtists.Add(newArtist);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        //     // Regex rx = new Regex(@"" + searchName + @"");
        //     // foreach (Artist artist in _instances)
        //     // {
        //     //     if (rx.Match(artist.Name).Success)
        //     //     {
        //     //         listOfMatchedArtists.Add(artist);
        //     //     }
        //     // }
            return listOfMatchedArtists;
        }
        public void Save()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;

          cmd.CommandText = @"INSERT INTO artists (name) VALUES (@ArtistName);";
          MySqlParameter name = new MySqlParameter();
          name.ParameterName = "@ArtistName";
          name.Value = this.Name;
          cmd.Parameters.Add(name);
          cmd.ExecuteNonQuery();
          Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}