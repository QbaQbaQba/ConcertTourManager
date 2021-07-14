using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using ConcertTourManager.App.Common;
using ConcertTourManager.Domain.Entity;

namespace ConcertTourManager.App.Concrete
{
    public class VenueService : BaseService<Venue>
    {
        public VenueService()
        {
        }
        public VenueService(string xmlDBFilePath)
        {
            _xmlDBFilePath = xmlDBFilePath;
            InitializeFromXmlDB();
        }
        private string _xmlDBFilePath;

        private void InitializeFromXmlDB()
        {
            Items = GetVenuesFromXml(_xmlDBFilePath);
        }
        public List<Venue> GetVenuesFromXml(string filePath)
        {
            string xml = File.ReadAllText(filePath);
            StringReader stringReader = new StringReader(xml);
            XmlRootAttribute root = new XmlRootAttribute("Venues");
            root.IsNullable = true;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Venue>), root);
            var venues = (List<Venue>)xmlSerializer.Deserialize(stringReader);
            return venues;
        }
        public void SerializeVenuesToXmlDB()
        {
            XmlRootAttribute root = new XmlRootAttribute("Venues");
            root.IsNullable = true;
            XmlSerializer xmlSerDes = new XmlSerializer(typeof(List<Venue>), root);
            using StreamWriter sw = new StreamWriter(_xmlDBFilePath);
            xmlSerDes.Serialize(sw, Items);
        }

    }
}
