using System;
using System.Collections.Generic;
using System.Text;
using ConcertTourManager.Domain.Entity;
using ConcertTourManager.App.Common;
using ConcertTourManager.Domain.Helpers;
using System.Linq;

namespace ConcertTourManager.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }

        public List<MenuAction> GetMenuActionsByMenuName(MenuName menuName)
        {
            return Items.Where(p => p.MenuName == menuName).ToList();
        }
        private void Initialize()
        {
            AddItem(new MenuAction("DATA BASE", MenuName.Main));
            AddItem(new MenuAction("TOUR MANAGER", MenuName.Main));
            AddItem(new MenuAction("EXIT", MenuName.Main));

            AddItem(new MenuAction("ARTISTS", MenuName.DataBase));
            AddItem(new MenuAction("VENUES", MenuName.DataBase));
            AddItem(new MenuAction("BACK TO MAIN MENU", MenuName.DataBase));


            AddItem(new MenuAction("ADD", MenuName.DataBaseArtists));
            AddItem(new MenuAction("REMOVE", MenuName.DataBaseArtists));
            AddItem(new MenuAction("SHOW", MenuName.DataBaseArtists));
            AddItem(new MenuAction("BACK", MenuName.DataBaseArtists));
            AddItem(new MenuAction("BACK TO MAIN MENU", MenuName.DataBaseArtists));

            AddItem(new MenuAction("ADD", MenuName.DataBaseVenues));
            AddItem(new MenuAction("REMOVE", MenuName.DataBaseVenues));
            AddItem(new MenuAction("SHOW", MenuName.DataBaseVenues));
            AddItem(new MenuAction("BACK", MenuName.DataBaseVenues));
            AddItem(new MenuAction("BACK TO MAIN MENU", MenuName.DataBaseVenues));

            AddItem(new MenuAction("CREATE NEW TOUR", MenuName.TourManagement));
            AddItem(new MenuAction("REMOVE TOUR", MenuName.TourManagement));
            AddItem(new MenuAction("MANAGE TOUR", MenuName.TourManagement));
            AddItem(new MenuAction("SHOW MANGED TOURS", MenuName.TourManagement));
            AddItem(new MenuAction("BACK TO MAIN MENU", MenuName.TourManagement));

            AddItem(new MenuAction("ADD CONCERT", MenuName.ManageTour));
            AddItem(new MenuAction("REMOVE CONCERT", MenuName.ManageTour));
            AddItem(new MenuAction("SHOW DATES", MenuName.ManageTour));
            AddItem(new MenuAction("GENERATE REPORT", MenuName.ManageTour));
            AddItem(new MenuAction("BACK", MenuName.ManageTour));
            AddItem(new MenuAction("BACK TO MAIN MENU", MenuName.ManageTour));
        }

    }
}
