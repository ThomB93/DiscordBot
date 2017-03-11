using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LethBot2._0;

namespace LethBotTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            BlackCard blackCard = new BlackCard();
            //Act
            BlackCard randomBlackCard = blackCard.GetBlackCard();
            //Assert
            Assert.IsNotNull(randomBlackCard);
        }
    }
}
