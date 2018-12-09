using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CN.Common.Contracts.IManagers;
using CN.UnitTest.Containers;
using CN.Common.Models;
using CN.Common.Contracts.IRepositories;
using CN.Common.Models.TempModels;
using CN.Common.Enums;

namespace CN.UnitTest.Unit_Tests
{
    [TestClass]
    public class LinesManagerUnitTest
    {
        ILinesManager linesManager { get; set; }
        INetworkRepository networkRepository { get; set; }
        public LinesManagerUnitTest()
        {
            linesManager = UnitTestContainer.container.GetInstance<ILinesManager>();
            networkRepository = UnitTestContainer.container.GetInstance<INetworkRepository>();
        }
        [TestMethod]
        public void TestGetNewLine()
        {
            Client client = networkRepository.GetClientByID("312149891");
            Assert.AreEqual("0523974471", linesManager.GetNewLine(client));
        }
        [TestMethod]
        public void TestGetDefaultPackages()
        {
            Assert.AreEqual(3, linesManager.GetDefaultPackages().Count);
        }
        [TestMethod]
        public void TestGetPackageDetails()
        {
            Assert.AreEqual(15, linesManager.GetPackageDetailsByPackageId(1).DiscountPercentage);
        }
        [TestMethod]
        public void TestGetGetSelectedNumbers()
        {
            Assert.AreEqual("", linesManager.GetSelectedNumbersById(1).FirstNumber);
        }
        [TestMethod]
        public void TestGetClientLines()
        {
            Assert.AreEqual("", linesManager.GetClientLinesByClientId("0523974471").Count);
        }
        [TestMethod]
        public void TestGetPackageByLineID()
        {
            Assert.AreEqual("", linesManager.GetPackageByLineId("0523974471"));
        }
        [TestMethod]
        public void TestSendLinePackObj()
        {
            Package pack = linesManager.GetPackageByLineId("0523974471");
            PackageDetails packDet = linesManager.GetPackageDetailsByPackageId(pack.ID);
            SelectedNumbers selectedNums = linesManager.GetSelectedNumbersById(packDet.SelectedNumbersID);
            LinePackObject linePackObj = new LinePackObject("0523974471", pack, packDet, selectedNums, "312149891", 1);
            Assert.AreEqual(RequestStatusEnum.Success, linesManager.SendLinePackageObj(linePackObj));
        }
        [TestMethod]
        public void TestDeleteLine()
        {
            Assert.AreEqual(RequestStatusEnum.Success, linesManager.DeleteLine("0523974471"));
        }
        [TestMethod]
        public void TestGetLineStatus()
        {
            Assert.AreEqual(LineStatusEnum.Available, linesManager.GetLineStatus("0523974471"));
        }
        [TestMethod]
        public void TestUpdateLineStatus()
        {
            Assert.AreEqual(RequestStatusEnum.Success, linesManager.UpdateLineStatus(new Line { Number = "0523974471" }));
        }
        [TestMethod]
        public void TestLineExistedAtDate()
        {
            YearAndMonth date = new YearAndMonth(2018, 11);
            Assert.AreEqual(true, linesManager.LineExistedAtDate("0523974471", date));
        }
        [TestMethod]
        public void TestGetClientValue()
        {
            Assert.AreEqual("", linesManager.GetClientValue(networkRepository.GetClientByID("312149891")));
        }
        [TestMethod]
        public void TestGetSumOfLast3Month()
        {
            Assert.AreEqual("", linesManager.GetSumOfLast3Months(networkRepository.GetClientByID("312149891")));
        }

    }
}

