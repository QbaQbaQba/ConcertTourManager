using System;
using System.Collections.Generic;
using System.Text;
using ConcertTourManager.Domain.Common;
using ConcertTourManager.Domain.Helpers;

namespace ConcertTourManager.Domain.Entity
{
    public class MenuAction : BaseEntity
    {
        public MenuAction(string actionName, MenuName menuName)
        {
            ActionName = actionName;
            MenuName = menuName;
        }
        public string ActionName { get; set; }
        public MenuName MenuName { get; set; }
    }
}

