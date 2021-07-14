using System;
using System.Collections.Generic;
using System.Text;
using ConcertTourManager.App.Concrete;
using ConcertTourManager.App.Managers;

namespace ConcertTourManager
{
    public class AppObjects
    {
        public ArtistService ArtistService { get; private set; }
        public TourService TourService { get; private set; }
        public VenueService VenueService { get; private set; }
        public ArtistManager ArtistManager { get; private set; }
        public TourManager TourManager { get; private set; }
        public VenueManager VenueManager { get; private set; }
        public AppObjects()
        {
            InitializeServices();
            InitializeManagers();
        }
        public void InitializeServices()
        {
            ArtistService = new ArtistService(@"D:\DOTNET_KURSY\KURS_SZKOLA_DOTNETA\ConcertTourManager\BaseFiles\ArtistsDB.xml");
            TourService = new TourService(@"D:\DOTNET_KURSY\KURS_SZKOLA_DOTNETA\ConcertTourManager\ConcertTours");
            VenueService = new VenueService(@"D:\DOTNET_KURSY\KURS_SZKOLA_DOTNETA\ConcertTourManager\BaseFiles\VenuesDB.xml");
        }
        public void InitializeManagers()
        {
            ArtistManager = new ArtistManager(ArtistService);
            VenueManager = new VenueManager(VenueService);
            TourManager = new TourManager(TourService, ArtistManager);
        }
    }
}
