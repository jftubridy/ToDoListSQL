using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoList.Models;
using System;
using MySql.Data.MySqlClient;


namespace ToDoList.Tests
{

  [TestClass]
  public class ItemTest : IDisposable
  {
  public void Dispose()
    {
      Item.ClearAll();
    }
    public ItemTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=to_do_list_test;";
    }

    [TestMethod]
public void GetAll_ReturnsEmptyListFromDatabase_ItemList()
{
   List<Item> newList = new List<Item> { };
   List<Item> result = Item.GetAll();
   CollectionAssert.AreEqual(newList, result);
}
   [TestMethod]
public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
{
  Item firstItem = new Item("Mow the lawn");
  Item secondItem = new Item("Mow the lawn");
  Assert.AreEqual(firstItem, secondItem);
}
[TestMethod]
  public void Save_SavesToDatabase_ItemList()
  {
    Item testItem = new Item("Mow the lawn");
    testItem.Save();
    List<Item> result = Item.GetAll();
    List<Item> testList = new List<Item>{testItem};
    CollectionAssert.AreEqual(testList, result);
  }
  [TestMethod]
  public void GetAll_ReturnsItems_ItemList()
  {
    string description01 = "Walk the dog";
    string description02 = "Wash the dishes";
    Item newItem1 = new Item(description01);
    newItem1.Save(); 
    Item newItem2 = new Item(description02);
    newItem2.Save(); 
    List<Item> newList = new List<Item> { newItem1, newItem2 };
    List<Item> result = Item.GetAll();
    CollectionAssert.AreEqual(newList, result);
  }
  [TestMethod]
    public void Find_ReturnsCorrectItemFromDatabase_Item()
    {
      Item newItem = new Item("Mow the lawn");
      newItem.Save();
      Item newItem2 = new Item("Wash dishes");
      newItem2.Save();
      Item foundItem = Item.Find(newItem.Id);
      Assert.AreEqual(newItem, foundItem);
    }

    public static Item Find(int id)
{
  MySqlConnection conn = DB.Connection();
  conn.Open();
  var cmd = conn.CreateCommand() as MySqlCommand;
  cmd.CommandText = @"SELECT * FROM `items` WHERE id = @thisId;";
  MySqlParameter thisId = new MySqlParameter();
  thisId.ParameterName = "@thisId";
  thisId.Value = id;
  cmd.Parameters.Add(thisId);
  var rdr = cmd.ExecuteReader() as MySqlDataReader;
  int itemId = 0;
  string itemDescription = "";
  while (rdr.Read())
  {
     itemId = rdr.GetInt32(0);
     itemDescription = rdr.GetString(1);
  }
  Item foundItem= new Item(itemDescription, itemId);
  conn.Close();
  if (conn != null)
  {
    conn.Dispose();
  }
  return foundItem;
}

  }
}