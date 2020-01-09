using Microsoft.VisualStudio.TestTools.UnitTesting;
using System; // Add this if you want to use Console.WriteLine() in test method.
using System.Collections.Generic;
using MusicOrganizer.Models;

namespace MusicOrganizer.Tests
{
  [TestClass]
  public class RecordTest : IDisposable
  {

    public void Dispose()
    {
      Record.ClearAll();
    }

    [TestMethod]
    public void RecordConstructor_CreatesInstanceOfRecord_Record()
    {
      Record newRecord = new Record("test");
      Assert.AreEqual(typeof(Record), newRecord.GetType());
    }

    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      //Arrange
      string title = "Walk the dog.";

      //Act
      Record newRecord = new Record(title);
      string result = newRecord.Title;

      //Assert
      Assert.AreEqual(title, result);
    }

    [TestMethod]
    public void SetTitle_SetTitle_String()
    {
      //Arrange
      string title = "Walk the dog.";
      Record newRecord = new Record(title);

      //Act
      string updatedTitle = "Do the dishes";
      newRecord.Title = updatedTitle;
      string result = newRecord.Title;

      //Assert
      Assert.AreEqual(updatedTitle, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_RecordList()
    {
      // Arrange
      List<Record> newList = new List<Record> { };

      // Act
      List<Record> result = Record.GetAll();

      // Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsRecords_RecordList()
    {
      //Arrange
      string title01 = "Walk the dog";
      string title02 = "Wash the dishes";
      Record newRecord1 = new Record(title01);
      Record newRecord2 = new Record(title02);
      List<Record> newList = new List<Record> { newRecord1, newRecord2 };

      //Act
      List<Record> result = Record.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetId_RecordsInstantiateWithAnIdAndGetterReturns_Int()
    {
      //Arrange
      string Title = "Walk the dog.";
      Record newRecord = new Record(Title);

      //Act
      int result = newRecord.Id;

      //Assert
      Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectRecord_Record()
    {
      //Arrange
      string title01 = "Walk the dog";
      string title02 = "Wash the dishes";
      Record newRecord1 = new Record(title01);
      Record newRecord2 = new Record(title02);

      //Act
      Record result = Record.Find(2);

      //Assert
      Assert.AreEqual(newRecord2, result);
    }
  }
}