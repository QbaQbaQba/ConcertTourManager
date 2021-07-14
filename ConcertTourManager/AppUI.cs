using System;
using System.Collections.Generic;
using System.Text;
using ConcertTourManager.Domain.Helpers;
using ConcertTourManager.Domain.Entity;
using ConcertTourManager.App.Concrete;
using ConcertTourManager.App.Managers;

namespace ConcertTourManager
{
    public class AppUI
    {

        private AppObjects _appObjects;
        private MenuActionService _menuActionService;
        public AppUI(AppObjects appObjects, MenuActionService menuActionService)
        {
            _appObjects = appObjects;
            _menuActionService = menuActionService;
        }

        public void RunUI()
        {
            MainMenu();
        }
        private void MainMenu()
        {
            switch (MenuHandler(MenuName.Main))
            {
                case 1:
                    DataBaseMenu();
                    break;
                case 2:
                    TourManagementMenu();
                    break;
                case 3:
                    break;
            }
        }
        private void DataBaseMenu()
        {
            switch (MenuHandler(MenuName.DataBase))
            {
                case 1:
                    DataBaseArtistsMenu();
                    break;
                case 2:
                    DataBaseVenuesMenu();
                    break;
                case 3:
                    MainMenu();
                    break;
            }
        }
        private void DataBaseArtistsMenu()
        {
            switch (MenuHandler(MenuName.DataBaseArtists))
            {
                case 1:
                    _appObjects.ArtistManager.AddArtistView();
                    Console.WriteLine();
                    DataBaseArtistsMenu();
                    break;
                case 2:
                    _appObjects.ArtistManager.RemoveArtistView();
                    Console.WriteLine();
                    DataBaseArtistsMenu();
                    break;
                case 3:
                    _appObjects.ArtistManager.ShowArtistsView();
                    Console.WriteLine();
                    DataBaseArtistsMenu();
                    break;
                case 4:
                    DataBaseMenu();
                    break;
                case 5:
                    MainMenu();
                    break;
            }
        }
        private void DataBaseVenuesMenu()
        {
            switch (MenuHandler(MenuName.DataBaseVenues))
            {
                case 1:
                    _appObjects.VenueManager.AddVenueView();
                    Console.WriteLine();
                    DataBaseVenuesMenu();
                    break;
                case 2:
                    _appObjects.VenueManager.RemoveVenueView();
                    Console.WriteLine();
                    DataBaseVenuesMenu();
                    break;
                case 3:
                    _appObjects.VenueManager.ShowVenuesView();
                    Console.WriteLine();
                    DataBaseVenuesMenu();
                    break;
                case 4:
                    DataBaseMenu();
                    break;
                case 5:
                    MainMenu();
                    break;
            }
        }
        private void TourManagementMenu()
        {
            switch (MenuHandler(MenuName.TourManagement))
            {
                case 1:
                    _appObjects.TourManager.CreateTourView();
                    Console.WriteLine();
                    TourManagementMenu();
                    break;
                case 2:
                    _appObjects.TourManager.RemoveTourView();
                    Console.WriteLine();
                    TourManagementMenu();
                    break;
                case 3:
                    var tour = _appObjects.TourManager.DefineTourView();
                    if (tour!=null)
                    {
                        ManageTourMenu(tour);
                    }
                    else
                    {
                        Console.WriteLine("Error during tour definition!");
                        TourManagementMenu();
                    }
                    break;
                case 4:
                    _appObjects.TourManager.ShowTours();
                    Console.WriteLine();
                    TourManagementMenu();
                    break;
                case 5:
                    MainMenu();
                    break;
            }
        }
        private void ManageTourMenu(Tour tour)
        {
            var concertManager = new ConcertManager(tour, _appObjects.VenueManager, _appObjects.ArtistManager);
            Console.WriteLine("[" + tour.Headliner.Name + " - " + tour.Theme + "]");
            switch (MenuHandler(MenuName.ManageTour))
            {
                case 1:
                    concertManager.AddConcertView();
                    Console.WriteLine();
                    ManageTourMenu(tour);
                    break;
                case 2:
                    concertManager.RemoveConcertView();
                    Console.WriteLine();
                    ManageTourMenu(tour);
                    break;
                case 3:
                    concertManager.ShowDatesView();
                    Console.WriteLine();
                    ManageTourMenu(tour);
                    break;
                case 4:
                    Console.WriteLine(tour.GenerateTourReportFile(@"D:\DOTNET_KURSY\KURS_SZKOLA_DOTNETA\ConcertTourManager\Reports\" + tour.Theme + "_TourReport.txt"));
                    Console.WriteLine();
                    ManageTourMenu(tour);
                    break;
                case 5:
                    TourManagementMenu();
                    break;
                case 6:
                    MainMenu();
                    break;
            }
        }

        private void ShowMenuActions(MenuName menuName)
        {
            Console.WriteLine(GetMenuTitle(menuName));
            int actionNumerator = 0;

            foreach (var action in _menuActionService.Items)
            {
                if (action.MenuName==menuName)
                {
                    actionNumerator+=1;
                    Console.WriteLine(actionNumerator + " - " + action.ActionName);
                }
            }
        }
        private int MenuActionCount (MenuName menuName)
        {
            int menuActionCount = 0;
            foreach (var action in _menuActionService.Items)
            {
                if (action.MenuName == menuName)
                {
                    menuActionCount++;
                }
            }
            return menuActionCount;
        }

        private string GetMenuTitle(MenuName menuName)
        {
            string menuTitle = menuName switch
            {
                MenuName.Main => "----- MAIN MENU -----",
                MenuName.DataBase => "----- DATA BASE -----",
                MenuName.DataBaseArtists => "----- DATA BASE - ARTISTS -----",
                MenuName.DataBaseVenues => "----- DATA BASE - VENUES -----",
                MenuName.TourManagement => "----- TOUR MANAGER -----",
                MenuName.ManageTour => "----- TOUR MANAGER -----",
                _ => ""
            };
            return menuTitle;
        }

        private ConsoleKeyInfo GetKey()
        {
            var pressedKey= Console.ReadKey();
            Console.WriteLine();
            return pressedKey;
        }
        private int GetAction(MenuName menuName)
        {
            var chosenAction=GetKey();
            var chosenActionIsValid = ChosenActionValidator(menuName, chosenAction);
            if(!chosenActionIsValid)
            {
                do
                {
                    Console.WriteLine("Chose option is not valid!");
                    chosenAction = GetKey();
                    chosenActionIsValid = ChosenActionValidator(menuName, chosenAction);
                } while (!chosenActionIsValid);
            }
            return int.Parse(chosenAction.KeyChar.ToString());
        }

        private bool ChosenActionValidator(MenuName menuName, ConsoleKeyInfo pressedKey)
        {
            int actionsCount = MenuActionCount(menuName);
            if (Char.IsDigit(pressedKey.KeyChar))
            {
                if(int.Parse(pressedKey.KeyChar.ToString())<=actionsCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private int MenuHandler(MenuName menuName)
        {
            ShowMenuActions(menuName);
            int chosenOption = GetAction(menuName);
            return chosenOption;
        }
    }
}
