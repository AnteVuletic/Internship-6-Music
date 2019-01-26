using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Music.Tables;

namespace Music
{
    public class Entry
    {
        public static void Main(string[] args)
        {
            const string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Music;Integrated Security=true;MultipleActiveResultSets=true;";
            using (var connection = new SqlConnection(connectionString))
            {
                var Albums = connection.Query<Album>("SELECT * FROM Album").ToList();
                var Songs = connection.Query<Song>("SELECT * FROM Song").ToList();
                var SongsOnAlbum = connection.Query<Album_Song>("SELECT * FROM Album_Song").ToList();
                var Artists = connection.Query<Artist>("SELECT * FROM Artist").ToList();
                var allArtistsOrderedByName = from artist in Artists orderby artist.Name descending select artist;
                foreach (var artist in allArtistsOrderedByName)
                {
                    Console.WriteLine($"ID: {artist.ID_Artist} Name:{artist.Name} Nationality: {artist.Nationality}");
                }
                Console.WriteLine();
                Console.WriteLine("Pick nationality: ");
                //var nationality = Console.ReadLine();
                const string nationality = "American";
                var allArtistsOfNationality =
                    from artist in Artists where artist.Nationality == nationality select artist;
                foreach (var artist in allArtistsOfNationality)
                {
                    Console.WriteLine($"ID: {artist.ID_Artist} Name:{artist.Name} Nationality: {artist.Nationality}");
                }
                Console.WriteLine();
                var albumArtists = from alb in Albums
                    join art in Artists on alb.FK_Artist equals art.ID_Artist
                    select new {alb.ID_Album, alb.Name, alb.YearOfRelease.Year, ArtistName = art.Name};
                var albumArtistsGroupedByYear =
                    from albArt in albumArtists orderby albArt.Year group albArt by albArt.Year;
                foreach (var byYear in albumArtistsGroupedByYear)
                {
                    Console.WriteLine($"Year of release {byYear.Key}:");
                    foreach (var album in byYear)
                    {
                        Console.WriteLine($"\t{album.Name} by {album.ArtistName}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Please enter a part of name of album:");
                //var partOfName = Console.ReadLine();
                const string partOfName = "Man";
                var albumsWithPartOfName = from alb in Albums where alb.Name.Contains(partOfName) select alb;
                foreach (var album in albumsWithPartOfName)
                {
                    Console.WriteLine($"ID:{album.ID_Album} | Name:{album.Name} | Year:{album.YearOfRelease.Year}");
                }

                Console.WriteLine();

                var songsOnAlbum = from alb in Albums
                    join albSng in SongsOnAlbum on alb.ID_Album equals albSng.FK_Album
                    join sng in Songs on albSng.FK_Song equals sng.ID_Song into allSongsOnAlbums
                    group allSongsOnAlbums by alb 
                    into allSongsOnAlbumByAlbum 
                    select allSongsOnAlbumByAlbum;
                foreach (var byAlbum in songsOnAlbum)
                {
                    Console.WriteLine($"Album:{byAlbum.Key.Name} Duration of all songs: {byAlbum.Sum(songs => songs.Sum(song => song.Duration.Minutes + song.Duration.Seconds/60))} minutes");
                }

                Console.WriteLine();

                Console.WriteLine("Enter name of song to show which albums have the song: ");

                const string nameOfSong = "The Great Gig in the Sky";
                //nameOfSong = Console.ReadLine();
                var songsWithSongName = from alb in Albums
                    join albSng in SongsOnAlbum on alb.ID_Album equals albSng.FK_Album
                    join sng in Songs on albSng.FK_Song equals sng.ID_Song
                    where sng.Name.Contains(nameOfSong) group sng by alb;
                foreach (var albumWithSong in songsWithSongName)
                {
                    Console.WriteLine(
                        $"-> Album:{albumWithSong.Key.Name} | matches entry ( \"{nameOfSong}\") with songs:");
                    foreach (var song in albumWithSong)
                    {
                        Console.WriteLine($"\t Song name: {song.Name}");
                    }
                }

                Console.WriteLine();

                Console.WriteLine("Enter artist name: ");
                //var artistName = Console.ReadLine();
                const string artistName = "Kid Cudi";
                Console.WriteLine("Please enter year:");
                //var yearOfAlbum = int.Parse(Console.ReadLine());
                const int yearOfAlbum = 2003;

                var allSongsWithArtistAboveYear = from sng in Songs
                    join albSng in SongsOnAlbum on sng.ID_Song equals albSng.FK_Song
                    join alb in Albums on albSng.FK_Album equals alb.ID_Album
                    where alb.YearOfRelease.Year > yearOfAlbum
                    join art in Artists on alb.FK_Artist equals art.ID_Artist
                    where art.Name.Contains(artistName)
                    select sng;
                Console.WriteLine("Songs that match criteria entered are: ");
                foreach (var song in allSongsWithArtistAboveYear)
                {
                    Console.WriteLine($"Song name: {song.Name}");
                }

            }
        }
    }
}

