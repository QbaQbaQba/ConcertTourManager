using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ConcertTourManager.App.Concrete;
using ConcertTourManager.Domain.Entity;
using ConcertTourManager.App.Abstract;

namespace ConcertTourManager.App.Managers
{
    public class ConcertManager
    {
        private Tour _tour;
        private ConcertService _concertService;
        private VenueManager _venueManager;
        private ArtistManager _artistManager;

        public ConcertManager(Tour tour, VenueManager venueManager, ArtistManager artistManager)
        {
            _tour = tour;
            _venueManager = venueManager;
            _concertService = new ConcertService(tour);
            _artistManager = artistManager;
        }

        public void AddConcertView()
        {
            Console.WriteLine("Enter concert title:");
            string concertTitle = ConcertTitleCreator(Console.ReadLine());
            var concertVenue = _venueManager.GetVenueView();
            var concertDate = GetDateTimeView();
            Console.WriteLine("Enter ticket cost:");
            decimal concertTicketCost = Decimal.Parse(Console.ReadLine());
            var concert = new Concert(concertTitle, _tour.Headliner, concertVenue, concertDate, concertTicketCost);
            AddConcert(concert);
            UpdateTourDates();
            Console.WriteLine("New date is added to tour concert list");
        }

        public void RemoveConcertView()
        {
            Console.WriteLine("Enter date of concert you want to remove:");
            var concertToRemove = GetConcertByDate(Console.ReadLine());
            if (concertToRemove != null)
            {
                RemoveConcert(concertToRemove);
                UpdateTourDates();
                Console.WriteLine("Concert removed successfully.");
            }
            else
            {
                Console.WriteLine("Date doesn't exist.");
            }

        }

        public void ShowDatesView() 
        {
            if (_tour.Concerts.Count==0)
            {
                Console.WriteLine("Tour concert list is empty.");
            }
            else
            {
                var concertsSortedByDate = _tour.Concerts.OrderBy(p => p.Date).ToList();
                Console.WriteLine(_tour.Theme + " - DATES:");
                foreach (var concert in concertsSortedByDate)
                {
                    Console.WriteLine(concert.Date.Day.ToString() + "." + concert.Date.Month.ToString() + "." + concert.Date.Year.ToString() + " - " + concert.Title + " - " + concert.Venue.Name + " - " + concert.Venue.Address.City);
                }
            }
        }

        public void AddConcert(Concert concert)
        {
            _concertService.AddItem(concert);
            _tour.SerializeToJson();
        }
        public void RemoveConcert(Concert concert)
        {
            _concertService.RemoveItem(concert);
            _tour.SerializeToJson();
        }
        public void UpdateTourDates()
        {
            _tour.Concerts = _concertService.Items;
            _tour.SerializeToJson();
        }

        public DateTime GetDateTimeView()
        {
            Console.WriteLine("Enter concert date [DD/MM/YYYY]:");
            return DateTimeParser(Console.ReadLine());
        }

        public DateTime DateTimeParser(string date)
        {
            DateTime convertedDate = Convert.ToDateTime(date);
            return convertedDate;
        }

        public Concert GetConcertByDate(string date)
        {
            var concertDate = DateTimeParser(date);
            var concertByDate = _concertService.Items.FirstOrDefault(p => p.Date == concertDate);
            return concertByDate;
        }
        public string ConcertTitleCreator(string title)
        {
            if (String.IsNullOrWhiteSpace(title))
            {
                return _tour.Theme;
            }
            else
            {
                return title;
            }
        } 
    }
}
