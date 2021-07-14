using System;
using ConcertTourManager.App.Concrete;
using ConcertTourManager.Domain.Entity;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConcertTourManager
{
    public class Program
    {

        static void Main(string[] args)
        {
            AppObjects appObjects = new AppObjects();
            MenuActionService menuActionService = new MenuActionService();
            AppUI appUI = new AppUI(appObjects, menuActionService);

            Console.WriteLine("Welcome to Concert Tour Manager App!");
            appUI.RunUI();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
