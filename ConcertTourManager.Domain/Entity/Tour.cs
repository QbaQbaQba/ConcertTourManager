using System;
using System.Collections.Generic;
using System.Text;
using ConcertTourManager.Domain.Common;
using ConcertTourManager.Domain.Helpers;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace ConcertTourManager.Domain.Entity
{
    public class Tour : BaseEntity
    {
        public Tour()
        {
            Concerts = new List<Concert>();
        }
        public Tour(string jsonConfigFilePath)
        {
            JsonConfigFilePath = jsonConfigFilePath;
            var deserializedTour = DeserializeFromJson(jsonConfigFilePath);
            Id = deserializedTour.Id;
            Theme = deserializedTour.Theme;
            Headliner = deserializedTour.Headliner;
            Concerts = deserializedTour.Concerts;
        }
        public string JsonConfigFilePath { get; private set; }
        public string Theme { get; set; }
        public Artist Headliner { get; set; }
        public List<Concert> Concerts { get; set; }
        public void SerializeToJson(string jsonConfigFilePath)
        {
            string output = JsonConvert.SerializeObject(this);

            using StreamWriter sw = new StreamWriter(jsonConfigFilePath);
            using JsonWriter jw = new JsonTextWriter(sw);

            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(jw, this);
        }
        public void SerializeToJson()
        {
            string output = JsonConvert.SerializeObject(this);

            using StreamWriter sw = new StreamWriter(JsonConfigFilePath);
            using JsonWriter jw = new JsonTextWriter(sw);

            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(jw, this);
        }
        private Tour DeserializeFromJson(string jsonConfigFilePath)
        {
            string jsonConfigFile = File.ReadAllText(jsonConfigFilePath);
            var deserializedTour = JsonConvert.DeserializeObject<Tour>(jsonConfigFile);
            return deserializedTour;
        }
        private string GetTourReportText()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine("Tour report file");
            report.AppendLine();
            report.AppendLine("THEME: " + Theme);
            report.AppendLine("HEADLINER: " + Headliner.Name);
            report.AppendLine("TOUR DATES:");
            var tourDates = Concerts.OrderBy(p => p.Date);
            foreach (var concert in tourDates)
            {
                report.AppendLine(concert.Info());
            }
            return report.ToString();
        }
        public string GenerateTourReportFile(string filePath)
        {
            using StreamWriter sw = new StreamWriter(filePath);
            //StringReader stringReader = new StringReader(GetTourReportText(tour));
            sw.WriteLine(GetTourReportText());
            return "Raport generated. File path: " + filePath;
        }
    }
}
