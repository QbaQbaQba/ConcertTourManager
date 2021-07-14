using System;
using System.Collections.Generic;
using System.Text;
using ConcertTourManager.App.Common;
using ConcertTourManager.Domain.Entity;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace ConcertTourManager.App.Concrete
{
    public class TourService : BaseService<Tour>
    {
        public TourService()
        {
        }
        public TourService(string configFilesFolderPath)
        {
            ConfigFilesFolderPath = configFilesFolderPath;
            var configFilesPaths = GetPathsOfConfigFiles(ConfigFilesFolderPath);
            foreach (var path in configFilesPaths)
            {
                Items.Add(new Tour(path));
            }
        }
        public string ConfigFilesFolderPath { get; private set; }
        public List<string> GetPathsOfConfigFiles(string configFilesFolderPath)
        {
            return Directory.GetFiles(configFilesFolderPath).ToList();
        }
    }
}
