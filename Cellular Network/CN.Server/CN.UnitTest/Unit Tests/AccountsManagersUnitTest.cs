using System;
using System.Collections.Generic;
using CN.Common.Contracts.IManagers;
using CN.Common.Contracts.IRepositories;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using CN.DAL.Databases;
using CN.UnitTest.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CN.UnitTest
{
    [TestClass]
    public class AccountsManagersUnitTest
    {
        IAccountsManager accountsManager { get; set; }
        public AccountsManagersUnitTest()
        {
            accountsManager = UnitTestContainer.container.GetInstance<IAccountsManager>();
        }

        [TestMethod]
        public void TestUserLogin()
        {
            UserLogin userLogin = new UserLogin("Shahaf", "123456");
            User expectedUser = new User { Username = "Shahaf" };
            Assert.AreEqual(expectedUser.Username, accountsManager.UserLogin(userLogin).Username);
        }
        [TestMethod]
        public void TestIsClientExists()
        {
            Assert.AreEqual(true, accountsManager.IsClientIdExists("312149891"));
        }
        [TestMethod]
        public void TestClientSearch()
        {
            List<Client> Clients = new List<Client> { new Client { ID = "312149891" } };
            Assert.AreEqual(Clients[0].ID, accountsManager.SearchForClients("sha")[0].ID);
        }
        [TestMethod]
        public void TestClientLogin()
        {
            Assert.AreEqual("312149891", accountsManager.ClientLogin(new ClientLogin("312149891", 1994)).ID);
        }
    }
}
