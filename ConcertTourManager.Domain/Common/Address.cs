using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;

namespace ConcertTourManager.Domain.Common
{
    public class Address
    {
        public Address()
        {

        }
        public Address(string streetAddress, string city, string zipCode, string country)
        {
            StreetAddress = streetAddress;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }
        [XmlAttribute("StreetAddres")]
        public string StreetAddress { get; set; }

        [XmlAttribute("City")]
        public string City { get; set; }

        [XmlAttribute("ZipCode")]
        public string ZipCode { get; set; }

        [XmlAttribute("Country")]
        public string Country { get; set; }

    }
}
