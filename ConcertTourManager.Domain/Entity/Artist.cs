using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using ConcertTourManager.Domain.Common;
using ConcertTourManager.Domain.Helpers;

namespace ConcertTourManager.Domain.Entity
{
    public class Artist : BaseEntity
    {
        public Artist()
        {

        }
        public Artist(string name)
        {
            Name = name;
        }
        [XmlElement("Name")]
        public string Name { get; set; }
        
    }
}
