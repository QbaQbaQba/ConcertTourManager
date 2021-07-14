using System;
using System.Collections.Generic;
using System.Text;
using ConcertTourManager.App.Concrete;
using ConcertTourManager.Domain.Entity;
using ConcertTourManager.App.Managers;
using ConcertTourManager.App.Abstract;
using System.IO;

namespace ConcertTourManager.App.Managers
{
    public class TourManager
    {
        private TourService _tourService;
        private ArtistManager _artistManager;
        public TourManager(TourService tourService, ArtistManager artistManager)
        {
            _tourService = tourService;
            _artistManager = artistManager;
        }
        public void CreateTourView()
        {
            Console.WriteLine("Enter tour theme:");
            string newTourTheme = Console.ReadLine();
            Console.WriteLine("Enter artist (headliner):");
            var artist = _artistManager.GetArtistByName(Console.ReadLine());
            if (artist!=null)
            {
                AddTour(newTourTheme, artist);
                Console.WriteLine("Tour " + newTourTheme + " is created successfully");
            }
            else
            {
                Console.WriteLine("Entered artist doesn't exist in data base.");
            }
        }
        public void RemoveTourView()
        {
            Console.WriteLine("Enter id number of tour you want to remove:");
            int id = Int32.Parse(Console.ReadLine());
            bool tourIsRemoved = RemoveTourById(id);
            if (tourIsRemoved)
            {
                Console.WriteLine("Tour is removed succesfully");
            }
            else
            {
                Console.WriteLine("Tour with entered id doesn't exist!");
            }
        }
        public void AddTour(string theme, Artist artist)
        {
            var tour = new Tour() 
            { 
                Theme = theme, 
                Headliner = artist
            };
            _tourService.AddItem(tour);
            tour.SerializeToJson(GetTourConfigFilePath(tour));
        }
        public bool RemoveTourById(int id)
        {
            var tourToRemove = _tourService.GetItemById(id);
            if (tourToRemove != null)
            {
                File.Delete(GetTourConfigFilePath(tourToRemove));
                _tourService.RemoveItem(tourToRemove);
                return true;
            }
            else
            {
                return false;
            } 
        }
        public Tour DefineTourView()
        {
            Console.WriteLine("Enter tour id you want to manage:");
            var id = Int32.Parse(Console.ReadLine());
            return _tourService.GetItemById(id);
        }
        public Tour GetTourById(int id)
        {
            return _tourService.GetItemById(id);
        }

        public void ShowTours()
        {
            if (_tourService.Items.Count==0)
            {
                Console.WriteLine("Tour data base is empty.");
            }
            else
            {
                Console.WriteLine("Managed tours:");
                foreach(var tour in _tourService.Items)
                {
                    Console.WriteLine("[" + tour.Id + "] " + tour.Theme);
                }
            }
        }
        private string GetTourConfigFilePath(Tour tour)
        {
            return _tourService.ConfigFilesFolderPath + @"\" + tour.Theme + "_TourSetting.json";
        }
    }
}
