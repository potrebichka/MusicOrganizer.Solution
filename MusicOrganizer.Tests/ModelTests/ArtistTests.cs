using Microsoft.VisualStudio.TestTools.UnitTesting;
using System; // Add this if you want to use Console.WriteLine() in test method.
using System.Collections.Generic;
using MusicOrganizer.Models;
using MySql.Data.MySqlClient;

namespace MusicOrganizer.Tests
{
  [TestClass]
  public class ArtistTest : IDisposable
  {

    public void Dispose()
    {
      Artist.ClearAll();
    }
    public ArtistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=artists_test;";
    }

    [TestMethod]
    public void GetAll_ReturnEmptyListFromDatabase_ArtistList()
    {
      List<Artist> newList = new List<Artist> { };
      List<Artist> result = Artist.GetAll();
      CollectionAssert.AreEqual(newList, result);   
    }
    
    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Artist()
    {
     Artist firstArtist = new Artist("Mow the lawn");
     Artist secondArtist = new Artist("Mow the lawn");
     Assert.AreEqual(firstArtist, secondArtist);
    }
     [TestMethod]
     public void Save_SavesToDatabase_ArtistList()
     {
       Artist testArtist = new Artist("Hello");
       testArtist.Save();
       List<Artist> result = Artist.GetAll();
       List<Artist> testList = new List<Artist>{testArtist};
       CollectionAssert.AreEqual(testList, result);

     }
     
    [TestMethod]
    public void ArtistConstructor_CreatesInstanceOfArtist_Artist()
    {
      Artist newArtist = new Artist("test");
      Assert.AreEqual(typeof(Artist), newArtist.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "Walk the dog.";

      //Act
      Artist newArtist = new Artist(name);
      string result = newArtist.Name;

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void SetName_SetName_String()
    {
      //Arrange
      string name = "Walk the dog.";
      Artist newArtist = new Artist(name);

      //Act
      string updatedName = "Do the dishes";
      newArtist.Name = updatedName;
      string result = newArtist.Name;

      //Assert
      Assert.AreEqual(updatedName, result);
    }

    // [TestMethod]
    // public void GetAll_ReturnsEmptyList_ArtistList()
    // {
    //   // Arrange
    //   List<Artist> newList = new List<Artist> { };

    //   // Act
    //   List<Artist> result = Artist.GetAll();

    //   // Assert
    //   CollectionAssert.AreEqual(newList, result);
    // }

    [TestMethod]
    public void GetAll_ReturnsArtists_ArtistList()
    {
      //Arrange
      string name01 = "Walk the dog";
      string name02 = "Wash the dishes";
      Artist newArtist1 = new Artist(name01);
      newArtist1.Save();
      Artist newArtist2 = new Artist(name02);
      newArtist2.Save();
      List<Artist> newList = new List<Artist> { newArtist1, newArtist2 };

      //Act
      List<Artist> result = Artist.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetId_ArtistsInstantiateWithAnIdAndGetterReturns_Int()
    {
      //Arrange
      string name = "Walk the dog.";
      Artist newArtist = new Artist(name);

      //Act
      int result = newArtist.Id;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectArtist_Artist()
    {
      //Arrange
        string name01 = "Walk the dog";
        string name02 = "Wash the dishes";
        Artist newArtist1 = new Artist(name01);
        newArtist1.Save();
        Artist newArtist2 = new Artist(name02);
        newArtist2.Save();

        //Act
        Artist result = Artist.Find(newArtist1.Id);

        //Assert
        Assert.AreEqual(newArtist1, result);
    }

    [TestMethod]
    public void FindArtist_ReturnCorrectArtistFromDataBase_Artist()
    {
      Artist newArtist = new Artist("Mow the lawn");
      newArtist.Save();
      Artist newArtist2 = new Artist("Wash dishes");
      newArtist2.Save();
      int foundArtist = Artist.FindArtist(newArtist.Name);
      Assert.AreEqual(newArtist.Id, foundArtist);

    }
    [TestMethod]
    public void GetArtistPartial_ReturnListOfArtists_List()
    {
            //Arrange
        string name01 = "Walk the dog";
        string name02 = "Wash the dishes";
        Artist newArtist1 = new Artist(name01);
        Artist newArtist2 = new Artist(name02);

        List<Artist> artists =  Artist.GetArtistPartial("dog");
        Assert.AreEqual(typeof(List<Artist>), artists.GetType());
    }

    [TestMethod]
    public void GetArtistPartial_ReturnListOfArtists_CorrectList()
    {
            //Arrange
        string name01 = "Walk the dog";
        string name02 = "Wash the dishes";
        Artist newArtist1 = new Artist(name01);
        newArtist1.Save();
        Artist newArtist2 = new Artist(name02);
        newArtist2.Save();

        List<Artist> artists =  Artist.GetArtistPartial("dog");
        List<Artist> result = new List <Artist> {newArtist1};
        CollectionAssert.AreEqual(artists, result);
    }

    [TestMethod]
    public void GetArtistPartial_ReturnListOfArtists_CorrectList2()
    {
            //Arrange
        string name01 = "Walk the dog";
        string name02 = "Wash the dog";
        Artist newArtist1 = new Artist(name01);
        newArtist1.Save();
        Artist newArtist2 = new Artist(name02);
        newArtist2.Save();

        List<Artist> artists =  Artist.GetArtistPartial("dog");
        List<Artist> result = new List <Artist> {newArtist1, newArtist2};
        CollectionAssert.AreEqual(artists, result);
    }

    [TestMethod]
    public void GetArtistPartial_ReturnListOfArtists_CorrectList3()
    {
            //Arrange
        string name01 = "Walk thedog";
        string name02 = "Wash thedog";
        Artist newArtist1 = new Artist(name01);
        newArtist1.Save();
        Artist newArtist2 = new Artist(name02);
        newArtist2.Save();

        List<Artist> artists =  Artist.GetArtistPartial("dog");
        foreach(Artist artist in artists)
        {
            Console.WriteLine("Name", artist.Name);
        }
        List<Artist> result = new List <Artist> {newArtist1, newArtist2};
        CollectionAssert.AreEqual(artists, result);
    }
  }
}