using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ConcertTourManager.App.Concrete;
using ConcertTourManager.Domain.Entity;
using ConcertTourManager.App.Abstract;
using System.Xml.Serialization;
using System.IO;

namespace ConcertTourManager.App.Managers
{
    public class ArtistManager
    {
        private ArtistService _artistService;

        public ArtistManager(ArtistService artistService)
        {
            _artistService = artistService;
        }
        public void AddArtistView()
        {
            Console.WriteLine("\r\nEnter artist name:");

            string newArtistName = Console.ReadLine();

            if(ArtistNameExistsInBase(newArtistName))
            {
                Console.WriteLine("\r\nArtist with entered name already exists!");
                Console.ReadKey();
            }
            else
            {
                AddArtistByName(newArtistName, true);
                Console.WriteLine("\r\nArtist {0} is added to base succesfully.", newArtistName);
                Console.ReadKey();
            }
        }
        public void RemoveArtistView()
        {
            Console.WriteLine("\r\nEnter artist name you want to remove:");

            string artistToRemove = Console.ReadLine();
            if (ArtistNameExistsInBase(artistToRemove))
            {
                RemoveArtistByName(artistToRemove, true);
                
                Console.WriteLine("\r\nArtist {0} is removed form data base succesfully.", artistToRemove);
            }
            else
            {
                Console.WriteLine("\r\nArtist doesn't exist!");
            }
        }
        public void ShowArtistsView()
        {
            if (_artistService.Items.Count == 0)
            {
                Console.WriteLine("Artist data base is empty.");
            }
            else
            {
                Console.WriteLine("Artists list (Name / [Id]):");
                var artists = GetArtistsSortedByName();
                foreach (var artist in artists)
                {
                    Console.WriteLine(artist.Name + "[" + artist.Id + "] ");
                }
            }
        }
        public Artist GetArtistByName(string name)
        {
            if(ArtistNameExistsInBase(name))
            {
                var artist = _artistService.Items.FirstOrDefault(p => p.Name == name);
                return artist;
            }
            else
            {
                return null;
            }
        }
        public void AddArtistByName(string name, bool serialize)
        {
            _artistService.AddItem(new Artist(name));
            if (serialize)
            { 
                _artistService.SerializeArtistsToXmlDB();
            }
        }
        public void RemoveArtistByName(string name, bool serialize)
        {
            var artist = _artistService.Items.FirstOrDefault(p => p.Name == name);
            if (artist!=null)
            {
                _artistService.RemoveItem(artist);
                if(serialize)
                {
                    _artistService.SerializeArtistsToXmlDB();
                }
            }
        }

        private bool ArtistNameExistsInBase(string name)
        {
            if(_artistService.Items.Count==0)
            {
                return false;
            }
            else
            {
                foreach(var artist in _artistService.Items)
                {
                    if(artist.Name==name)
                    {
                        return true;
                    }
                }
            }
            return false;
        } 

        public List<Artist> GetArtistsSortedByName()
        {
            var sortedArtists = _artistService.Items.OrderBy(p => p.Name).ToList();
            return sortedArtists;
        }
    }
}
