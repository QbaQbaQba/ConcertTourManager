using System;
using System.Collections.Generic;
using ConcertTourManager.App.Concrete;
using ConcertTourManager.App.Common;
using ConcertTourManager.App.Managers;
using ConcertTourManager.Domain.Entity;
using ConcertTourManager.Domain.Common;
using ConcertTourManager.Domain.Helpers;
using ConcertTourManager.App.Abstract;
using System.Text;
using System.Linq;
using Moq;
using FluentAssertions;
using Xunit;

namespace ConcertTourManager.Tests
{
    public class VenueManagerTests
    {
        [Fact]
        public void WhenExistingIdIsGivenThenVenueIsReturned()
        {
            //Arrange
            var testVenue = new Venue(){Id = 10, Name = "TestVenue"};
            var venueService = new VenueService();
            venueService.AddItem(testVenue);
            var venueManager = new VenueManager(venueService);

            //Act
            var returnedVenue = venueManager.GetVenueById(testVenue.Id);

            //Assert
            Assert.Equal(testVenue, returnedVenue);
        }
    }
}
