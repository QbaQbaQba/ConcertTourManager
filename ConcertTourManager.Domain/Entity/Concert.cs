using System;
using System.Collections.Generic;
using System.Text;
using ConcertTourManager.Domain.Common;
using ConcertTourManager.Domain.Helpers;

namespace ConcertTourManager.Domain.Entity
{
    public class Concert : BaseEntity
    {
        public Concert(string title, Artist artist, Venue venue, DateTime date, decimal ticketCost)
        {
            Title = title;
            Headliner = artist;
            Venue = venue;
            Date = date;
            TicketCost = ticketCost; 
        }
        public string Title { get; set; }
        public Artist Headliner { get; set; }
        public Venue Venue { get; set; }
        public DateTime Date { get; set; }
        public decimal TicketCost { get; private set; }
        public string Info()
        {
            return Date.Day + "." + Date.Month + "." + Date.Year + " - " + Venue.Name + "  |  " + Venue.Address.City;
        }
    }
}
