using System;
using System.Collections.Generic;
using ConcertTourManager.App.Concrete;
using ConcertTourManager.App.Managers;
using ConcertTourManager.Domain.Entity;
using ConcertTourManager.Domain.Helpers;
using System.Text;
using Moq;
using FluentAssertions;
using Xunit;

namespace ConcertTourManager.Tests
{
    public class ArtistManagerTests
    {
        [Fact]
        public void CanGetArtistWhenProperNameIsGiven()
        {
            //Arrange
            var artistService = new ArtistService();
            artistService.Items = GetArtistDB();
            var artistManager = new ArtistManager(artistService);
            var expectedArtist = new Artist() { Name = "D", Id = 2 };

            //Act
            var actualArtist = artistManager.GetArtistByName("D");

            //Assert
            actualArtist.Equals(expectedArtist);
            actualArtist.Should().BeOfType(typeof(Artist));
        }

        [Fact]
        public void CanAddArtistWhenArtistNameIsGiven()
        {
            //Method test without serialization
            //Arrange
            var artistToAdd = "TestArtist";
            var artistService = new ArtistService();
            var artistManager = new ArtistManager(artistService);
            var expectedAddedArtist = new Artist() { Id = 1, Name = artistToAdd };

            //Act
            artistManager.AddArtistByName(artistToAdd,false);
            var actualAddedArtist = artistManager.GetArtistByName(artistToAdd);
            //Assert
            actualAddedArtist.Should().NotBeNull();
            actualAddedArtist.Equals(expectedAddedArtist);         
        }
        [Fact]
        public void CanRemoveArtistWhenExistingArtistNameIsGiven()
        {
            //Method test without serialization
            //Arrange
            var artistToAdd = "TestArtist";
            var artistService = new ArtistService();
            var artistManager = new ArtistManager(artistService);
            var expectedAddedArtist = new Artist() { Id = 1, Name = artistToAdd };

            //Act
            artistManager.AddArtistByName(artistToAdd, false);
            artistManager.RemoveArtistByName(artistToAdd, false);

            //Assert
            artistManager.GetArtistByName(artistToAdd).Should().BeNull();
        }
        [Fact]
        public void CanSortArtistListByName()
        {
            //Arrange
            var artistService = new ArtistService();
            artistService.Items = GetArtistDB();
            var artistManager = new ArtistManager(artistService);

            //Act
            var sortedArtistList = artistManager.GetArtistsSortedByName();

            //Assert
            sortedArtistList[0].Name = "A";
            sortedArtistList[1].Name = "B";
            sortedArtistList[2].Name = "C";
            sortedArtistList[3].Name = "D";
            sortedArtistList[4].Name = "F";
        }

        private List<Artist> GetArtistDB()
        {
            var artistDB = new List<Artist>();
            artistDB.Add(new Artist() { Name = "B", Id = 1 });
            artistDB.Add(new Artist() { Name = "D", Id = 2 });
            artistDB.Add(new Artist() { Name = "A", Id = 3 });
            artistDB.Add(new Artist() { Name = "F", Id = 4 });
            artistDB.Add(new Artist() { Name = "C", Id = 5 });

            return artistDB;
        }
    }
}
