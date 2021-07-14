using System;
using System.Collections.Generic;
using System.Text;
using ConcertTourManager.App.Concrete;
using ConcertTourManager.Domain.Entity;
using ConcertTourManager.App.Abstract;
using ConcertTourManager.Domain.Helpers;
using System.Linq;

namespace ConcertTourManager.App.Managers
{
    public class VenueManager
    {
        private VenueService _venueService;
        public VenueManager(VenueService venueService)
        {
            _venueService = venueService;
        }
        
        public void AddVenueView()
        {
            Venue venueToAdd = new Venue();
            Console.WriteLine("Enter information about new venue.");
            Console.WriteLine("NAME: ");
            venueToAdd.Name = Console.ReadLine();

            //Venue type definition
            venueToAdd = SetVenueTypeView(venueToAdd);

            Console.WriteLine("\r\nVENUE OWNER: ");
            venueToAdd.Owner = Console.ReadLine();

            //Addres definition
            venueToAdd = SetAddresView(venueToAdd);

            Console.WriteLine("E-MAIL: ");
            venueToAdd.Email = Console.ReadLine();
            Console.WriteLine("PHONE: ");
            venueToAdd.Phone = Console.ReadLine();
            Console.WriteLine("CAPACITY ");
            venueToAdd.Capacity = Int32.Parse(Console.ReadLine());

            AddVenue(venueToAdd,true);

            Console.WriteLine("Venue is added succesully:");
            ShowVenueFullInformation(venueToAdd);
            Console.ReadKey();
        }
        public void RemoveVenueView()
        {
            Console.WriteLine("Enter venue id you want to remove:\r\n");
            int id = Int32.Parse(Console.ReadLine());
            bool elementIsRemoved = RemoveVenueById(id,true); 

            if (elementIsRemoved)
            {
                Console.WriteLine("\r\nVenue is removed successfully");
            }
            else
            {
                Console.WriteLine("\r\nVenue with entered id doesn't exist!");
            }
        }
        public Venue SetAddresView(Venue venue)
        {
            Console.WriteLine("ADDRESS - Street addres: ");
            venue.Address.StreetAddress = Console.ReadLine();
            Console.WriteLine("ADDRESS - City: ");
            venue.Address.City = Console.ReadLine();
            Console.WriteLine("ADDRESS - Zip code: ");
            venue.Address.ZipCode = Console.ReadLine();
            Console.WriteLine("ADDRESS - Country: ");
            venue.Address.Country = Console.ReadLine();
            return venue;
        }
        public Venue SetVenueTypeView(Venue venue)
        {
            Console.WriteLine("Set venue type [1]Club, [2]Pub, [3] Stadium.");
            ConsoleKeyInfo chosenOption = Console.ReadKey();
            switch (chosenOption.KeyChar)
            {
                case '1':
                    venue.Type = VenueType.Club;
                    break;
                case '2':
                    venue.Type = VenueType.Pub;
                    break;
                case '3':
                    venue.Type = VenueType.Stadium;
                    break;
                default:
                    venue.Type = VenueType.Club;
                    break;
            }
            return venue;
        }
        public void ShowVenuesView()
        {
            if (_venueService.Items.Count == 0)
            {
                Console.WriteLine("Venue data base is empty.");
            }
            else
            {
                Console.WriteLine("Chose sorting mode - [1] by name / [2] by city:");
                ConsoleKeyInfo chosenOption = Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine("Venue list (Name / City / [Id]):");
                switch (chosenOption.KeyChar)
                {
                    case '1':
                        ShowVenuesSortedByName();
                        break;
                    case '2':
                        ShowVenuesSortedByCities();
                        break;
                    default:
                        ShowVenuesSortedByName();
                        break;
                }
            }
        }
        public Venue GetVenueView()
        {
            Console.WriteLine("Enter venue by id:");
            return GetVenueById(Int32.Parse(Console.ReadLine()));
        }
        public Venue GetVenueById(int id)
        {
            return _venueService.GetItemById(id);
        }
        public bool RemoveVenueById(int id, bool serialize)
        {
            var venueToRemove = _venueService.GetItemById(id);
            if(venueToRemove != null)
            {
                _venueService.RemoveItem(venueToRemove);
                if(serialize)
                {
                    _venueService.SerializeVenuesToXmlDB();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddVenue(Venue venue, bool serialize)
        {
            _venueService.AddItem(venue);
            if(serialize)
            {
                _venueService.SerializeVenuesToXmlDB();
            }
        }
        private void ShowVenueFullInformation(Venue venue)
        {
            Console.WriteLine("--------" + venue.Name + "--------");
            Console.WriteLine("--> OWNER: " + venue.Owner);
            Console.WriteLine("--> TYPE: " + venue.Type.ToString());
            Console.WriteLine("--> E-MAIL: " + venue.Email);
            Console.WriteLine("--> PHONE: " + venue.Phone);
            Console.WriteLine("--> ADDRESS: " + GetVenueAddressInformation(venue));
            Console.WriteLine("--> CAPACITY: " + venue.Capacity);
        }
        private string GetVenueAddressInformation(Venue venue)
        {
            return venue.Address.StreetAddress + " / " + venue.Address.ZipCode + " " + venue.Address.City + " / " + venue.Address.Country;
        }

        private string GetVenueShortInformation(Venue venue)
        {
            return venue.Name + " | " + venue.Address.City + " [" + venue.Id + "]";
        }
        private void ShowVenuesSortedByName()
        {
            var venues = _venueService.Items.OrderBy(p => p.Name).ToList();
            PrintVenues(venues);
        }

        private void ShowVenuesSortedByCities()
        {
            var venues = _venueService.Items.OrderBy(p => p.Address.City).ThenBy(p => p.Name).ToList();
            PrintVenues(venues);
        }
        private void PrintVenues(List<Venue> venues)
        {
            foreach (var venue in venues)
            {
                Console.WriteLine(GetVenueShortInformation(venue));
            }
        }
    }
}
