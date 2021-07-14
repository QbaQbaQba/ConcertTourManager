using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using ConcertTourManager.Domain.Common;
using ConcertTourManager.Domain.Helpers;

namespace ConcertTourManager.Domain.Entity
{
    public class Venue : BaseEntity
    {
        public Venue()
        {
            Address = new Address();
        }
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Type")]
        public VenueType Type { get; set; }

        [XmlElement("Owner")]
        public string Owner { get; set; }

        [XmlElement("Address")]
        public Address Address { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlElement("Phone")]
        public string Phone { get; set; }

        [XmlElement("Capacity")]
        public int Capacity { get; set; }
    }
}
