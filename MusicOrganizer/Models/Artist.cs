using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicOrganizer.Models
{
    public class Artist
    {
        public string Name {get;set;}
        public int Id {get;}
        
        private static List<Artist> _instances = new List<Artist>{};
        private List<Record> records= new List<Record>{};
        public Artist (string name)
        {
            Name = name;
            _instances.Add(this);
            Id = _instances.Count;
        }

        public static List<Artist> GetAll()
        {
            return _instances;
        }

        public static void ClearAll()
        {
            _instances.Clear();
        }

        public static Artist Find(int searchId)
        {
            return _instances[searchId-1];
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
            foreach (Artist artist in _instances)
            {
                if (artist.Name == name)
                {
                    return artist.Id;
                }
            }
            return -1;
        }
        public static Artist GetArtist(int id)
        {
            return _instances[id-1];
        }
        public static List <Artist> GetArtistPartial(string searchName)
        {
            List<Artist> listOfMatchedArtists = new List <Artist> {};
            Regex rx = new Regex(@"" + searchName + @"");
            foreach (Artist artist in _instances)
            {
                if (rx.Match(artist.Name).Success)
                {
                    listOfMatchedArtists.Add(artist);
                }
            }
            return listOfMatchedArtists;
            
        }
    }
}