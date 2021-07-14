using System;
using System.Collections.Generic;
using ConcertTourManager.App.Concrete;
using ConcertTourManager.Domain.Entity;
using ConcertTourManager.Domain.Helpers;
using System.Text;
using Moq;
using FluentAssertions;
using Xunit;

namespace ConcertTourManager.Tests
{
    public class MenuActionServiceTest
    {
        [Fact]
        public void WhenMenuNameIsGivenThenMenuActionsAreReturned()
        {
            //Arrange
            var testedMenuName = MenuName.Main;
            var expectedMenuActions = new List<MenuAction>()
            {
                new MenuAction("DATA BASE", testedMenuName){Id=0},
                new MenuAction("TOUR MANAGER", testedMenuName){Id=0},
                new MenuAction("EXIT", testedMenuName){Id=0}
            };
            var menuActionService = new Mock<MenuActionService>();

            //Act
            var actualMenuActions = menuActionService.Object.GetMenuActionsByMenuName(testedMenuName);

            //Assert
            actualMenuActions.Equals(expectedMenuActions);
        }

    }
}
