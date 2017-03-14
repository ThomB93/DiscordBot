using System;
using LethBot2._0;
using NUnit.Framework;
using Moq;

namespace LethBotTests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            //Arrange
            BlackCard blackCard = new BlackCard();
            //Act
            BlackCard randomBlackCard = blackCard.GetBlackCard();
            //Assert
            Assert.IsNotNull(randomBlackCard);
        }

        [Test]
        public void MoqTest()
        {
            
        }
    }
}
