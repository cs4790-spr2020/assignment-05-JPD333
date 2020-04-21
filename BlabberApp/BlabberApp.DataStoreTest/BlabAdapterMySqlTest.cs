using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlabberApp.DataStore.Adapters;
using BlabberApp.DataStore.Plugins;
using BlabberApp.Domain.Entities;

namespace BlabberApp.DataStoreTest
{
    [TestClass]
    public class BlabAdapter_MySql_UnitTests
    {
        private BlabAdapter _harness = new BlabAdapter(new MySqlBlab());
        private User _user;
        private string _email;

        [TestInitialize]
        public void Setup()
        {
            _email = "fooabar@example.com";
            _user = new User(_email);
        }
        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void Canary()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestAddAndGetBlab()
        {
            //Arrange
            Blab blab = new Blab("Now is the time for, blabs...", _user);
            //Act
            _harness.Add(blab);
            ArrayList actual = (ArrayList)_harness.GetByUserId(_email);
            //Assert
            Assert.AreEqual(1, actual.Count);
            _harness.Remove(blab);
        }

        [TestMethod]
        public void TestUpdateBlab()
        {
            //Arrange
            Blab blab = new Blab("Now is the time for, blabs...", _user);
            string updatedMessage = "This is my updated blab";
            //Act
            _harness.Add(blab);
            blab.Message = updatedMessage;
            _harness.Update(blab);
            _harness.GetById(blab.Id);
            //Assert
            Assert.AreEqual(updatedMessage, blab.Message);
            _harness.Remove(blab);
        }

        [TestMethod]
        public void TestEmptyGetAll()
        {
            //Arrange
            ArrayList blabs = (ArrayList)_harness.GetAll();
            //Act
            int expected = 0;
            int actual = blabs.Count;
            //Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
