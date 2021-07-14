using System;
using System.Collections.Generic;
using System.Text;
using ConcertTourManager.App.Common;
using ConcertTourManager.Domain.Entity;
using System.Xml.Serialization;
using System.IO;

namespace ConcertTourManager.App.Concrete
{
    public class ArtistService : BaseService<Artist>
    {
        public ArtistService()
        {
            
        }
        public ArtistService(string xmlDBFilePath)
        {
            _xmlDBFilePath = xmlDBFilePath;
            InitializeFromXmlDB();
        }
        private string _xmlDBFilePath;

        private void InitializeFromXmlDB()
        {
            Items = GetArtistsFromXml(_xmlDBFilePath);
        }
        private List<Artist> GetArtistsFromXml(string filePath)
        {
            string xml = File.ReadAllText(filePath);
            StringReader stringReader = new StringReader(xml);
            XmlRootAttribute root = new XmlRootAttribute("Artists");
            root.IsNullable = true;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Artist>),root);
            var artists = (List<Artist>)xmlSerializer.Deserialize(stringReader);
            return artists;
        }
        public void SerializeArtistsToXmlDB()
        {
            XmlRootAttribute root = new XmlRootAttribute("Artists");
            root.IsNullable = true;
            XmlSerializer xmlSerDes = new XmlSerializer(typeof(List<Artist>), root);
            using StreamWriter sw = new StreamWriter(_xmlDBFilePath);
            xmlSerDes.Serialize(sw, Items);
        }
    }
}
